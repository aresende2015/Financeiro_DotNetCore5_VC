import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AdministradorDeFundoImobiliario } from '@app/models/AdministradorDeFundoImobiliario';
import { FundoImobiliario } from '@app/models/FundoImobiliario';
import { SegmentoAnbima } from '@app/models/SegmentoAnbima';
import { TipoDeInvestimento } from '@app/models/TipoDeInvestimento';
import { AdministradordefundoimobiliarioService } from '@app/services/administradordefundoimobiliario.service';
import { FundoimobiliarioService } from '@app/services/fundoimobiliario.service';
import { SegmentoanbimaService } from '@app/services/segmentoanbima.service';
import { TipodeinvestimentoService } from '@app/services/tipodeinvestimento.service';
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
  public tiposDeInvestimentos: TipoDeInvestimento[] = [];
  public segmentosAnbimas: SegmentoAnbima[] = [];
  public administradoresDeFundosImobiliarios: AdministradorDeFundoImobiliario[] = [];

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
              private tipodeinvestimentoService: TipodeinvestimentoService,
              private segmentoAnbimaService: SegmentoanbimaService,
              private administradorDeFundoImobiliarioService: AdministradordefundoimobiliarioService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService)
  {
    this.localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.validation();
    this.carregarFundoImobiliario();
    this.carregarTiposDeInvestimentos();
    this.carregarSegmentosAnbimas();
    this.carregarAdministradoresDeFundosImobiliarios();
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

  public carregarTiposDeInvestimentos(): void {
    const observer = {
      next: (_tiposDeInvestimentos: TipoDeInvestimento[]) => {
        this.tiposDeInvestimentos = _tiposDeInvestimentos;
      },
      error: (error: any) => {
        this.spinner.hide();
        console.error(error);
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.tipodeinvestimentoService.getAllTiposDeInvestimentos().subscribe(observer);
  }

  public carregarSegmentosAnbimas(): void {
    const observer = {
      next: (_segmentosAnbimas: SegmentoAnbima[]) => {
        this.segmentosAnbimas = _segmentosAnbimas;
      },
      error: (error: any) => {
        this.spinner.hide();
        console.error(error);
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.segmentoAnbimaService.getAllSegmentosAnbimas().subscribe(observer);
  }

  public carregarAdministradoresDeFundosImobiliarios(): void {
    const observer = {
      next: (_administradoresDeFundosImobiliarios: AdministradorDeFundoImobiliario[]) => {
        this.administradoresDeFundosImobiliarios = _administradoresDeFundosImobiliarios;
      },
      error: (error: any) => {
        this.spinner.hide();
        console.error(error);
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.administradorDeFundoImobiliarioService.getAllAdministradoresDeFundosImobiliarios().subscribe(observer);
  }

  public validation(): void {
    this.form = this.fb.group({
      cnpj: ['', [Validators.required, Validators.minLength(14), Validators.maxLength(14)]],
      razaoSocial: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      descricao: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      dataDeInicio: ['', Validators.required],
      dataDeFim: ['', Validators.required],
      tipoDeInvestimentoId: [null, [Validators.required]],
      segmentoAnbimaId: [null, [Validators.required]],
      administradorDeFundoImobiliarioId: [null, [Validators.required]]
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
          this.toastr.success('Fundo Imobiliário salvo com sucesso!', 'Sucesso');
        },
        (error: any) => {
          //console.error(error);
          this.toastr.error('Erro ao atualizar fundo imobiliário', 'Erro');
        },
        () => {
          //this.spinner.hide();
        }
      ).add(() => {this.spinner.hide()});

    }

  }

}
