import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Ativo } from '@app/models/Ativo';
import { Carteira } from '@app/models/Carteira';
import { TipoDeAtivo } from '@app/models/Enum/TipoDeAtivo.enum';
import { Lancamento } from '@app/models/Lancamento';
import { AtivoService } from '@app/services/ativo.service';
import { CarteiraService } from '@app/services/carteira.service';
import { LancamentoService } from '@app/services/lancamento.service';
import { Guid } from 'guid-typescript';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-lancamento-detalhe',
  templateUrl: './lancamento-detalhe.component.html',
  styleUrls: ['./lancamento-detalhe.component.scss']
})
export class LancamentoDetalheComponent implements OnInit {

  public lancamento = {} as Lancamento;
  public lancamentos: Lancamento[] = [];
  public ativos: Ativo[] = [];
  public carteiraDescricao: string = '';
  public carteiraId: Guid;

  lancamentoId: Guid;
  form!: FormGroup;

  tipoDeMovimentacaoOp: any[];
  tipoDeAtivoOp: any[];

  estadoSalvar = 'post';

  get f(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY',
      containerClass: 'theme-default',
      showWeekNumbers: false
    }
  }

  get modoEditar(): boolean {
    return this.estadoSalvar === 'put';
  }

  constructor(private fb: FormBuilder,
              private activatedRouter: ActivatedRoute,
              private localeService: BsLocaleService,
              private lancamentoService: LancamentoService,
              private ativoService: AtivoService,
              private carteiraService: CarteiraService,
              private spinner: NgxSpinnerService,
              private router: Router,
              private toastr: ToastrService)
  {
    this.localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.validation();
    this.carregarLancamento();

    this.tipoDeMovimentacaoOp = this.lancamentoService.getTipoDeMovimentacao();
    this.tipoDeAtivoOp = this.lancamentoService.getTipoDeAtivo();
  }

  tipoDeAtivoSelecionado(evento) {
    this.carregarAtivos(evento.target.value);
  }

  public carregarAtivos(tipoDeAtivo: TipoDeAtivo): void {
    const observer = {
      next: (_ativos: Ativo[]) => {
        this.ativos = _ativos;
      },
      error: (error: any) => {
        this.spinner.hide();
        console.error(error);
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.ativoService.getAllAtivosByTipoDeAtivo(tipoDeAtivo).subscribe(observer);
  }

  public bucarNomeDaCarteira(carteiraId: Guid): void {
    this.carteiraService.getCarteiraById(carteiraId).subscribe({
      next: (carteira: Carteira) => {
        this.carteiraDescricao = carteira.descricao;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao tentar carregar o nome da carteira.', 'Erro!');
        console.error(error)
      },
      complete: () => {this.spinner.hide()}
    });
  }

  public carregarLancamento(): void {

    if (this.activatedRouter.snapshot.paramMap.get('id') === null) {
      this.lancamentoId = Guid.createEmpty();
      this.carteiraId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('carteiraid').toString());
      this.bucarNomeDaCarteira(this.carteiraId);
      this.form.controls['carteiraId'].setValue(this.carteiraId.toString());
    }
    else {

      this.lancamentoId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

      if (this.lancamentoId !== null && !this.lancamentoId.isEmpty()) {
        this.spinner.show();

        this.estadoSalvar = 'put';

        this.lancamentoService.getLancamentoById(this.lancamentoId).subscribe({
          next: (_lancamento: Lancamento) => {
            this.lancamento = {..._lancamento};
            this.lancamento.tipoDeAtivo = this.lancamento.ativo.tipoDeAtivo;
            this.carregarAtivos(this.lancamento.tipoDeAtivo);
            this.form.patchValue(this.lancamento);
          },
          error: (error: any) => {
            this.spinner.hide();
            this.toastr.error('Erro ao tentar carregar o Provento.', 'Erro!');
            console.error(error);
          },
          complete: () => {this.spinner.hide()}
        });
      }
    }
  }

  public validation(): void {
    this.form = this.fb.group({
      valorDaOperacao: ['', [Validators.required]],
      dataDaOperacao: ['', [Validators.required]],
      quantidade: ['', [Validators.required]],
      tipoDeMovimentacao: [0, [Validators.required]],
      tipoDeOperacao: [0, [Validators.required]],
      ativoId: [null, [Validators.required]],
      tipoDeAtivo: [null, [Validators.required]],
      carteiraId: [null, [Validators.required]]
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

      this.lancamento = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.lancamento.id, ...this.form.value};

      this.lancamentoService[this.estadoSalvar](this.lancamento).subscribe(
        (_lancamento: Lancamento) => {

          this.toastr.success('Lançamento salvo com sucesso!', 'Sucesso');
          this.router.navigate([`lancamentos/detalhe/${_lancamento.id}`]);
        },
        (error: any) => {
          console.error(error);
          //this.spinner.hide();
          this.toastr.error('Erro ao atualizar o lançamento', 'Erro');
        },
        () => {
          //this.spinner.hide();
        }
      ).add(() => {this.spinner.hide()});

    }

  }

}
