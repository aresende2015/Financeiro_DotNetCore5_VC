import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Ativo } from '@app/models/Ativo';
import { Provento } from '@app/models/Provento';
import { AtivoService } from '@app/services/ativo.service';
import { ProventoService } from '@app/services/provento.service';
import { Guid } from 'guid-typescript';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { TipoDeAtivo } from '@app/models/Enum/TipoDeAtivo.enum';

@Component({
  selector: 'app-proventos-detalhe',
  templateUrl: './proventos-detalhe.component.html',
  styleUrls: ['./proventos-detalhe.component.scss']
})
export class ProventosDetalheComponent implements OnInit {

  public provento = {} as Provento;
  public proventos: Provento[] = [];
  public ativos: Ativo[] = [];

  proventoId: Guid;
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
              private proventoService: ProventoService,
              private ativoService: AtivoService,
              private spinner: NgxSpinnerService,
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.carregarProvento();
    this.validation();
    this.tipoDeMovimentacaoOp = this.proventoService.getTipoDeMovimentacao();
    this.tipoDeAtivoOp = this.proventoService.getTipoDeAtivo();
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

  public carregarProvento(): void {

    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      this.proventoId = Guid.createEmpty();
    else {
      this.proventoId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

      if (this.proventoId !== null && !this.proventoId.isEmpty()) {
        this.spinner.show();

        this.estadoSalvar = 'put';

        this.proventoService.getProventosById(this.proventoId).subscribe({
          next: (_provento: Provento) => {
            this.provento = {..._provento};
            this.provento.tipoDeAtivo = this.provento.ativo.tipoDeAtivo;
            this.carregarAtivos(this.provento.tipoDeAtivo);
            this.form.patchValue(this.provento);
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
      dataCom: ['', Validators.required],
      dataEx: ['', Validators.required],
      valor: ['', [Validators.required]],
      tipoDeMovimentacao: [false, [Validators.required]],
      tipoDeAtivo: [false, [Validators.required]],
      ativoId: [null, [Validators.required]]
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

      this.provento = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.provento.id, ...this.form.value};

      this.proventoService[this.estadoSalvar](this.provento).subscribe(
        (_provento: Provento) => {

          this.toastr.success('Provento salvo com sucesso!', 'Sucesso');
          this.router.navigate([`proventos/detalhe/${_provento.id}`]);
        },
        (error: any) => {
          console.error(error);
          //this.spinner.hide();
          this.toastr.error('Erro ao atualizar o provento', 'Erro');
        },
        () => {
          //this.spinner.hide();
        }
      ).add(() => {this.spinner.hide()});

    }

  }

}
