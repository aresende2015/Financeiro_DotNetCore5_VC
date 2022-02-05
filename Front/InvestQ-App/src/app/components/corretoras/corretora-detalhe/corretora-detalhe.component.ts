import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { CorretoraService } from '@app/services/corretora.service';
import { Corretora } from '@app/models/Corretora';


@Component({
  selector: 'app-corretora-detalhe',
  templateUrl: './corretora-detalhe.component.html',
  styleUrls: ['./corretora-detalhe.component.scss']
})
export class CorretoraDetalheComponent implements OnInit {

  corretora = {} as Corretora;

  form!: FormGroup;

  estadoSalvar = 'post';

  get f(): any {
    return this.form.controls;
  }

  constructor(private fb: FormBuilder,
              private router: ActivatedRoute,
              private corretoraService: CorretoraService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService) {

  }

  public carregarCorretora(): void {
    const corretoraIdParam = this.router.snapshot.paramMap.get('id');

    if (corretoraIdParam !== null) {
      this.spinner.show();

      this.estadoSalvar = 'put';

      this.corretoraService.getCorretoraById(+corretoraIdParam).subscribe({
        next: (corretora: Corretora) => {
          this.corretora = {...corretora};
          this.form.patchValue(this.corretora);
        },
        error: (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao tentar carregar a corretora.', 'Erro!');
          console.error(error)
        },
        complete: () => {this.spinner.hide()}
      });
    }
  }

  ngOnInit(): void {
    this.carregarCorretora();
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      imagen: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]]
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

      this.corretora = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.corretora.id, ...this.form.value};

      this.corretoraService[this.estadoSalvar](this.corretora).subscribe(
        () => {
          //this.spinner.hide();
          this.toastr.success('Corretora salva com sucesso!', 'Sucesso');
        },
        (error: any) => {
          console.error(error);
          //this.spinner.hide();
          this.toastr.error('Erro ao atualizar corretora', 'Erro');
        },
        () => {
          //this.spinner.hide();
        }
      ).add(() => {this.spinner.hide()});

    }

  }
}
