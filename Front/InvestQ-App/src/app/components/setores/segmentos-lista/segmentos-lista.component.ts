import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Segmento } from '@app/models/Segmento';
import { Subsetor } from '@app/models/Subsetor';
import { SegmentoService } from '@app/services/segmento.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-segmentos-lista',
  templateUrl: './segmentos-lista.component.html',
  styleUrls: ['./segmentos-lista.component.scss']
})
export class SegmentosListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public segmentos: Segmento[] = [];
  public segmentosFiltrados: Segmento[] = [];

  public subsetor: Subsetor;

  public descricaoSubsetor: string = '';
  public descricaoSetor: string = '';

  public segmentoId = Guid.createEmpty();

  subsetorId: Guid;

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.segmentosFiltrados = this.filtroLista
                              ? this.filtrarSegmentos(this.filtroLista)
                              : this.segmentos;
  }

  filtrarSegmentos(filtrarPor: string): Segmento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.segmentos.filter(
      (segmento: {descricao: string}) =>
        segmento.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(private segmentoService: SegmentoService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService,
              private modalService: BsModalService,
              private activatedRouter: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.spinner.show();

    this.carregarSegmentos();
  }

  public carregarSegmentos(): void {

    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      this.subsetorId = Guid.createEmpty();
    else {
      this.subsetorId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());
      //this.subsetorId = +this.activatedRouter.snapshot.paramMap.get('id')!;

      const observer = {
        next: (_segmentos: Segmento[]) => {
          this.segmentos = _segmentos;
          this.segmentosFiltrados = this.segmentos;

          if (this.segmentos !== null && this.segmentos.length > 0) {
            this.descricaoSubsetor = this.segmentos.find(s => s.subsetorId == this.subsetorId).subsetor.descricao.toString();
            this.descricaoSetor = this.segmentos.find(s => s.subsetorId == this.subsetorId).subsetor.setor.descricao.toString();
          }
        },
        error: (error: any) => {
          this.toastr.error('Erro ao carregar a tela...', 'Error"');
          console.error(error);
        },
        complete: () => {}
      }
      this.segmentoService.getSegmentosBySubsetorId(this.subsetorId).subscribe(observer).add(() => {this.spinner.hide()});
    }


  }

  openModal(event: any, template: TemplateRef<any>, segmentoId: Guid): void {
    event.stopPropagation();
    this.segmentoId = segmentoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.segmentoService.deleteSegmento(this.subsetorId, this.segmentoId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          this.carregarSegmentos();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar o segmento ${this.segmentoId}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();})

    this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  public editarSegmento(subsetorId: Guid, id: Guid): void {
    this.router.navigate([`setores/segmentodetalhe/${subsetorId}/${id}`])
  }

}
