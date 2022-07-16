import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Lancamento } from '@app/models/Lancamento';
import { LancamentoService } from '@app/services/lancamento.service';
import { Guid } from 'guid-typescript';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { CarteiraService } from '@app/services/carteira.service';
import { Carteira } from '@app/models/Carteira';
import { TipoDeMovimentacao } from '@app/models/Enum/TipoDeMovimentacao.enum';
import { PaginatedResult, Pagination } from '@app/models/pagination/Pagination';
import { debounceTime, Subject } from 'rxjs';

@Component({
  selector: 'app-lancamentos-lista',
  templateUrl: './lancamentos-lista.component.html',
  styleUrls: ['./lancamentos-lista.component.scss']
})
export class LancamentosListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public pagination = {} as Pagination;

  public _filtro: string = '';

  termoBuscaChanged: Subject<string> = new Subject<string>();

  public lancamentos: Lancamento[] = [];
  public lancamentoId = Guid.createEmpty();
  public carteiraId =  Guid.createEmpty();
  public carteiraDescricao: string = '';
  public tipoDeMovimentacao: TipoDeMovimentacao = TipoDeMovimentacao.NaoInformada;

  public onFiltroAcionado(evento: any) {
    this._filtro = evento.filtro;
    this.filtrarLancamentos(evento.filtro) ;
  }

  filtrarLancamentos(evt: any): void {
    if (this.termoBuscaChanged.observers.length === 0) {
      this.termoBuscaChanged.pipe(debounceTime(500)).subscribe(
        filtrarPor => {
          this.spinner.show();
          this.lancamentoService.getAllLancamentosByCarteiraId(
            this.carteiraId,
            this.pagination.currentPage,
            this.pagination.itemsPerPage,
            filtrarPor
        ).subscribe(
          (paginatedResult: PaginatedResult<Lancamento[]>) => {
            this.lancamentos = paginatedResult.result;
            this.pagination = paginatedResult.pagination;
          },
          (error: any) => {
            console.log(error.error)
            this.toastr.error('Erro ao carregar a tela...', 'Error!');
          }
        ).add(() => this.spinner.hide());
        }
      )
    }
    this.termoBuscaChanged.next(evt);
  }

  constructor(
    private lancamentoService: LancamentoService,
    private carteiraService: CarteiraService,
    private modalService: BsModalService,
    private activatedRouter: ActivatedRoute,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit() {
    this.pagination = {currentPage: 1, itemsPerPage: 5, totalItems: 1} as Pagination;
    this.carregarLancamentos();

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

  public getTipoDeMovimentacao(_tipoDeMovimentacao): string {
    return TipoDeMovimentacao[_tipoDeMovimentacao];
  }

  public carregarLancamentos(): void {
    this.spinner.show();

    this.carteiraId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

    this.bucarNomeDaCarteira(this.carteiraId);

    const observer = {
      next: (paginatedResult: PaginatedResult<Lancamento[]>) => {
        this.lancamentos = paginatedResult.result;
        this.pagination = paginatedResult.pagination;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.lancamentoService
        .getAllLancamentosByCarteiraId(this.carteiraId,
                                       this.pagination.currentPage,
                                       this.pagination.itemsPerPage,
                                       null)
        .subscribe(observer);
  }

  public openModal(event: any, template: TemplateRef<any>, lancamentosId: Guid): void {
    event.stopPropagation();
    this.lancamentoId = lancamentosId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.lancamentoService.deleteLancamento(this.lancamentoId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          this.carregarLancamentos();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar o lancamento ${this.lancamentoId}. ${error.error}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();});
  }

  public decline(): void {
    this.modalRef?.hide();
  }

  public editarLancamento(id: Guid): void {
    this.router.navigate([`lancamentos/detalhe/${this.carteiraId}/${id}`])  ;
  }

  public pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.carregarLancamentos();
    this.filtrarLancamentos(this._filtro) ;
  }

}
