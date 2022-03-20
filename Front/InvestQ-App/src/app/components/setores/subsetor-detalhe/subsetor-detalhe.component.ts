import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Segmento } from '@app/models/Segmento';
import { Subsetor } from '@app/models/Subsetor';
import { SegmentoService } from '@app/services/segmento.service';
import { SubsetorService } from '@app/services/subsetor.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-subsetor-detalhe',
  templateUrl: './subsetor-detalhe.component.html',
  styleUrls: ['./subsetor-detalhe.component.scss']
})
export class SubsetorDetalheComponent implements OnInit {

  modalRef: BsModalRef;

  subsetorId: Guid;

  subsetor = {} as Subsetor;

  form!: FormGroup;

  estadoSalvar = 'post';

  segmentoAtual = {id: Guid.createEmpty(), descricao: '', indice: 0};

  get f(): any {
    return this.form.controls;
  }

  get segmentos(): FormArray {
    return this.form.get('segmentos') as FormArray;
  }

  get modoEditar(): boolean {
    return this.estadoSalvar === 'put';
  }

  constructor(private activatedRouter: ActivatedRoute,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService,
              private subsetorService: SubsetorService,
              private segmentoService: SegmentoService,
              private fb: FormBuilder,
              private router: Router,
              private modalService: BsModalService) { }

  public carregarSubsetor(): void {

    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      this.subsetorId = Guid.createEmpty();
    else {
      this.subsetorId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());
      //this.subsetorId = +this.activatedRouter.snapshot.paramMap.get('id')!;

      if (this.subsetorId !== null && !this.subsetorId.isEmpty()) {
        this.spinner.show();

        this.estadoSalvar = 'put';

        this.subsetorService.getSubsetorById(this.subsetorId).subscribe(
          (_subsetor: Subsetor) => {
            this.subsetor = {..._subsetor};
            this.form.patchValue(this.subsetor);
            this.subsetor.segmentos.forEach(segmento => {
              this.segmentos.push(this.criarSegmento(segmento));
            })
          },
          (error: any) => {
            this.toastr.error('Erro ao tentar carregar o subsetor.', 'Erro!');
            console.error(error);
          }
        ).add(() => this.spinner.hide());
      }
    }

  }

  ngOnInit(): void {
    this.carregarSubsetor();
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      segmentos: this.fb.array([]),
    })
  }

  public adicionarSegmento(): void {
    this.segmentos.push(this.criarSegmento({id: Guid.createEmpty()} as Segmento));
  }

  public criarSegmento(segmento: Segmento): FormGroup {
    return this.fb.group({
      id: [segmento.id.toString()],
      descricao: [segmento.descricao, [Validators.required, Validators.minLength(3), Validators.maxLength(100)]]
    });
  }

  public resetFormSegmento(): void {
    this.form.get('segmentos').reset();
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public salvarSegmentos(): void {

    if (this.form.get('segmentos').valid) {
      this.spinner.show();

      this.segmentoService.putSegmentos(this.subsetorId, this.form.value.segmentos)
        .subscribe(
          () => {
            this.toastr.success('Segmentos salvos com sucesso!', 'Sucesso"');
            this.router.navigate([`setores/listarsubsetores/${this.subsetor.setorId}`]);
            //this.subsetores.reset();
          },
          (error: any) => {
            this.toastr.error('Erro ao tentar salvar segmentos.', 'Erro');
            console.error(error);
          }
        ).add(() => this.spinner.hide());
    }

  }

  public removerSegmento(template: TemplateRef<any>,
    indice: number): void {
    this.segmentoAtual.id = this.segmentos.get(indice + '.id').value;
    this.segmentoAtual.descricao = this.segmentos.get(indice + '.descricao').value;
    this.segmentoAtual.indice = indice;

    this.modalRef = this.modalService.show(template, {class:'modal-sm'})
    //this.subsetores.removeAt(indice);
  }

  public confirmaDeletarSegmento(): void {
    this.modalRef.hide();
    this.spinner.show();

    this.segmentoService.deleteSegmento(this.subsetorId, this.segmentoAtual.id)
      .subscribe(
        () => {
          this.toastr.success('Segmento deletado com sucesso.', 'Sucesso');
          this.segmentos.removeAt(this.segmentoAtual.indice);
        },
        (error: any) => {
          this.toastr.error(`Erro ao deletar o segmento com ID: ${this.segmentoAtual.id}`, 'Erro');
          console.error(error);
        }
      ).add(() => this.spinner.hide());
  }

  public CancelarDelecaoSegmento(): void {
    this.modalRef.hide();
  }

}
