import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Ativo } from '@app/models/Ativo';
import { TipoDeMovimentacao } from '@app/models/Enum/TipoDeMovimentacao.enum';
import { Lancamento } from '@app/models/Lancamento';
import { Portifolio } from '@app/models/portifolio';
import { AtivoService } from '@app/services/ativo.service';
import { LancamentoService } from '@app/services/lancamento.service';
import { PortifolioService } from '@app/services/portifolio.service';
import { Guid } from 'guid-typescript';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { TipoDeAtivo } from '@app/models/Enum/TipoDeAtivo.enum';

@Component({
  selector: 'app-portifolios-lista-ativos',
  templateUrl: './portifolios-lista-ativos.component.html',
  styleUrls: ['./portifolios-lista-ativos.component.scss']
})
export class PortifoliosListaAtivosComponent implements OnInit {

  @Input() _tipoDeAtivo = TipoDeAtivo.NaoInformada;

  public portifolios: Portifolio[] = [];
  public portifoliosFiltrados: Portifolio[] = [];
  public portifolioId = Guid.createEmpty();
  public carteiraId =  Guid.createEmpty();

  public ativo = {} as Ativo;
  private _filtroLista: string = '';

  public lancamento = {} as Lancamento;
  public tipoDeMovimentacaoOp: any[];
  public tipoDeMovimentacaoCompra = TipoDeMovimentacao.Compra;
  public tipoDeMovimentacaoVenda = TipoDeMovimentacao.Venda;

  modalRef?: BsModalRef;
  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  public getTipoDeMovimentacao() {
    return [
      {valor: TipoDeMovimentacao.NaoInformada, desc: 'NaoInformada' },
      {valor: TipoDeMovimentacao.Compra, desc: 'Compra' },
      {valor: TipoDeMovimentacao.Venda, desc: 'Venda' },
    ];
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

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.portifoliosFiltrados = this.filtroLista
                                    ? this.filtrarPortifolios(this.filtroLista)
                                    : this.portifolios;
  }

  public onFiltroAcionado(evento: any) {
    this.filtrarPortifolios(evento.filtro) ;
  }

  filtrarPortifolios(filtrarPor: string): Portifolio[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.portifolios.filter(
      (portifolio: {codigoDoAtivo: string}) =>
      portifolio.codigoDoAtivo.toString().indexOf(filtrarPor) !== -1
    )
  }

  constructor(
    private fb: FormBuilder,
    private modalService: BsModalService,
    private portifolioService: PortifolioService,
    private lancamentoService: LancamentoService,
    private ativoService: AtivoService,
    private activatedRouter: ActivatedRoute,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
  ) { }

  ngOnInit(): void {
    this.carregarPortifolios(null);

    setTimeout(() => {
      /** spinner ends after 5 seconds */
      //this.spinner.hide();
    }, 3000);
  }

  public carregarPortifolios(_carteiraId: Guid): void {
    this.spinner.show();

    if (_carteiraId === null) {
      this.carteiraId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());
    } else {
      this.carteiraId = _carteiraId;
    }

    const observer = {
      next: (_portifolios: Portifolio[]) => {
        this.portifolios = _portifolios;
        this.portifoliosFiltrados = this.portifolios;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.portifolioService.getAllPortifoliosByCarteiraIdTipoDeAtivo(this.carteiraId, this._tipoDeAtivo).subscribe(observer);
  }

  public carregarAtivo(ativo: Guid): void {

    const observer = {
      next: (_ativo: Ativo) => {
        this.ativo = _ativo;
      },
      error: (error: any) => {
        this.spinner.hide();
        console.error(error);
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.ativoService.getAtivoById(ativo).subscribe(observer);
  }

  public validation(): void {
    this.form = this.fb.group({
      valorDaOperacao: ['', [Validators.required]],
      dataDaOperacao: ['', [Validators.required]],
      quantidade: ['', [Validators.required]],
      tipoDeMovimentacao: [0, [Validators.required]],
      tipoDeOperacao: [0, [Validators.required]],
      codigoDoAtivo: [''],
      carteiraId: [null, [Validators.required]]
    });
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public resetForm(): void {
    this.form.reset();
  }

  public openModal(event: any,
                   template: TemplateRef<any>,
                   carteiraId: Guid,
                   ativoId: Guid,
                   tipoDeMovimentacao: TipoDeMovimentacao): void {

    event.stopPropagation();

    this.validation();

    this.tipoDeMovimentacaoOp = this.lancamentoService.getTipoDeMovimentacao();
    this.form.controls['tipoDeMovimentacao'].setValue(tipoDeMovimentacao);
    this.carregarAtivo(ativoId);
    this.form.controls['codigoDoAtivo'].setValue(this.ativo.codigoDoAtivo);
    this.form.controls['carteiraId'].setValue(carteiraId);

    this.form.controls['tipoDeMovimentacao'].disable();
    this.form.controls['codigoDoAtivo'].disable();

    this.modalRef = this.modalService.show(template);
  }

  public decline(): void {
    this.modalRef?.hide();
  }

  public confirm(): void {
    this.spinner.show();

    this.lancamento.carteiraId = this.form.controls['carteiraId'].value;
    this.lancamento.valorDaOperacao = this.form.controls['valorDaOperacao'].value;
    this.lancamento.quantidade = this.form.controls['quantidade'].value;
    this.lancamento.dataDaOperacao = this.form.controls['dataDaOperacao'].value;
    this.lancamento.tipoDeMovimentacao = this.form.controls['tipoDeMovimentacao'].value;
    this.lancamento.ativoId = this.ativo.id;
    this.lancamento.tipoDeOperacao = 0;
    this.lancamento.tipoDeAtivo = this.ativo.tipoDeAtivo;

    this.modalRef?.hide();

    this.spinner.show();

    this.lancamentoService.post(this.lancamento).subscribe(
      (_lancamento: Lancamento) => {
          this.toastr.success('Lançamento salvo com sucesso!', 'Sucesso');
          this.carregarPortifolios(_lancamento.carteiraId);
      },
      (error: any) => {
        console.error(error);
        this.toastr.error('Erro ao incluir o lançamento', 'Erro');
      },
    ).add(() => {this.spinner.hide();});
  }

}
