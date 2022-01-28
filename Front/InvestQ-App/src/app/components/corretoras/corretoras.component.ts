import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Corretora } from '../../models/Corretora';
import { CorretoraService } from '../../services/corretora.service';

@Component({
  selector: 'app-corretoras',
  templateUrl: './corretoras.component.html',
  styleUrls: ['./corretoras.component.scss'],
  //providers: [CorretoraService]
})
export class CorretorasComponent implements OnInit {

  modalRef?: BsModalRef;

  public corretoras: Corretora[] = [];
  public largudaImagem: number = 100;
  public margemImagem: number = 2;
  public mostrarImagem: boolean = true;

  constructor(
    private corretoraService: CorretoraService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  public ngOnInit(): void {
    this.getCorretoras();
    /** spinner starts on init */
    this.spinner.show();

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
        this.corretoras = _corretoras
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar a tela...', 'Error!');
      },
      complete: () => {this.spinner.hide()}
    }

    this.corretoraService.getCorretoras().subscribe(observer

      //(_corretoras: Corretora[]) => {
      //  this.corretoras = _corretoras
      //},
      //error => console.log(error)
    );
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

}
