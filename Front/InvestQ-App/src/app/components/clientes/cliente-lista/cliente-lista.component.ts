import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Cliente } from '@app/models/Cliente';
import { ClienteService } from '@app/services/cliente.service';
import { Router } from '@angular/router';
import { PaginatedResult, Pagination } from '@app/models/pagination/Pagination';
import { debounceTime, Subject } from 'rxjs';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-cliente-lista',
  templateUrl: './cliente-lista.component.html',
  styleUrls: ['./cliente-lista.component.scss']
  //providers: [ClienteService]
})
export class ClienteListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public pagination = {} as Pagination;

  public clientes: Cliente[] = [];
  public clienteId = Guid.createEmpty();

  termoBuscaChanged: Subject<string> = new Subject<string>();

  filtrarClientes(evt: any): void {
    if (this.termoBuscaChanged.observers.length === 0) {
      this.termoBuscaChanged.pipe(debounceTime(1000)).subscribe(
        filtrarPor => {
          this.spinner.show();
          this.clienteService.getAllClientes(
            this.pagination.currentPage,
            this.pagination.itemsPerPage,
            filtrarPor
        ).subscribe(
          (paginatedResult: PaginatedResult<Cliente[]>) => {
            this.clientes = paginatedResult.result;
            this.pagination = paginatedResult.pagination;
          },
          (error: any) => {
            console.log(error.error)
            this.toastr.error('Erro ao carregar a tela...', 'Error!');
          }
        ).add(() => this.spinner.hide());
        }
      )
    }
    this.termoBuscaChanged.next(evt.value);
  }

  constructor(
    private clienteService: ClienteService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  public ngOnInit() {
    this.pagination = {currentPage: 1, itemsPerPage: 2, totalItems: 1} as Pagination;
    this.carregarClientes();

    setTimeout(() => {
      /** spinner ends after 5 seconds */
      //this.spinner.hide();
    }, 3000);
  }

  public carregarClientes(): void {
    this.spinner.show();
    const observer = {
      next: (paginatedResult: PaginatedResult<Cliente[]>) => {
        this.clientes = paginatedResult.result;
        this.pagination = paginatedResult.pagination;
      },
      error: (error: any) => {
        console.log(error.error)
        this.toastr.error('Erro ao carregar a tela...', 'Error!');
      }
    };
    this.clienteService.getAllClientes(this.pagination.currentPage, this.pagination.itemsPerPage, null)
                       .subscribe(observer)
                       .add(() => this.spinner.hide());
  }

  public openModal(event: any, template: TemplateRef<any>, clienteId: Guid): void {
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
          //this.spinner.hide();
          this.carregarClientes();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar o cliente ${this.clienteId}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();});
  }

  public decline(): void {
    this.modalRef?.hide();
  }

  public detalheCliente(id: Guid): void {
    this.router.navigate([`clientes/detalhe/${id}`])
  }

  public pageChanged(event): void {
    this.pagination.currentPage = event.page;
    this.carregarClientes();
  }

}
