import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { Acao } from '@app/models/Acao';
import { AcaoService } from '@app/services/acao.service';
import { Guid } from 'guid-typescript';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-acoes-lista',
  templateUrl: './acoes-lista.component.html',
  styleUrls: ['./acoes-lista.component.scss']
})
export class AcoesListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public acoes: Acao[] = [];
  public acoesFiltrados: Acao[] = [];
  public acaoId = Guid.createEmpty();

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.acoesFiltrados = this.filtroLista
                          ? this.filtrarAcoes(this.filtroLista)
                          : this.acoes;
  }

  public onFiltroAcionado(evento: any) {
    this.filtroLista = evento.filtro;
  }

  filtrarAcoes(filtrarPor: string): Acao[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.acoes.filter(
      (acao: {descricao: string}) =>
      acao.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(
    private acaoService: AcaoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.spinner.show();

    this.carregarAcoes();

    setTimeout(() => {
      //this.spinner.hide();
    }, 3000);
  }

  public carregarAcoes(): void {
    const observer = {
      next: (_acoes: Acao[]) => {
        this.acoes = _acoes;
        this.acoesFiltrados = this.acoes;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.acaoService.getAllAcoes().subscribe(observer);
  }

  openModal(event: any, template: TemplateRef<any>, acaoId: Guid): void {
    event.stopPropagation();
    this.acaoId = acaoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.acaoService.deleteAcao(this.acaoId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          this.carregarAcoes();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar a ação ${this.acaoId}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();})

    this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  public editarAcao(id: Guid): void {
    this.router.navigate([`acoes/detalhe/${id}`])
  }

}
