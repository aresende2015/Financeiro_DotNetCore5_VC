import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { TesouroDireto } from '@app/models/TesouroDireto';
import { TesourodiretoService } from '@app/services/tesourodireto.service';
import { Guid } from 'guid-typescript';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-tesourosdiretos-lista',
  templateUrl: './tesourosdiretos-lista.component.html',
  styleUrls: ['./tesourosdiretos-lista.component.scss']
})
export class TesourosdiretosListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public tesourosDiretos: TesouroDireto[] = [];
  public tesourosDiretosFiltrados: TesouroDireto[] = [];
  public tesouroDiretoId = Guid.createEmpty();

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.tesourosDiretosFiltrados = this.filtroLista
                                    ? this.filtrarTesourosDiretos(this.filtroLista)
                                    : this.tesourosDiretos;
  }

  public onFiltroAcionado(evento: any) {
    this.filtroLista = evento.filtro;
  }

  filtrarTesourosDiretos(filtrarPor: string): TesouroDireto[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.tesourosDiretos.filter(
      (tesouroDireto: {descricao: string}) =>
      tesouroDireto.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(
    private tesourodiretoService: TesourodiretoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.spinner.show();

    this.carregarTesourosDiretos();

    setTimeout(() => {
      //this.spinner.hide();
    }, 3000);
  }

  public carregarTesourosDiretos(): void {
    const observer = {
      next: (_tesourosDiretos: TesouroDireto[]) => {
        this.tesourosDiretos = _tesourosDiretos;
        this.tesourosDiretosFiltrados = this.tesourosDiretos;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.tesourodiretoService.getAllTesourosDiretos().subscribe(observer);
  }

  openModal(event: any, template: TemplateRef<any>, tesouroDiretoId: Guid): void {
    event.stopPropagation();
    this.tesouroDiretoId = tesouroDiretoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.tesourodiretoService.deleteTesouroDireto(this.tesouroDiretoId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          this.carregarTesourosDiretos();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar o Tesouro Direto ${this.tesouroDiretoId}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();})

    this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  public editarTesouroDireto(id: Guid): void {
    this.router.navigate([`tesourosdiretos/detalhe/${id}`])
  }

}
