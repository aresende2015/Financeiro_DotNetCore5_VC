import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { CorretoraService } from '@app/services/corretora.service';
import { Corretora } from '@app/models/Corretora';
import { environment } from '@environments/environment';
import { Guid } from 'guid-typescript';


@Component({
  selector: 'app-corretora-detalhe',
  templateUrl: './corretora-detalhe.component.html',
  styleUrls: ['./corretora-detalhe.component.scss']
})
export class CorretoraDetalheComponent implements OnInit {

  corretora = {} as Corretora;

  imagemURL = 'assets/upload.png';

  corretoraId: Guid;

  file: File;

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
              private corretoraService: CorretoraService,
              private spinner: NgxSpinnerService,
              private router: Router,
              private toastr: ToastrService) {

  }

  public carregarCorretora(): void {

    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      this.corretoraId = Guid.createEmpty();
    else {
      this.corretoraId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

      if (this.corretoraId !== null && !this.corretoraId.isEmpty()) {
        this.spinner.show();

        this.estadoSalvar = 'put';

        this.corretoraService.getCorretoraById(this.corretoraId).subscribe({
          next: (corretora: Corretora) => {
            this.corretora = {...corretora};
            this.form.patchValue(this.corretora);
            if (this.corretora.imagen !== '') {
              this.imagemURL = environment.apiURL + 'resources/images/' + this.corretora.imagen;
            }
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
  }

  ngOnInit(): void {
    this.carregarCorretora();
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      imagen: ['']
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public onClicouEm(evento) {
    if (evento.botaoClicado === 'cancelar') {
      this.resetForm();
    } else {
      this.salvarAlteracao();
    }
  }

  public salvarAlteracao(): void {
    this.spinner.show();

    if (this.form.valid) {

      this.corretora = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.corretora.id, ...this.form.value};

      this.corretoraService[this.estadoSalvar](this.corretora).subscribe(
        (_corretora: Corretora) => {
          //this.spinner.hide();
          this.toastr.success('Corretora salva com sucesso!', 'Sucesso');
          this.router.navigate([`corretoras/detalhe/${_corretora.id}`]);
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

  onFileChange(ev: any): void {
    const reader = new FileReader();

    reader.onload = (event: any) => this.imagemURL = event.target.result;

    this.file = ev.target.files;

    reader.readAsDataURL(this.file[0]);

    this.uploadImagem();
  }

  uploadImagem(): void {
    this.spinner.show();
    this.corretoraService.postUpload(this.corretoraId, this.file).subscribe(
      () => {
        this.carregarCorretora();
        this.toastr.success('Imagen atualizada ocm sucesso.', 'Sucesso');
      },
      (error: any) => {
        this.toastr.error('Erro ao fazer upload de imagem.', 'Erro');
        console.log(error);
      }
    ).add(() => this.spinner.hide());
  }

}
