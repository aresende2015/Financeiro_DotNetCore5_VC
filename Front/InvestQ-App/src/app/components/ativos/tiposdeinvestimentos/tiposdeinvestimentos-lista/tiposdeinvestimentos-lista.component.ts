import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { TipoDeInvestimento } from '@app/models/TipoDeInvestimento';
import { Guid } from 'guid-typescript';
import { TipodeinvestimentoService } from '@app/services/tipodeinvestimento.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tiposdeinvestimentos-lista',
  templateUrl: './tiposdeinvestimentos-lista.component.html',
  styleUrls: ['./tiposdeinvestimentos-lista.component.scss']
})
export class TiposdeinvestimentosListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public tiposDeInvestimentos: TipoDeInvestimento[] = [];
  public tiposDeInvestimentosFiltrados: TipoDeInvestimento[] = [];
  public tipoDeInvestimentoId = Guid.createEmpty();

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.tiposDeInvestimentosFiltrados = this.filtroLista
                                        ? this.filtrarTiposDeInvestimentos(this.filtroLista)
                                        : this.tiposDeInvestimentos;
  }

  public onFiltroAcionado(evento: any) {
    this.filtroLista = evento.filtro;
  }

  filtrarTiposDeInvestimentos(filtrarPor: string): TipoDeInvestimento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.tiposDeInvestimentos.filter(
      (tipoDeInvestimento: {descricao: string}) =>
        tipoDeInvestimento.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(
    private tipodeinvestimentoService: TipodeinvestimentoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.spinner.show();

    this.carregarTiposDeInvestimentos();

    setTimeout(() => {
      //this.spinner.hide();
    }, 3000);
  }

  public carregarTiposDeInvestimentos(): void {
    const observer = {
      next: (_tiposDeInvestimentos: TipoDeInvestimento[]) => {
        this.tiposDeInvestimentos = _tiposDeInvestimentos;
        this.tiposDeInvestimentosFiltrados = this.tiposDeInvestimentos;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.tipodeinvestimentoService.getAllTiposDeInvestimentos().subscribe(observer);
  }

  openModal(event: any, template: TemplateRef<any>, tipoDeInvestimentoId: Guid): void {
    event.stopPropagation();
    this.tipoDeInvestimentoId = tipoDeInvestimentoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.tipodeinvestimentoService.deleteTipoDeInvestimento(this.tipoDeInvestimentoId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          this.carregarTiposDeInvestimentos();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar o setor ${this.tipoDeInvestimentoId}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();})

    this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  public editarTipoDeInvestimento(id: Guid): void {
    this.router.navigate([`tiposdeinvestimentos/detalhe/${id}`])
  }

}
