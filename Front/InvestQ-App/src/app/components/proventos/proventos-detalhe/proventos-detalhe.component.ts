import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Provento } from '@app/models/Provento';
import { ProventoService } from '@app/services/provento.service';
import { Guid } from 'guid-typescript';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-proventos-detalhe',
  templateUrl: './proventos-detalhe.component.html',
  styleUrls: ['./proventos-detalhe.component.scss']
})
export class ProventosDetalheComponent implements OnInit {

  public provento = {} as Provento;
  public proventos: Provento[] = [];

  proventoId: Guid;
  form!: FormGroup;

  jurosSemestraisOp: any[];

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

  get modoEditar(): boolean {
    return this.estadoSalvar === 'put';
  }

  constructor(private fb: FormBuilder,
              private activatedRouter: ActivatedRoute,
              private proventoService: ProventoService,
              //private tipodeinvestimentoService: TipodeinvestimentoService,
              private spinner: NgxSpinnerService,
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.carregarProvento();
    //this.carregarTiposDeInvestimentos();
    this.validation();
    //this.jurosSemestraisOp = this.proventoService.getJurosSemestrais();
  }

  // public carregarProventos(): void {
  //   const observer = {
  //     next: (_proventos: Provento[]) => {
  //       this.proventos = _proventos;
  //     },
  //     error: (error: any) => {
  //       this.spinner.hide();
  //       console.error(error);
  //       this.toastr.error('Erro ao carregar a tela...', 'Error"');
  //     },
  //     complete: () => {this.spinner.hide()}
  //   }

  //   this.tipodeinvestimentoService.getAllTiposDeInvestimentos().subscribe(observer);
  // }

  public carregarProvento(): void {

    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      this.proventoId = Guid.createEmpty();
    else {
      this.proventoId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

      if (this.proventoId !== null && !this.proventoId.isEmpty()) {
        this.spinner.show();

        this.estadoSalvar = 'put';

        this.proventoService.getProventosById(this.proventoId).subscribe({
          next: (_provento: Provento) => {
            this.provento = {..._provento};
            this.form.patchValue(this.provento);
          },
          error: (error: any) => {
            this.spinner.hide();
            this.toastr.error('Erro ao tentar carregar o Provento.', 'Erro!');
            console.error(error);
          },
          complete: () => {this.spinner.hide()}
        });
      }
    }
  }

  public validation(): void {
    this.form = this.fb.group({
      dataCom: ['', Validators.required],
      dataEx: ['', Validators.required],
      valor: ['', [Validators.required]]
      //jurosSemestrais: [false, [Validators.required]],
      //tipoDeInvestimentoId: [null, [Validators.required]],
      //dataDeVencimento: ['', Validators.required]
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

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public salvarAlteracao(): void {
    this.spinner.show();

    if (this.form.valid) {

      this.provento = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.provento.id, ...this.form.value};

      this.proventoService[this.estadoSalvar](this.provento).subscribe(
        (_provento: Provento) => {

          this.toastr.success('Provento salvo com sucesso!', 'Sucesso');
          this.router.navigate([`proventos/detalhe/${_provento.id}`]);
        },
        (error: any) => {
          console.error(error);
          //this.spinner.hide();
          this.toastr.error('Erro ao atualizar o provento', 'Erro');
        },
        () => {
          //this.spinner.hide();
        }
      ).add(() => {this.spinner.hide()});

    }

  }

}
