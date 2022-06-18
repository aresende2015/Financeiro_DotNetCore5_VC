import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { PaginatedResult, Pagination } from '@app/models/pagination/Pagination';
import { FundoimobiliarioService } from '@app/services/fundoimobiliario.service';
import { Guid } from 'guid-typescript';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { debounceTime, Subject } from 'rxjs';
import { FundoImobiliario } from './../../../models/FundoImobiliario';

@Component({
  selector: 'app-fundosimobiliarios-lista',
  templateUrl: './fundosimobiliarios-lista.component.html',
  styleUrls: ['./fundosimobiliarios-lista.component.scss']
})
export class FundosimobiliariosListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public pagination = {} as Pagination;

  public fundosImobiliarios: FundoImobiliario[] = [];
  public fundoImobiliarioId = Guid.createEmpty();

  termoBuscaChanged: Subject<string> = new Subject<string>();

  filtrarFundosImobiliarios(evt: any): void {
    if (this.termoBuscaChanged.observers.length === 0) {
      this.termoBuscaChanged.pipe(debounceTime(1000)).subscribe(
        filtrarPor => {
          this.spinner.show();
          this.fundoImobiliarioService.getAllFundosImobiliarios(
            this.pagination.currentPage,
            this.pagination.itemsPerPage,
            filtrarPor
        ).subscribe(
          (paginatedResult: PaginatedResult<FundoImobiliario[]>) => {
            this.fundosImobiliarios = paginatedResult.result;
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
    private fundoImobiliarioService: FundoimobiliarioService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit() {
    this.pagination = {currentPage: 1, itemsPerPage: 2, totalItems: 1} as Pagination;
    this.carregarFundosImobiliarios();

    setTimeout(() => {
      /** spinner ends after 5 seconds */
      //this.spinner.hide();
    }, 3000);
  }

  public carregarFundosImobiliarios(): void {
    this.spinner.show();
    const observer = {
      next: (paginatedResult: PaginatedResult<FundoImobiliario[]>) => {
        this.fundosImobiliarios = paginatedResult.result;
        this.pagination = paginatedResult.pagination;
      },
      error: (error: any) => {
        console.log(error.error)
        this.toastr.error('Erro ao carregar a tela...', 'Error!');
      }
    };
    this.fundoImobiliarioService.getAllFundosImobiliarios(this.pagination.currentPage, this.pagination.itemsPerPage, null)
                                .subscribe(observer)
                                .add(() => this.spinner.hide());
  }

  public openModal(event: any, template: TemplateRef<any>, fundoImobiliarioId: Guid): void {
    event.stopPropagation();
    this.fundoImobiliarioId = fundoImobiliarioId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.fundoImobiliarioService.deleteFundoImobiliario(this.fundoImobiliarioId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          this.carregarFundosImobiliarios();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar o fundo imobiliário ${this.fundoImobiliarioId}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();});
  }

  public decline(): void {
    this.modalRef?.hide();
  }

  public detalheFundoImobiliario(id: Guid): void {
    this.router.navigate([`fundosimobiliarios/detalhe/${id}`])
  }

  public pageChanged(event): void {
    this.pagination.currentPage = event.page;
    this.carregarFundosImobiliarios();
  }

}
