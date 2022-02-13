import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, FormArray, AbstractControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { Setor } from '@app/models/Setor';
import { SetorService } from '@app/services/setor.service';
import { Subsetor } from '@app/models/Subsetor';
import { SubsetorService } from '@app/services/subsetor.service';


@Component({
  selector: 'app-setor-detalhe',
  templateUrl: './setor-detalhe.component.html',
  styleUrls: ['./setor-detalhe.component.scss']
})
export class SetorDetalheComponent implements OnInit {

  modalRef: BsModalRef;

  setorId: number;

  setor = {} as Setor;

  form!: FormGroup;

  estadoSalvar = 'post';

  subsetorAtual = {id: 0, descricao: '', indice: 0};

  get f(): any {
    return this.form.controls;
  }

  get subsetores(): FormArray {
    return this.form.get('subsetores') as FormArray;
  }

  get modoEditar(): boolean {
    return this.estadoSalvar === 'put';
  }

  constructor(private activatedRouter: ActivatedRoute,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService,
              private fb: FormBuilder,
              private setorService: SetorService,
              private subsetorService: SubsetorService,
              private router: Router,
              private modalService: BsModalService) { }

  public carregarSetor(): void {
    this.setorId = +this.activatedRouter.snapshot.paramMap.get('id')!;

    if (this.setorId !== null || this.setorId === 0) {
      this.spinner.show();

      this.estadoSalvar = 'put';

      this.setorService.getSetorById(this.setorId).subscribe({
        next: (_setor: Setor) => {
          this.setor = {..._setor};
          this.form.patchValue(this.setor);
          this.setor.subsetores.forEach(subsetor => {
            this.subsetores.push(this.criarSubsetor(subsetor));
          });
          //this.carregarSubsetores();
        },
        error: (error: any) => {
          this.toastr.error('Erro ao tentar carregar o setor.', 'Erro!');
          console.error(error);
        }
      }).add(() => this.spinner.hide());

    }
  }

  /* public carregarSubsetores(): void {
    this.subsetorService.getSubsetoresBySetorId(this.setorId).subscribe(
      (subsetoresRetorno: Subsetor[]) => {
        subsetoresRetorno.forEach(subsetor => {
          this.subsetores.push(this.criarSubsetor(subsetor));
        });
      },
      (error: any) => {
        this.toastr.error('Erro ao tentar carregar os subsetores.', 'Erro');
        console.error(error);
      }
    ).add(() => this.spinner.hide());
  } */

  ngOnInit(): void {
    this.carregarSetor();
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      subsetores: this.fb.array([]),
    })
  }

  public adicionarSubsetor(): void {
    this.subsetores.push(this.criarSubsetor({id: 0} as Subsetor));
  }

  public criarSubsetor(subsetor: Subsetor): FormGroup {
    return this.fb.group({
      id: [subsetor.id],
      descricao: [subsetor.descricao, [Validators.required, Validators.minLength(3), Validators.maxLength(100)]]
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public salvarSetor(): void {
    this.spinner.show();

    if (this.form.valid) {
      this.setor = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.setor.id, ...this.form.value};

      this.setorService[this.estadoSalvar](this.setor).subscribe(
        (setorRetorno: Setor) => {
          this.toastr.success('Setor salvo com sucesso!', 'Sucesso');
          this.router.navigate([`setores/detalhe/${setorRetorno.id}`]);
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

  public salvarSubsetores(): void {
    this.spinner.show();

    if (this.form.get('subsetores').valid) {
      this.subsetorService.put(this.setorId, this.form.value.subsetores)
        .subscribe(
          () => {
            this.toastr.success('Subsetores salvos com sucesso!', 'Sucesso"');
            //this.subsetores.reset();
          },
          (error: any) => {
            this.toastr.error('Erro ao tentar salvar subsetores.', 'Erro');
            console.error(error);
          }
        ).add(() => this.spinner.hide());
    }

  }

  public removerSubsetor(template: TemplateRef<any>,
                         indice: number): void {
    this.subsetorAtual.id = this.subsetores.get(indice + '.id').value;
    this.subsetorAtual.descricao = this.subsetores.get(indice + '.descricao').value;
    this.subsetorAtual.indice = indice;

    this.modalRef = this.modalService.show(template, {class:'modal-sm'})
    //this.subsetores.removeAt(indice);
  }

  public confirmaDeletarSubsetor(): void {
    this.modalRef.hide();
    this.spinner.show();

    this.subsetorService.deleteSubsetor(this.setorId, this.subsetorAtual.id)
      .subscribe(
        () => {
          this.toastr.success('Subsetor deletado com sucesso.', 'Sucesso');
          this.subsetores.removeAt(this.subsetorAtual.indice);
        },
        (error: any) => {
          this.toastr.error(`Erro ao deletar o subsetor com ID: ${this.subsetorAtual.id}`, 'Erro');
          console.error(error);
        }
      ).add(() => this.spinner.hide());
  }

  public CancelarDelecaoSubsetor(): void {
    this.modalRef.hide();
  }



}
