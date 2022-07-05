import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Lancamento } from '@app/models/Lancamento';
import { LancamentoService } from '@app/services/lancamento.service';
import { Guid } from 'guid-typescript';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-lancamentos-lista',
  templateUrl: './lancamentos-lista.component.html',
  styleUrls: ['./lancamentos-lista.component.scss']
})
export class LancamentosListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public lancamentos: Lancamento[] = [];
  public lancamentosFiltrados: Lancamento[] = [];
  public lancamentosId = Guid.createEmpty();
  public carteiraId =  Guid.createEmpty();

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.lancamentosFiltrados = this.filtroLista
                                    ? this.filtrarLancamentos(this.filtroLista)
                                    : this.lancamentos;
  }

  public onFiltroAcionado(evento: any) {
    this.filtrarLancamentos(evento.filtro) ;
  }

  filtrarLancamentos(filtrarPor: string): Lancamento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.lancamentos.filter(
      (lancamento: {dataDaOperacao: Date}) =>
      lancamento.dataDaOperacao.toString().indexOf(filtrarPor) !== -1
    )
  }

  constructor(
    private lancamentoService: LancamentoService,
    private modalService: BsModalService,
    private activatedRouter: ActivatedRoute,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit() {
    this.carregarLancamentos();

    setTimeout(() => {
      /** spinner ends after 5 seconds */
      //this.spinner.hide();
    }, 3000);
  }

  public carregarLancamentos(): void {
    this.spinner.show();

    this.carteiraId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());
    //alert(this.carteiraId);
    const observer = {
      next: (_lancamentos: Lancamento[]) => {
        this.lancamentos = _lancamentos;
        //alert(this.proventos[1].ativo.codigoDoAtivo);
        this.lancamentosFiltrados = this.lancamentos;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.lancamentoService.getAllLancamentosByCarteiraId(this.carteiraId).subscribe(observer);
  }

  public openModal(event: any, template: TemplateRef<any>, lancamentosId: Guid): void {
    event.stopPropagation();
    this.lancamentosId = lancamentosId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.lancamentoService.deleteLancamento(this.lancamentosId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          //this.carregarLancamentos(Guid.createEmpty());
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar o lancamento ${this.lancamentosId}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();});
  }

  public decline(): void {
    this.modalRef?.hide();
  }

  public editarProvento(id: Guid): void {
    this.router.navigate([`lancamentos/detalhe/${id}`])
  }

}
