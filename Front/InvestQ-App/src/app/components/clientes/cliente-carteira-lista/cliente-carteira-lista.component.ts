import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Carteira } from '@app/models/Carteira';
import { Cliente } from '@app/models/Cliente';
import { CarteiraService } from '@app/services/carteira.service';
import { ClienteService } from '@app/services/cliente.service';
import { Guid } from 'guid-typescript';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cliente-carteira-lista',
  templateUrl: './cliente-carteira-lista.component.html',
  styleUrls: ['./cliente-carteira-lista.component.scss']
})
export class ClienteCarteiraListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public carteiras: Carteira[] = [];
  public carteirasFiltrados: Carteira[] = [];
  public carteiraId = Guid.createEmpty();

  clienteId: Guid;
  corretoraId: Guid;

  clienteNome: string;

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.carteirasFiltrados = this.filtroLista
                              ? this.filtrarCarteiras(this.filtroLista)
                              : this.carteiras;
  }

  public onFiltroAcionado(evento: any) {
    this.filtroLista = evento.filtro;
  }

  filtrarCarteiras(filtrarPor: string): Carteira[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.carteiras.filter(
      (carteira: {descricao: string}) =>
        carteira.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(private carteiraService: CarteiraService,
              private clienteService: ClienteService,
              private spinner: NgxSpinnerService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private activatedRouter: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.spinner.show();

    this.carregarCarteiras();
    this.bucarNomeDoCliente();

  }

  public bucarNomeDoCliente(): void {
    this.clienteService.getClienteById(this.clienteId).subscribe({
      next: (cliente: Cliente) => {
        this.clienteNome = cliente.nomeCompleto;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao tentar carregar o nome do cliente.', 'Erro!');
        console.error(error)
      },
      complete: () => {this.spinner.hide()}
    });
  }

  public carregarCarteiras(): void {

    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      this.clienteId = Guid.createEmpty();
    else {

      this.clienteId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());
      //this.setorId = +this.activatedRouter.snapshot.paramMap.get('id')!;

      const observer = {
        next: (_carteiras: Carteira[]) => {
          this.carteiras = _carteiras;
          this.carteirasFiltrados = this.carteiras;
        },
        error: (error: any) => {
          this.toastr.error('Erro ao carregar a tela...', 'Error"');
        },
        complete: () => {}
      }

      this.carteiraService.getCarteirasByClienteId(this.clienteId).subscribe(observer).add(() => {this.spinner.hide()});

    }
  }

  openModal(event: any, template: TemplateRef<any>, carteiraId: Guid): void {
    event.stopPropagation();
    this.carteiraId = carteiraId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.carteiraService.deleteCarteira(this.carteiraId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          this.carregarCarteiras();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar a carteira ${this.carteiraId}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();})

    this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  public editarCarteira(id: Guid): void {
    //alert(this.clienteId);
    //alert(id);
    this.router.navigate([`clientes/carteiradetalhe/${this.clienteId}/${id}`])  ;
  }

}
