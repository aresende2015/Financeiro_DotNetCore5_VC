import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { FundoImobiliario } from '@app/models/FundoImobiliario';
import { FundoimobiliarioService } from '@app/services/fundoimobiliario.service';
import { Guid } from 'guid-typescript';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-fundosimobiliarios-detalhe',
  templateUrl: './fundosimobiliarios-detalhe.component.html',
  styleUrls: ['./fundosimobiliarios-detalhe.component.scss']
})
export class FundosimobiliariosDetalheComponent implements OnInit {

  fundoImobiliario = {} as FundoImobiliario;
  //cliente: Cliente;
  fundoImobiliarioId: Guid;

  form!: FormGroup;

  estadoSalvar = 'post';

  get f(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY',
      containerClass: 'theme-default',
      showWeekNumbers: false
    }
  }

  constructor(private fb: FormBuilder,
              private localeService: BsLocaleService,
              private router: ActivatedRoute,
              private activatedRouter: ActivatedRoute,
              private fundoImobiliarioService: FundoimobiliarioService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService)
  {
    this.localeService.use('pt-br');
  }

  public carregarFundoImobiliario(): void {
    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      this.fundoImobiliarioId = Guid.createEmpty();
    else {
      this.fundoImobiliarioId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

      if (this.fundoImobiliarioId !== null && !this.fundoImobiliarioId.isEmpty()) {

        this.spinner.show();

        this.estadoSalvar = 'put';

        this.fundoImobiliarioService.getFundoImobiliarioById(this.fundoImobiliarioId).subscribe({
          next: (fundoImobiliario: FundoImobiliario) => {
            this.fundoImobiliario = {...fundoImobiliario};
            this.form.patchValue(this.fundoImobiliario);
          },
          error: (error: any) => {
            this.spinner.hide();
            this.toastr.error('Erro ao tentar carregar o fundo imobiliario.', 'Erro!');
            console.error(error)
          },
          complete: () => {this.spinner.hide()}
        });
      }
    }
  }

  ngOnInit(): void {
    this.validation();
    this.carregarFundoImobiliario();
  }

  public validation(): void {
    this.form = this.fb.group({
      cnpj: ['', [Validators.required, Validators.minLength(14), Validators.maxLength(14)]],
      razaoSocial: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      descricao: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      dataInicio: ['', Validators.required],
      dataFim: ['', Validators.required]
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public onClicouEm(evento) {
    if (evento.botaoClicado === 'cancelar') {
      this.resetForm();
    } else {
      this.salvarAlteracao();
    }
  }

  public cssValidator(campoForm: FormControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public salvarAlteracao(): void {
    this.spinner.show();
    if (this.form.valid) {

      //if (this.estadoSalvar === 'post') {
      //  this.cliente = {...this.form.value};
      //} else  {
      //  this.cliente = {id: this.cliente.id, ...this.form.value};
      //}

      this.fundoImobiliario = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.fundoImobiliario.id, ...this.form.value};

      this.fundoImobiliarioService[this.estadoSalvar](this.fundoImobiliario).subscribe(
        () => {
          //this.spinner.hide();
          this.toastr.success('Fundo Imobiliário salvo com sucesso!', 'Sucesso');
        },
        (error: any) => {
          console.error(error);
          //this.spinner.hide();
          this.toastr.error('Erro ao atualizar fundo imobiliário', 'Erro');
        },
        () => {
          //this.spinner.hide();
        }
      ).add(() => {this.spinner.hide()});

    }

  }

}
