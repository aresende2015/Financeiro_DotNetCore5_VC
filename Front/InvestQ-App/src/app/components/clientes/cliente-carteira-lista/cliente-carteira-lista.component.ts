import { Component, OnInit } from '@angular/core';
import { Carteira } from '@app/models/Carteira';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-cliente-carteira-lista',
  templateUrl: './cliente-carteira-lista.component.html',
  styleUrls: ['./cliente-carteira-lista.component.scss']
})
export class ClienteCarteiraListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public carteiras: Carteira[] = [];
  public subsetoresFiltrados: Subsetor[] = [];
  public subsetorId = Guid.createEmpty();

  setorId: Guid;
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

  public onFiltroAcionado(evento: any) {
    this.filtroLista = evento.filtro;
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

    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      this.setorId = Guid.createEmpty();
    else {

      this.setorId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());
      //this.setorId = +this.activatedRouter.snapshot.paramMap.get('id')!;

      const observer = {
        next: (_subsetores: Subsetor[]) => {
          this.subsetores = _subsetores;
          this.subsetoresFiltrados = this.subsetores;

          if (this.subsetores !== null && this.subsetores.length > 0) {
            this.setorDescricao = this.subsetores.find(s => s.setorId == this.setorId).setor.descricao;
          }

        },
        error: (error: any) => {
          this.toastr.error('Erro ao carregar a tela...', 'Error"');
        },
        complete: () => {}
      }

      this.subsetorService.getSubsetoresBySetorId(this.setorId).subscribe(observer).add(() => {this.spinner.hide()});

    }
  }

  openModal(event: any, template: TemplateRef<any>, subsetorId: Guid): void {
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

  public editarSubsetor(id: Guid): void {
    this.router.navigate([`setores/subsetordetalhe/${id}`])
  }

  public listarSegmentos(id: Guid): void {
    this.router.navigate([`setores/listarsegmentos/${id}`])
  }

}
