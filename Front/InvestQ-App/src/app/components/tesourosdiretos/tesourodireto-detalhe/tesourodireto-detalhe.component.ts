import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TesouroDireto } from '@app/models/TesouroDireto';
import { TipoDeInvestimento } from '@app/models/TipoDeInvestimento';
import { TesourodiretoService } from '@app/services/tesourodireto.service';
import { TipodeinvestimentoService } from '@app/services/tipodeinvestimento.service';
import { Guid } from 'guid-typescript';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-tesourodireto-detalhe',
  templateUrl: './tesourodireto-detalhe.component.html',
  styleUrls: ['./tesourodireto-detalhe.component.scss']
})
export class TesourodiretoDetalheComponent implements OnInit {

  public tesouroDireto = {} as TesouroDireto;
  public tiposDeInvestimentos: TipoDeInvestimento[] = [];

  tesouroDiretoId: Guid;
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
              private tesourodiretoService: TesourodiretoService,
              private tipodeinvestimentoService: TipodeinvestimentoService,
              private spinner: NgxSpinnerService,
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.carregarTesouroDireto();
    this.carregarTiposDeInvestimentos();
    this.validation();
    this.jurosSemestraisOp = this.tesourodiretoService.getJurosSemestrais();
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

  public carregarTesouroDireto(): void {

    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      this.tesouroDiretoId = Guid.createEmpty();
    else {
      this.tesouroDiretoId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

      if (this.tesouroDiretoId !== null && !this.tesouroDiretoId.isEmpty()) {
        this.spinner.show();

        this.estadoSalvar = 'put';

        this.tesourodiretoService.getTesouroDiretoById(this.tesouroDiretoId).subscribe({
          next: (_tesouroDireto: TesouroDireto) => {
            this.tesouroDireto = {..._tesouroDireto};
            this.form.patchValue(this.tesouroDireto);
          },
          error: (error: any) => {
            this.spinner.hide();
            this.toastr.error('Erro ao tentar carregar o Tesouro Direto.', 'Erro!');
            console.error(error);
          },
          complete: () => {this.spinner.hide()}
        });
      }
    }
  }

  public validation(): void {
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      jurosSemestrais: [false, [Validators.required]],
      tipoDeInvestimentoId: [null, [Validators.required]],
      dataDeVencimento: ['', Validators.required]
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

      this.tesouroDireto = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.tesouroDireto.id, ...this.form.value};

      this.tesourodiretoService[this.estadoSalvar](this.tesouroDireto).subscribe(
        (_tesouroDireto: TesouroDireto) => {

          this.toastr.success('Tesouro Direto salvo com sucesso!', 'Sucesso');
          this.router.navigate([`tesourosdiretos/detalhe/${_tesouroDireto.id}`]);
        },
        (error: any) => {
          console.error(error);
          //this.spinner.hide();
          this.toastr.error('Erro ao atualizar o tesouro direto', 'Erro');
        },
        () => {
          //this.spinner.hide();
        }
      ).add(() => {this.spinner.hide()});

    }

  }

}
