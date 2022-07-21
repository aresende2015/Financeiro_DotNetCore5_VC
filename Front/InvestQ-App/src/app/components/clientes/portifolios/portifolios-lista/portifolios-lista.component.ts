import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Carteira } from '@app/models/Carteira';
import { TipoDeMovimentacao } from '@app/models/Enum/TipoDeMovimentacao.enum';
import { Portifolio } from '@app/models/portifolio';
import { CarteiraService } from '@app/services/carteira.service';
import { PortifolioService } from '@app/services/portifolio.service';
import { Guid } from 'guid-typescript';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { LancamentoService } from '@app/services/lancamento.service';
import { Lancamento } from '@app/models/Lancamento';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-portifolios-lista',
  templateUrl: './portifolios-lista.component.html',
  styleUrls: ['./portifolios-lista.component.scss']
})
export class PortifoliosListaComponent implements OnInit {

  public portifolios: Portifolio[] = [];
  public portifoliosFiltrados: Portifolio[] = [];
  public portifolioId = Guid.createEmpty();
  public carteiraId =  Guid.createEmpty();
  public carteiraDescricao: string = '';

  public lancamento = {} as Lancamento;

  modalRef?: BsModalRef;
  form!: FormGroup;

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

  private _filtroLista: string = '';

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
    private carteiraService: CarteiraService,
    private lancamentoService: LancamentoService,
    private activatedRouter: ActivatedRoute,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit() {
    this.carregarPortifolios();

    setTimeout(() => {
      /** spinner ends after 5 seconds */
      //this.spinner.hide();
    }, 3000);
  }

  public bucarNomeDaCarteira(carteiraId: Guid): void {
    this.carteiraService.getCarteiraById(carteiraId).subscribe({
      next: (carteira: Carteira) => {
        this.carteiraDescricao = carteira.descricao;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao tentar carregar o nome da carteira.', 'Erro!');
        console.error(error)
      },
      complete: () => {this.spinner.hide()}
    });
  }

  public carregarPortifolios(): void {
    this.spinner.show();

    this.carteiraId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

    this.bucarNomeDaCarteira(this.carteiraId);

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

    this.portifolioService.getAllPortifoliosByCarteiraId(this.carteiraId).subscribe(observer);
  }

  // public comprarAtivo(carteiraId: Guid, ativoId: Guid): void {
  //   //this.router.navigate([`clientes/listarcarteiras/${id}`])
  // }

  // public venderAtivo(id: Guid, ativoId: Guid): void {
  //   //this.router.navigate([`clientes/listarcarteiras/${id}`])
  // }

  public validation(): void {
    this.form = this.fb.group({
      valorDaOperacao: ['', [Validators.required]],
      dataDaOperacao: ['', [Validators.required]],
      quantidade: ['', [Validators.required]],
      tipoDeMovimentacao: [0, [Validators.required]],
      tipoDeOperacao: [0, [Validators.required]],
      ativoId: [null, [Validators.required]],
      tipoDeAtivo: [null, [Validators.required]],
      carteiraId: [null, [Validators.required]]
    });
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public resetForm(): void {
    this.form.reset();
  }

  public openModal(event: any, template: TemplateRef<any>, carteiraId: Guid, ativoId: Guid): void {
    event.stopPropagation();
    //this.lancamentoId = lancamentosId;
    this.modalRef = this.modalService.show(template);
  }

  public decline(): void {
    this.modalRef?.hide();
  }

  public confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.lancamentoService.post(this.lancamento).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          //this.carregarLancamentos();
        }
      },
      (error: any) => {
        console.error(error);
        //this.toastr.error(`Erro ao tentar deletar o lancamento ${this.lancamentoId}. ${error.error}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();});
  }

}
