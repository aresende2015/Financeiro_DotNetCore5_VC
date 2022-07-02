import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { Provento } from '@app/models/Provento';
import { ProventoService } from '@app/services/provento.service';
import { Guid } from 'guid-typescript';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { TipoDeAtivo } from '@app/models/Enum/TipoDeAtivo.enum';

@Component({
  selector: 'app-proventos-lista',
  templateUrl: './proventos-lista.component.html',
  styleUrls: ['./proventos-lista.component.scss']
})
export class ProventosListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public proventos: Provento[] = [];
  public proventosFiltrados: Provento[] = [];
  public proventoId = Guid.createEmpty();

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.proventosFiltrados = this.filtroLista
                                    ? this.filtrarProventos(this.filtroLista)
                                    : this.proventos;
  }

  public onFiltroAcionado(evento: any) {
    this.filtrarProventos(evento.filtro) ;
  }

  filtrarProventos(filtrarPor: string): Provento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.proventos.filter(
      (provento: {dataCom: Date}) =>
      provento.dataCom.toString().indexOf(filtrarPor) !== -1
    )
  }

  constructor(
    private proventoService: ProventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit() {
    this.carregarProventos();

    setTimeout(() => {
      /** spinner ends after 5 seconds */
      //this.spinner.hide();
    }, 3000);
  }

  public carregarProventos(): void {
    this.spinner.show();
    const observer = {
      next: (_proventos: Provento[]) => {
        this.proventos = _proventos;
        //alert(this.proventos[1].ativo.codigoDoAtivo);
        this.proventosFiltrados = this.proventos;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.proventoService.getAllProventos().subscribe(observer);
  }

  public openModal(event: any, template: TemplateRef<any>, proventoId: Guid): void {
    event.stopPropagation();
    this.proventoId = proventoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.proventoService.deleteProvento(this.proventoId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          this.carregarProventos();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar o provento ${this.proventoId}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();});
  }

  public decline(): void {
    this.modalRef?.hide();
  }

  public editarProvento(id: Guid): void {
    this.router.navigate([`proventos/detalhe/${id}`])
  }

}
