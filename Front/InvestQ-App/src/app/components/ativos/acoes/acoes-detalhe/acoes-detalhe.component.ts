import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Guid } from 'guid-typescript';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Acao } from '@app/models/Acao';
import { Segmento } from '@app/models/Segmento';
import { Setor } from '@app/models/Setor';
import { Subsetor } from '@app/models/Subsetor';
import { TipoDeInvestimento } from '@app/models/TipoDeInvestimento';
import { AcaoService } from '@app/services/acao.service';
import { SegmentoService } from '@app/services/segmento.service';
import { SetorService } from '@app/services/setor.service';
import { SubsetorService } from '@app/services/subsetor.service';
import { TipodeinvestimentoService } from '@app/services/tipodeinvestimento.service';


@Component({
  selector: 'app-acoes-detalhe',
  templateUrl: './acoes-detalhe.component.html',
  styleUrls: ['./acoes-detalhe.component.scss']
})
export class AcoesDetalheComponent implements OnInit {

  public acao = {} as Acao;
  public tiposDeInvestimentos: TipoDeInvestimento[] = [];

  public setores: Setor[] = [];
  public subSetores: Subsetor[] = [];
  public segmentos: Segmento[] = [];

  acaoId: Guid;
  //setorId: Guid;
  //subSetorId: Guid;
  //segmentoId: Guid;

  form!: FormGroup;

  estadoSalvar = 'post';

  get f(): any {
    return this.form.controls;
  }

  get modoEditar(): boolean {
    return this.estadoSalvar === 'put';
  }

  constructor(private fb: FormBuilder,
              private activatedRouter: ActivatedRoute,
              private acaoService: AcaoService,
              private tipodeinvestimentoService: TipodeinvestimentoService,
              private setorService: SetorService,
              private subSetorService: SubsetorService,
              private segmentoService: SegmentoService,
              private spinner: NgxSpinnerService,
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.carregarAcao();
    this.carregarTiposDeInvestimentos();
    this.carregarSetores();
    this.validation();
  }

  setorSelecionado(evento) {
    //alert("setor");
    this.carregarSubSetores(evento.target.value);
  }

  subSetorSelecionado(evento) {
    //alert("subsetor");

    this.carregarSegmentos(evento.target.value);
  }

  public carregarTiposDeInvestimentos(): void {

    const observer = {
      next: (_tiposDeInvestimentos: TipoDeInvestimento[]) => {
        this.tiposDeInvestimentos = _tiposDeInvestimentos;
      },
      error: (error: any) => {
        this.spinner.hide();
        console.error(error);
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.tipodeinvestimentoService.getAllTiposDeInvestimentos().subscribe(observer);
  }

  public carregarSetores(): void {
    const observer = {
      next: (_setores: Setor[]) => {
        this.setores = _setores;
      },
      error: (error: any) => {
        this.spinner.hide();
        console.error(error);
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.setorService.getAllSetores().subscribe(observer);
  }

  public carregarSubSetores(setorId: Guid): void {
    const observer = {
      next: (_subSetores: Subsetor[]) => {
        this.subSetores = _subSetores;
      },
      error: (error: any) => {
        this.spinner.hide();
        console.error(error);
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.subSetorService.getSubsetoresBySetorId(setorId).subscribe(observer);
  }

  public carregarSegmentos(subSetorId: Guid): void {
    const observer = {
      next: (_segmentos: Segmento[]) => {
        this.segmentos = _segmentos;
      },
      error: (error: any) => {
        this.spinner.hide();
        console.error(error);
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.segmentoService.getSegmentosBySubsetorId(subSetorId).subscribe(observer);
  }

  public carregarAcao(): void {

    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      this.acaoId = Guid.createEmpty();
    else {
      this.acaoId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

      if (this.acaoId !== null && !this.acaoId.isEmpty()) {
        this.spinner.show();

        this.estadoSalvar = 'put';

        this.acaoService.getAcaoById(this.acaoId).subscribe({
          next: (_acao: Acao) => {
            this.carregarSubSetores(_acao.setorId);
            this.carregarSegmentos(_acao.subSetorId);
            this.acao = {..._acao};
            this.form.patchValue(this.acao);
          },
          error: (error: any) => {
            this.spinner.hide();
            this.toastr.error('Erro ao tentar carregar a Ação.', 'Erro!');
            console.error(error);
          },
          complete: () => {this.spinner.hide()}
        });
      }
    }
  }

  public validation(): void {
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      cnpj: ['', [Validators.required, Validators.minLength(14), Validators.maxLength(14)]],
      razaoSocial: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      setorId: [null],
      subSetorId: [null],
      segmentoId: [null, [Validators.required]],
      tipoDeInvestimentoId: [null, [Validators.required]]
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public onClicouEm(evento) {
    if (evento.botaoClicado === 'cancelar') {
      this.resetForm();
    } else {
      this.salvarAlteracao();
    }
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public salvarAlteracao(): void {
    this.spinner.show();

    if (this.form.valid) {

      this.acao = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.acao.id, ...this.form.value};

      this.acaoService[this.estadoSalvar](this.acao).subscribe(
        (_acao: Acao) => {

          this.toastr.success('Ação salva com sucesso!', 'Sucesso');
          this.router.navigate([`acoes/detalhe/${_acao.id}`]);
        },
        (error: any) => {
          console.error(error);
          //this.spinner.hide();
          this.toastr.error('Erro ao atualizar a ação', 'Erro');
        },
        () => {
          //this.spinner.hide();
        }
      ).add(() => {this.spinner.hide()});

    }

  }

}
