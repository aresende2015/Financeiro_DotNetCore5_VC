import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Setor } from '@app/models/Setor';
import { SetorService } from '@app/services/setor.service';
import { Subsetor } from './../../../models/Subsetor';


@Component({
  selector: 'app-setor-detalhe',
  templateUrl: './setor-detalhe.component.html',
  styleUrls: ['./setor-detalhe.component.scss']
})
export class SetorDetalheComponent implements OnInit {

  setor = {} as Setor;

  form!: FormGroup;

  estadoSalvar = 'post';

  get f(): any {
    return this.form.controls;
  }

  constructor(private router: ActivatedRoute,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService,
              private fb: FormBuilder,
              private setorService: SetorService) { }

  public carregarSetor(): void {
    const setorIdParam = this.router.snapshot.paramMap.get('id');

    if (setorIdParam !== null) {
      this.spinner.show();

      this.estadoSalvar = 'put';

      this.setorService.getSetorById(+setorIdParam).subscribe({
        next: (_setor: Setor) => {
          this.setor = {..._setor};
          this.form.patchValue(this.setor);
        },
        error: (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao tentar carregar o setor.', 'Erro!');
          console.error(error);
        },
        complete: () => {this.spinner.hide();}
      });

    }
  }

  ngOnInit(): void {
    this.carregarSetor();
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(200)]]
    })
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched}
  }

  public salvarAlteracao(): void {
    this.spinner.show();

    if (this.form.valid) {
      this.setor = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.setor.id, ...this.form.value};

      this.setorService[this.estadoSalvar](this.setor).subscribe(
        () => {
          this.toastr.success('Setor salvo com sucesso!', 'Sucesso');
        },
        (error: any) => {
          console.error(error);
          this.toastr.error('Erro ao salvar o sertor.', 'Erro');
        },
        () => {
          //this.spinner.hide();
        }
      ).add(() => {this.spinner.hide()});
    }
  }

}
