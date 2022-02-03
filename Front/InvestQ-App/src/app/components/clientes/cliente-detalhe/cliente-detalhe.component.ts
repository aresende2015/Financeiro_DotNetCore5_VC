import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Cliente } from '@app/models/Cliente';
import { ClienteService } from '@app/services/cliente.service';
@Component({
  selector: 'app-cliente-detalhe',
  templateUrl: './cliente-detalhe.component.html',
  styleUrls: ['./cliente-detalhe.component.scss']
})
export class ClienteDetalheComponent implements OnInit {

  cliente = {} as Cliente;
  //cliente: Cliente;

  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'YYYY/MM/DD hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false
    }
  }

  constructor(private fb: FormBuilder,
              private localeService: BsLocaleService,
              private router: ActivatedRoute,
              private clienteService: ClienteService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService)
  {
    this.localeService.use('pt-br');
  }

  public carregarCliente(): void {
    const clienteIdParam = this.router.snapshot.paramMap.get('id');

    if (clienteIdParam !== null) {
      this.spinner.show();
      this.clienteService.getClienteById(+clienteIdParam).subscribe({
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

}
