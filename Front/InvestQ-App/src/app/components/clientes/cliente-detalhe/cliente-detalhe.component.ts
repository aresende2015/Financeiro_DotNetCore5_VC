import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Cliente } from '@app/models/Cliente';
import { ClienteService } from '@app/services/cliente.service';
import { Guid } from 'guid-typescript';
@Component({
  selector: 'app-cliente-detalhe',
  templateUrl: './cliente-detalhe.component.html',
  styleUrls: ['./cliente-detalhe.component.scss']
})
export class ClienteDetalheComponent implements OnInit {

  cliente = {} as Cliente;
  //cliente: Cliente;
  clienteId: Guid;

  form!: FormGroup;

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

  constructor(private fb: FormBuilder,
              private localeService: BsLocaleService,
              private router: ActivatedRoute,
              private activatedRouter: ActivatedRoute,
              private clienteService: ClienteService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService)
  {
    this.localeService.use('pt-br');
  }

  public carregarCliente(): void {
    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      this.clienteId = Guid.createEmpty();
    else {
      this.clienteId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

      if (this.clienteId !== null && !this.clienteId.isEmpty()) {

        this.spinner.show();

        this.estadoSalvar = 'put';

        this.clienteService.getClienteById(this.clienteId).subscribe({
          next: (cliente: Cliente) => {
            this.cliente = {...cliente};
            this.form.patchValue(this.cliente);
          },
          error: (error: any) => {
            this.spinner.hide();
            this.toastr.error('Erro ao tentar carregar o cliente.', 'Erro!');
            console.error(error)
          },
          complete: () => {this.spinner.hide()}
        });
      }
    }
  }

  ngOnInit(): void {
    this.validation();
    this.carregarCliente();
  }

  public validation(): void {
    this.form = this.fb.group({
      cpf: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
      nome: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      sobreNome: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      email: ['', [Validators.required, Validators.email]],
      dataDeNascimento: ['', Validators.required]
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public salvarAlteracao(): void {
    this.spinner.show();
    if (this.form.valid) {

      //if (this.estadoSalvar === 'post') {
      //  this.cliente = {...this.form.value};
      //} else  {
      //  this.cliente = {id: this.cliente.id, ...this.form.value};
      //}

      this.cliente = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.cliente.id, ...this.form.value};

      this.clienteService[this.estadoSalvar](this.cliente).subscribe(
        () => {
          //this.spinner.hide();
          this.toastr.success('Cliente salvo com sucesso!', 'Sucesso');
        },
        (error: any) => {
          console.error(error);
          //this.spinner.hide();
          this.toastr.error('Erro ao atualizar cliente', 'Erro');
        },
        () => {
          //this.spinner.hide();
        }
      ).add(() => {this.spinner.hide()});

    }

  }

}
