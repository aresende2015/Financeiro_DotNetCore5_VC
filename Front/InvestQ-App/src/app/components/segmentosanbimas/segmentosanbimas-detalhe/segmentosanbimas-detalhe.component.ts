import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SegmentoAnbima } from '@app/models/SegmentoAnbima';
import { SegmentoanbimaService } from '@app/services/segmentoanbima.service';
import { Guid } from 'guid-typescript';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-segmentosanbimas-detalhe',
  templateUrl: './segmentosanbimas-detalhe.component.html',
  styleUrls: ['./segmentosanbimas-detalhe.component.scss']
})
export class SegmentosanbimasDetalheComponent implements OnInit {

  segmentoAnbima = {} as SegmentoAnbima;

  segmentoAnbimaId: Guid;

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
              private segmentoanbimaService: SegmentoanbimaService,
              private spinner: NgxSpinnerService,
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.carregarSegmentoAnbima();
    this.validation();
  }

  public carregarSegmentoAnbima(): void {

    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      this.segmentoAnbimaId = Guid.createEmpty();
    else {
      this.segmentoAnbimaId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

      if (this.segmentoAnbimaId !== null && !this.segmentoAnbimaId.isEmpty()) {
        this.spinner.show();

        this.estadoSalvar = 'put';

        this.segmentoanbimaService.getSegmentoAnbimaById(this.segmentoAnbimaId).subscribe({
          next: (_segmentoAnbima: SegmentoAnbima) => {
            this.segmentoAnbima = {..._segmentoAnbima};
            this.form.patchValue(this.segmentoAnbima);
          },
          error: (error: any) => {
            this.spinner.hide();
            this.toastr.error('Erro ao tentar carregar o Segmento Anbima.', 'Erro!');
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

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public salvarAlteracao(): void {
    this.spinner.show();

    if (this.form.valid) {

      this.segmentoAnbima = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.segmentoAnbima.id, ...this.form.value};

      this.segmentoanbimaService[this.estadoSalvar](this.segmentoAnbima).subscribe(
        (_segmentoAnbima: SegmentoAnbima) => {
          //this.spinner.hide();
          this.toastr.success('Segmento Anbima salvo com sucesso!', 'Sucesso');
          this.router.navigate([`segmentosanbimas/detalhe/${_segmentoAnbima.id}`]);
        },
        (error: any) => {
          console.error(error);
          //this.spinner.hide();
          this.toastr.error('Erro ao atualizar o segmento anbima', 'Erro');
        },
        () => {
          //this.spinner.hide();
        }
      ).add(() => {this.spinner.hide()});

    }

  }

}
