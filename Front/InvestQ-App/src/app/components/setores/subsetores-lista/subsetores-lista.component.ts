import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Setor } from '@app/models/Setor';
import { Subsetor } from '@app/models/Subsetor';
import { SubsetorService } from '@app/services/subsetor.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-subsetores-lista',
  templateUrl: './subsetores-lista.component.html',
  styleUrls: ['./subsetores-lista.component.scss']
})
export class SubsetoresListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public subsetores: Subsetor[] = [];
  public subsetoresFiltrados: Subsetor[] = [];
  public subsetorId = 0;

  setorId: number;
  setorDescricao: string;

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.subsetoresFiltrados = this.filtroLista
                              ? this.filtrarSubsetores(this.filtroLista)
                              : this.subsetores;
  }

  filtrarSubsetores(filtrarPor: string): Subsetor[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.subsetores.filter(
      (subsetor: {descricao: string}) =>
          subsetor.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(private subsetorService: SubsetorService,
              private spinner: NgxSpinnerService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private activatedRouter: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.spinner.show();

    this.carregarSubsetores();
  }

  public carregarSubsetores(): void {
    this.setorId = +this.activatedRouter.snapshot.paramMap.get('id')!;

    const observer = {
      next: (_subsetores: Subsetor[]) => {
        this.subsetores = _subsetores;
        this.subsetoresFiltrados = this.subsetores;

        this.setorDescricao = this.subsetores.find(s => s.setorId === this.setorId).setor.descricao;

      },
      error: (error: any) => {
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {}
    }

    this.subsetorService.getSubsetoresBySetorId(this.setorId).subscribe(observer).add(() => {this.spinner.hide()});
  }

  openModal(event: any, template: TemplateRef<any>, subsetorId: number): void {
    event.stopPropagation();
    this.subsetorId = subsetorId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.subsetorService.deleteSubsetor(this.setorId, this.subsetorId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          this.carregarSubsetores();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar o subsetor ${this.subsetorId}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();})

    this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  public editarSubsetor(id: number): void {
    this.router.navigate([`setores/subsetordetalhe/${id}`])
  }

  public listarSegmentos(id: number): void {
    this.router.navigate([`setores/listarsegmentos/${id}`])
  }

}
