import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AdministradorDeFundoImobiliario } from '@app/models/AdministradorDeFundoImobiliario';
import { AdministradordefundoimobiliarioService } from '@app/services/administradordefundoimobiliario.service';
import { Guid } from 'guid-typescript';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-administradoresdefundosimobiliarios-detalhe',
  templateUrl: './administradoresdefundosimobiliarios-detalhe.component.html',
  styleUrls: ['./administradoresdefundosimobiliarios-detalhe.component.scss']
})
export class AdministradoresdefundosimobiliariosDetalheComponent implements OnInit {

  administradorDeFundoImobiliairo = {} as AdministradorDeFundoImobiliario;

  administradorDeFundoImobiliairoId: Guid;

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
              private administradordefundoimobiliarioService: AdministradordefundoimobiliarioService,
              private spinner: NgxSpinnerService,
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.carregarAdministradorDeFundoImobiliario();
    this.validation();
  }

  public carregarAdministradorDeFundoImobiliario(): void {

    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      this.administradorDeFundoImobiliairoId = Guid.createEmpty();
    else {
      this.administradorDeFundoImobiliairoId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

      if (this.administradorDeFundoImobiliairoId !== null && !this.administradorDeFundoImobiliairoId.isEmpty()) {
        this.spinner.show();

        this.estadoSalvar = 'put';

        this.administradordefundoimobiliarioService.getAdministradorDeFundoImobiliarioById(this.administradorDeFundoImobiliairoId).subscribe({
          next: (_administradorDeFundoImobiliairo: AdministradorDeFundoImobiliario) => {
            this.administradorDeFundoImobiliairo = {..._administradorDeFundoImobiliairo};
            this.form.patchValue(this.administradorDeFundoImobiliairo);
          },
          error: (error: any) => {
            this.spinner.hide();
            this.toastr.error('Erro ao tentar carregar o Administrador de Fundo Imobiliario.', 'Erro!');
            console.error(error)
          },
          complete: () => {this.spinner.hide()}
        });
      }
    }
  }

  public validation(): void {
    this.form = this.fb.group({
      cnpj: ['', [Validators.required, Validators.minLength(14), Validators.maxLength(14)]],
      razaoSocial: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      telefone: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(11)]],
      email: ['', [Validators.required, Validators.email]],
      site: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]]
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

      this.administradorDeFundoImobiliairo = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.administradorDeFundoImobiliairo.id, ...this.form.value};

      this.administradordefundoimobiliarioService[this.estadoSalvar](this.administradorDeFundoImobiliairo).subscribe(
        (_administradorDeFundoImobiliairo: AdministradorDeFundoImobiliario) => {
          //this.spinner.hide();
          this.toastr.success('Administrador de Fundo Imobiliario salvo com sucesso!', 'Sucesso');
          this.router.navigate([`administradoresdefundosimobiliarios/detalhe/${_administradorDeFundoImobiliairo.id}`]);
        },
        (error: any) => {
          console.error(error);
          //this.spinner.hide();
          this.toastr.error('Erro ao atualizar o administrador de fundo imobiliário.', 'Erro');
        },
        () => {
          //this.spinner.hide();
        }
      ).add(() => {this.spinner.hide()});

    }

  }

}
