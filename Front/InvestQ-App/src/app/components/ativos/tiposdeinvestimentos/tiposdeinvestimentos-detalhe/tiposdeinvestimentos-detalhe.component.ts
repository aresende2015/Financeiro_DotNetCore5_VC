import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TipodeinvestimentoService } from '@app/services/tipodeinvestimento.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { TipoDeInvestimento } from '@app/models/TipoDeInvestimento';
import { Guid } from 'guid-typescript';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-tiposdeinvestimentos-detalhe',
  templateUrl: './tiposdeinvestimentos-detalhe.component.html',
  styleUrls: ['./tiposdeinvestimentos-detalhe.component.scss']
})
export class TiposdeinvestimentosDetalheComponent implements OnInit {

  tipoDeInvestimento = {} as TipoDeInvestimento;

  tipoDeInvestimentoId: Guid;

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
              private tipodeinvestimentoService: TipodeinvestimentoService,
              private spinner: NgxSpinnerService,
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.carregarTipoDeInvestimento();
    this.validation();
  }

  public carregarTipoDeInvestimento(): void {

    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      this.tipoDeInvestimentoId = Guid.createEmpty();
    else {
      this.tipoDeInvestimentoId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

      if (this.tipoDeInvestimentoId !== null && !this.tipoDeInvestimentoId.isEmpty()) {
        this.spinner.show();

        this.estadoSalvar = 'put';

        this.tipodeinvestimentoService.getTipoDeInvestimentoById(this.tipoDeInvestimentoId).subscribe({
          next: (_tipoDeInvestimento: TipoDeInvestimento) => {
            this.tipoDeInvestimento = {..._tipoDeInvestimento};
            this.form.patchValue(this.tipoDeInvestimento);
          },
          error: (error: any) => {
            this.spinner.hide();
            this.toastr.error('Erro ao tentar carregar o Tipo de Investimento.', 'Erro!');
            console.error(error)
          },
          complete: () => {this.spinner.hide()}
        });
      }
    }
  }

  public validation(): void {
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]]
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

      this.tipoDeInvestimento = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.tipoDeInvestimento.id, ...this.form.value};

      this.tipodeinvestimentoService[this.estadoSalvar](this.tipoDeInvestimento).subscribe(
        (_tipoDeInvestimento: TipoDeInvestimento) => {
          //this.spinner.hide();
          this.toastr.success('Tipo de investimento salvo com sucesso!', 'Sucesso');
          this.router.navigate([`tiposdeinvestimentos/detalhe/${_tipoDeInvestimento.id}`]);
        },
        (error: any) => {
          console.error(error);
          //this.spinner.hide();
          this.toastr.error('Erro ao atualizar o tipo de investimento', 'Erro');
        },
        () => {
          //this.spinner.hide();
        }
      ).add(() => {this.spinner.hide()});

    }

  }

}
