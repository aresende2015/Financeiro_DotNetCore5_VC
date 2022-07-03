import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { SegmentoAnbima } from '@app/models/SegmentoAnbima';
import { SegmentoanbimaService } from '@app/services/segmentoanbima.service';
import { Guid } from 'guid-typescript';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-segmentosanbimas-lista',
  templateUrl: './segmentosanbimas-lista.component.html',
  styleUrls: ['./segmentosanbimas-lista.component.scss']
})

export class SegmentosanbimasListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public segmentosAnbimas: SegmentoAnbima[] = [];
  public segmentosAnbimasFiltrados: SegmentoAnbima[] = [];
  public segmentoAnbimaId = Guid.createEmpty();

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.segmentosAnbimasFiltrados = this.filtroLista
                                        ? this.filtrarSegmentosAnbimas(this.filtroLista)
                                        : this.segmentosAnbimas;
  }

  public onFiltroAcionado(evento: any) {
    this.filtroLista = evento.filtro;
  }

  filtrarSegmentosAnbimas(filtrarPor: string): SegmentoAnbima[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.segmentosAnbimas.filter(
      (segmentoAnbima: {descricao: string}) =>
        segmentoAnbima.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(
    private segmentoanbimaService: SegmentoanbimaService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.spinner.show();

    this.carregarSegmentosAnbimas();

    setTimeout(() => {
      //this.spinner.hide();
    }, 3000);
  }

  public carregarSegmentosAnbimas(): void {
    const observer = {
      next: (_segmentosAnbimas: SegmentoAnbima[]) => {
        this.segmentosAnbimas = _segmentosAnbimas;
        this.segmentosAnbimasFiltrados = this.segmentosAnbimas;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.segmentoanbimaService.getAllSegmentosAnbimas().subscribe(observer);
  }

  openModal(event: any, template: TemplateRef<any>, segmentoAnbimaId: Guid): void {
    event.stopPropagation();
    this.segmentoAnbimaId = segmentoAnbimaId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.segmentoanbimaService.deleteSegmentoAnbima(this.segmentoAnbimaId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          this.carregarSegmentosAnbimas();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar o segmento anbima ${this.segmentoAnbimaId}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();})

    this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  public editarSegmentoAnbima(id: Guid): void {
    this.router.navigate([`segmentosanbimas/detalhe/${id}`])
  }

}
