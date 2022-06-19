import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Corretora } from '@app/models/Corretora';
import { CorretoraService } from '@app/services/corretora.service';
import { Router } from '@angular/router';
import { environment } from '@environments/environment';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-corretora-lista',
  templateUrl: './corretora-lista.component.html',
  styleUrls: ['./corretora-lista.component.scss']
})
export class CorretoraListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public corretoras: Corretora[] = [];
  public corretorasFiltradas: Corretora[] = [];
  public corretoraId = Guid.createEmpty();

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.corretorasFiltradas = this.filtroLista ? this.filtrarCorretoras(this.filtroLista) : this.corretoras;
  }

  public onFiltroAcionado(evento: any) {
    this.filtroLista = evento.filtro;
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

    this.carregarCorretoras();

    setTimeout(() => {
      /** spinner ends after 5 seconds */
      //this.spinner.hide();
    }, 3000);
  }

  public mostrarOcultarImagen(): void {
    this.mostrarImagem = !this.mostrarImagem;
  }

  public mostraImagem(imagemURL: string): string {
    return (imagemURL !== '')
      ? `${environment.apiURL}resources/images/${imagemURL}`
      : 'assets/img/semImagem.jpeg';
  }

  public carregarCorretoras(): void {

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

  openModal(event: any, template: TemplateRef<any>, corretoraId: Guid): void {
    event.stopPropagation();
    this.corretoraId = corretoraId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.corretoraService.deleteCorretora(this.corretoraId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          this.carregarCorretoras();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar o cliente ${this.corretoraId}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();})

    this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  public detalheCorretora(id: Guid): void {
    this.router.navigate([`corretoras/detalhe/${id}`])
  }

}
