import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Carteira } from '@app/models/Carteira';
import { TipoDeMovimentacao } from '@app/models/Enum/TipoDeMovimentacao.enum';
import { Portifolio } from '@app/models/portifolio';
import { CarteiraService } from '@app/services/carteira.service';
import { PortifolioService } from '@app/services/portifolio.service';
import { Guid } from 'guid-typescript';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-portifolios-lista',
  templateUrl: './portifolios-lista.component.html',
  styleUrls: ['./portifolios-lista.component.scss']
})
export class PortifoliosListaComponent implements OnInit {

  public portifolios: Portifolio[] = [];
  public portifoliosFiltrados: Portifolio[] = [];
  public portifolioId = Guid.createEmpty();
  public carteiraId =  Guid.createEmpty();
  public carteiraDescricao: string = '';

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.portifoliosFiltrados = this.filtroLista
                                    ? this.filtrarPortifolios(this.filtroLista)
                                    : this.portifolios;
  }

  public onFiltroAcionado(evento: any) {
    this.filtrarPortifolios(evento.filtro) ;
  }

  filtrarPortifolios(filtrarPor: string): Portifolio[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.portifolios.filter(
      (portifolio: {codigoDoAtivo: string}) =>
      portifolio.codigoDoAtivo.toString().indexOf(filtrarPor) !== -1
    )
  }

  constructor(
    private portifolioService: PortifolioService,
    private carteiraService: CarteiraService,
    private activatedRouter: ActivatedRoute,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit() {
    this.carregarPortifolios();

    setTimeout(() => {
      /** spinner ends after 5 seconds */
      //this.spinner.hide();
    }, 3000);
  }

  public bucarNomeDaCarteira(carteiraId: Guid): void {
    this.carteiraService.getCarteiraById(carteiraId).subscribe({
      next: (carteira: Carteira) => {
        this.carteiraDescricao = carteira.descricao;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao tentar carregar o nome da carteira.', 'Erro!');
        console.error(error)
      },
      complete: () => {this.spinner.hide()}
    });
  }

  public carregarPortifolios(): void {
    this.spinner.show();

    this.carteiraId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

    this.bucarNomeDaCarteira(this.carteiraId);

    const observer = {
      next: (_portifolios: Portifolio[]) => {
        this.portifolios = _portifolios;
        this.portifoliosFiltrados = this.portifolios;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.portifolioService.getAllPortifoliosByCarteiraId(this.carteiraId).subscribe(observer);
  }

}
