import { Component, OnInit, TemplateRef } from '@angular/core';
import { SetorService } from './../../../services/setor.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';
import { Setor } from '@app/models/Setor';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-setor-lista',
  templateUrl: './setor-lista.component.html',
  styleUrls: ['./setor-lista.component.scss']
})
export class SetorListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public setores: Setor[] = [];
  public setoresFiltrados: Setor[] = [];
  public setorId = Guid.createEmpty();

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.setoresFiltrados = this.filtroLista
                              ? this.filtrarSetores(this.filtroLista)
                              : this.setores;
  }

  public onFiltroAcionado(evento: any) {
    this.filtroLista = evento.filtro;
  }

  filtrarSetores(filtrarPor: string): Setor[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.setores.filter(
      (setor: {descricao: string}) =>
          setor.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(
    private setorService: SetorService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.spinner.show();

    this.carregarSetores();

    setTimeout(() => {
      //this.spinner.hide();
    }, 3000);

  }

  public carregarSetores(): void {
    const observer = {
      next: (_setores: Setor[]) => {
        this.setores = _setores;
        this.setoresFiltrados = this.setores;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.setorService.getAllSetores().subscribe(observer);
  }

  openModal(event: any, template: TemplateRef<any>, setorId: Guid): void {
    event.stopPropagation();
    this.setorId = setorId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.setorService.deleteSetor(this.setorId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          this.carregarSetores();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar o setor ${this.setorId}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();})

    this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  public editarSetor(id: Guid): void {

    this.router.navigate([`setores/detalhe/${id}`])
  }

  public listarSubsetores(id: Guid): void {
    this.router.navigate([`setores/listarsubsetores/${id}`])
  }

}
