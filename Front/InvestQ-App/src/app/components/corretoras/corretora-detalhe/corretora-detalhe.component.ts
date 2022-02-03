import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
}
