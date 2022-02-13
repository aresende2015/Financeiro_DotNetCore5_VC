import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Setor } from '@app/models/Setor';
import { Subsetor } from '@app/models/Subsetor';
import { SubsetorService } from '@app/services/subsetor.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-subsetores-lista',
  templateUrl: './subsetores-lista.component.html',
  styleUrls: ['./subsetores-lista.component.scss']
})
export class SubsetoresListaComponent implements OnInit {

  public subsetores: Subsetor[] = [];
  public subsetoresFiltrados: Subsetor[] = [];

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
              private toastr: ToastrService,
              private activatedRouter: ActivatedRoute) { }

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

}
