import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Corretora } from '@app/models/Corretora';
import { CorretoraService } from '@app/services/corretora.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-corretora-lista',
  templateUrl: './corretora-lista.component.html',
  styleUrls: ['./corretora-lista.component.scss']
})
export class CorretoraListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public corretoras: Corretora[] = [];
  public corretorasFiltradas: Corretora[] = [];

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.corretorasFiltradas = this.filtroLista ? this.filtrarCorretoras(this.filtroLista) : this.corretoras;
  }

  filtrarCorretoras(filtrarPor: string): Corretora[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.corretoras.filter(
      ( corretora: { descricao: string; }) =>
          corretora.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  public largudaImagem: number = 100;
  public margemImagem: number = 2;
  public mostrarImagem: boolean = true;

  constructor(
    private corretoraService: CorretoraService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  public ngOnInit(): void {
    /** spinner starts on init */
    this.spinner.show();

    this.getCorretoras();

    setTimeout(() => {
      /** spinner ends after 5 seconds */
      //this.spinner.hide();
    }, 3000);
  }

  public mostrarOcultarImagen(): void {
    this.mostrarImagem = !this.mostrarImagem;
  }

  public getCorretoras(): void {

    const observer = {
      next: (_corretoras: Corretora[]) => {
        this.corretoras = _corretoras;
        this.corretorasFiltradas = this.corretoras;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar a tela...', 'Error!');
      },
      complete: () => {this.spinner.hide()}
    }

    this.corretoraService.getAllCorretoras().subscribe(observer);
  }

  openModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  public detalheCorretora(id: number): void {
    this.router.navigate([`corretoras/detalhe/${id}`])
  }

}
