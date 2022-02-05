import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Cliente } from '@app/models/Cliente';
import { ClienteService } from '@app/services/cliente.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cliente-lista',
  templateUrl: './cliente-lista.component.html',
  styleUrls: ['./cliente-lista.component.scss']
  //providers: [ClienteService]
})
export class ClienteListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public clientes: Cliente[] = [];
  public clientesFiltrados: Cliente[] = [];
  public clienteId =0;

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.clientesFiltrados = this.filtroLista ? this.filtrarClientes(this.filtroLista) : this.clientes;
  }

  filtrarClientes(filtrarPor: string): Cliente[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.clientes.filter(
      ( cliente: { nomeCompleto: string; cpf: string; }) =>
            cliente.nomeCompleto.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
            cliente.cpf.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(
    private clienteService: ClienteService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  public ngOnInit() {
    this.spinner.show();
    this.carregarClientes();

    setTimeout(() => {
      /** spinner ends after 5 seconds */
      //this.spinner.hide();
    }, 3000);
  }

  public carregarClientes(): void {
    const observer = {
      next: (_clientes: Cliente[]) => {
        this.clientes = _clientes;
        this.clientesFiltrados = this.clientes;
      },
      error: (error: any) => {
        this.spinner.hide()
        this.toastr.error('Erro ao carregar a tela...', 'Error!');
      },
      complete: () => {
        this.spinner.hide()
      }

    };
    this.clienteService.getAllClientes().subscribe(observer);
  }

  public openModal(event: any, template: TemplateRef<any>, clienteId: number): void {
    event.stopPropagation();
    this.clienteId = clienteId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.clienteService.deleteCliente(this.clienteId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          this.spinner.hide();
          this.carregarClientes();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar o cliente ${this.clienteId}`, 'Erro');
        this.spinner.hide();
      },
      () => {this.spinner.hide();}
    );



  }

  public decline(): void {
    this.modalRef?.hide();
  }

  public detalheCliente(id: number): void {
    this.router.navigate([`clientes/detalhe/${id}`])
  }

}
