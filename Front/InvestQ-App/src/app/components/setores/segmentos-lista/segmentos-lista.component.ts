import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Segmento } from '@app/models/Segmento';
import { SegmentoService } from '@app/services/segmento.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-segmentos-lista',
  templateUrl: './segmentos-lista.component.html',
  styleUrls: ['./segmentos-lista.component.scss']
})
export class SegmentosListaComponent implements OnInit {

  public segmentos: Segmento[] = [];
  public segmentosFiltrados: Segmento[] = [];

  subsetorId: number;
  subsetorDescricao: string;

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.segmentosFiltrados = this.filtroLista
                              ? this.filtrarSegmentos(this.filtroLista)
                              : this.segmentos;
  }

  filtrarSegmentos(filtrarPor: string): Segmento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.segmentos.filter(
      (segmento: {descricao: string}) =>
        segmento.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(private segmentoService: SegmentoService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService,
              private activatedRouter: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.spinner.show();

    this.carregarSegmentos();
  }

  public carregarSegmentos(): void {
    this.subsetorId = +this.activatedRouter.snapshot.paramMap.get('id')!;

    const observer = {
      next: (_segmentos: Segmento[]) => {
        this.segmentos = _segmentos;
        this.segmentosFiltrados = this.segmentos;

        this.subsetorDescricao = this.segmentos.find(s => s.subsetorId === this.subsetorId).subsetor.descricao;

      },
      error: (error: any) => {
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
        console.error(error);
      },
      complete: () => {}
    }

    this.segmentoService.getSegmentosBySubsetorId(this.subsetorId).subscribe(observer).add(() => {this.spinner.hide()});
  }

}
