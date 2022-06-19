import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { AdministradorDeFundoImobiliario } from '@app/models/AdministradorDeFundoImobiliario';
import { AdministradordefundoimobiliarioService } from '@app/services/administradordefundoimobiliario.service';
import { Guid } from 'guid-typescript';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-administradoresdefundosimobiliarios-lista',
  templateUrl: './administradoresdefundosimobiliarios-lista.component.html',
  styleUrls: ['./administradoresdefundosimobiliarios-lista.component.scss']
})
export class AdministradoresdefundosimobiliariosListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public administradoresDeFundosDeInvestimentos: AdministradorDeFundoImobiliario[] = [];
  public administradoresDeFundosDeInvestimentosFiltrados: AdministradorDeFundoImobiliario[] = [];
  public administradorDeFundoImobiliarioId = Guid.createEmpty();

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.administradoresDeFundosDeInvestimentosFiltrados = this.filtroLista
                                                          ? this.filtrarAdministradoresDeFundosImobiliarios(this.filtroLista)
                                                          : this.administradoresDeFundosDeInvestimentos;
  }

  public onFiltroAcionado(evento: any) {
    this.filtroLista = evento.filtro;
  }

  filtrarAdministradoresDeFundosImobiliarios(filtrarPor: string): AdministradorDeFundoImobiliario[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.administradoresDeFundosDeInvestimentos.filter(
      (administradorDeFundoImobiliario: {razaoSocial: string}) =>
        administradorDeFundoImobiliario.razaoSocial.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(
    private administradordefundoimobiliarioService: AdministradordefundoimobiliarioService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.spinner.show();

    this.carregarAdministradoresDeFundosImobiliarios();

    setTimeout(() => {
      //this.spinner.hide();
    }, 3000);
  }

  public carregarAdministradoresDeFundosImobiliarios(): void {
    const observer = {
      next: (_administradoresDeFundosImobiliarios: AdministradorDeFundoImobiliario[]) => {
        this.administradoresDeFundosDeInvestimentos = _administradoresDeFundosImobiliarios;
        this.administradoresDeFundosDeInvestimentosFiltrados = this.administradoresDeFundosDeInvestimentos;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.administradordefundoimobiliarioService.getAllAdministradoresDeFundosImobiliarios().subscribe(observer);
  }

  openModal(event: any, template: TemplateRef<any>, administradorDeFundoImobiliarioId: Guid): void {
    event.stopPropagation();
    this.administradorDeFundoImobiliarioId = administradorDeFundoImobiliarioId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.administradordefundoimobiliarioService.deleteAdministradorDeFundoImobiliario(this.administradorDeFundoImobiliarioId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          this.carregarAdministradoresDeFundosImobiliarios();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar o administrador de fundo imobiliario ${this.administradorDeFundoImobiliarioId}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();})

    this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  public editarAdministradorDeFundoImobiliario(id: Guid): void {
    this.router.navigate([`administradoresdefundosimobiliarios/detalhe/${id}`])
  }

}
