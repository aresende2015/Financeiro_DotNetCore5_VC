import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '@app/helpers/ValidatorField';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from '@app/services/user.service';
import { Router } from '@angular/router';
import { UserUpdate } from '@app/models/identity/UserUpdate';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  userUpdate = {} as UserUpdate;

  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router,
    private toaster: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.validation();
    this.carregarUsuario();
  }

  public validation(): void {
    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password', 'confirmarPassword')
    };

    this.form = this.fb.group({
      username: [''],
      primeiroNome: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      ultimoNome: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required]],
      funcao: ['NaoInformado', Validators.required],
      password: ['', [Validators.nullValidator, Validators.minLength(8), Validators.maxLength(20)]],
      confirmarPassword: ['', [Validators.nullValidator]]
    }, formOptions);
  }

  public cssValidator(campoForm: FormControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public resetForm(event: any): void {
    event.preventDefault();
    this.form.reset();
  }

  onSubmit(): void {
    this.atualizarUsuario();
  }

  public atualizarUsuario(): void {
    this.userUpdate = { ...this.form.value };
    this.spinner.show();
    this.userService.UpdateUser(this.userUpdate).subscribe(
      () => this.toaster.success('Usuário atualizado!', 'Sucesso'),
      (error: any) => {
        this.toaster.error(error.error);
        console.error(error);
      }
    ).add(() => this.spinner.hide());

  }

  private carregarUsuario(): void {
    this.spinner.show();
    this.userService
      .getUser()
      .subscribe(
        (userRetorno: UserUpdate) => {
          console.log(userRetorno);
          this.userUpdate = userRetorno;
          this.form.patchValue(this.userUpdate);
          this.toaster.success('Usuário Carregado', 'Sucesso');
        },
        (error) => {
          console.error(error);
          this.spinner.hide();
          this.toaster.error('Usuário não Carregado', 'Erro');
          this.router.navigate(['/dashboard']);
        },
        () => {
          this.spinner.hide()
        }
      );
  }

}
