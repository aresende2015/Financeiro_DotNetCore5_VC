import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Segmento } from '@app/models/Segmento';
import { SegmentoService } from '@app/services/segmento.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-segmento-detalhe',
  templateUrl: './segmento-detalhe.component.html',
  styleUrls: ['./segmento-detalhe.component.scss']
})
export class SegmentoDetalheComponent implements OnInit {

  form!: FormGroup;

  segmentoId: number;
  subsetorId: number;

  segmento = {} as Segmento;

  estadoSalvar = 'post';

  get f(): any {
    return this.form.controls;
  }

  constructor(private spinner: NgxSpinnerService,
              private activatedRouter: ActivatedRoute,
              private toastr: ToastrService,
              private segmentoService: SegmentoService,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.carregarSegmento();
    this.validation();
  }

  public carregarSegmento(): void {
    this.segmentoId = +this.activatedRouter.snapshot.paramMap.get('id')!;
    this.subsetorId = +this.activatedRouter.snapshot.paramMap.get('subsetorId')!;

    if (this.segmentoId !== null && this.segmentoId !== 0) {
      this.spinner.show();

      this.estadoSalvar = 'put';

      this.segmentoService.getSegmentoBySubsetorIdSegmentoId(this.subsetorId, this.segmentoId).subscribe({
        next: (_segmento: Segmento) => {
          this.segmento = {..._segmento};
          this.form.patchValue(this.segmento);
        },
        error: (error: any) => {
          this.toastr.error('Erro ao tentar carregar o segmento.', 'Erro!');
          console.error(error);
        }
      }).add(() => this.spinner.hide());

    }
  }

  public validation(): void {
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]]
    })
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public resetForm(): void {
    this.form.reset();
  }

  public salvarSegmento(): void {

  }

}
