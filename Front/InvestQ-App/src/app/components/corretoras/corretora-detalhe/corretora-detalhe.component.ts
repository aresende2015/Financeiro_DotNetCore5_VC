import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-corretora-detalhe',
  templateUrl: './corretora-detalhe.component.html',
  styleUrls: ['./corretora-detalhe.component.scss']
})
export class CorretoraDetalheComponent implements OnInit {

  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
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
