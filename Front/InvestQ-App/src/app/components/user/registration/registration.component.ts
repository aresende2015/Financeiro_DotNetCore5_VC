import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ValidatorField } from '@app/helpers/ValidatorField';
import { TipoDeUsuario } from '@app/models/Enum/TipoDeUsuario.enum';
import { User } from '@app/models/identity/User';
import { UserService } from '@app/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Subsetor } from './../../../models/Subsetor';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  user = {} as User;

  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  constructor(private fb: FormBuilder,
              private userService: UserService,
              private router: Router,
              private toaster: ToastrService) { }

  ngOnInit(): void {
    this.validation();
  }

  public validation(): void {
    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password', 'confirmarPassword')
    };

    this.form = this.fb.group({
      primeiroNome: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      ultimoNome: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      email: ['', [Validators.required, Validators.email]],
      funcao: [''],
      username: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20)]],
      confirmarPassword: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20)]]
    }, formOptions);
  }

  public register(): void {
    this.user = { ...this.form.value};
    this.user.funcao = TipoDeUsuario.Usuario;
    this.userService.register(this.user).subscribe(
      () => this.router.navigateByUrl('/dashboard'),
      (error: any) => this.toaster.error(error.error)
    )
  }

}
