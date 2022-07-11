import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Carteira } from '@app/models/Carteira';
import { CarteiraService } from '@app/services/carteira.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-portifolio-filtro-lista',
  templateUrl: './portifolio-filtro-lista.component.html',
  styleUrls: ['./portifolio-filtro-lista.component.scss']
})
export class PortifolioFiltroListaComponent implements OnInit {

  public carteiras: Carteira[] = [];

  constructor(private carteiraService: CarteiraService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService,
              private router: Router)
  { }

  ngOnInit(): void {
    this.carregarCarteiras();
  }

  public carregarCarteiras(): void {
    const observer = {
    next: (_carteiras: Carteira[]) => {
      this.carteiras = _carteiras;
      //console.log(this.carteiras);
      //console.log(this.carteiras[0].clienteId);
    },
    error: (error: any) => {
      this.spinner.hide();
      console.error(error);
      this.toastr.error('Erro ao carregar a tela...', 'Error"');
    },
      complete: () => {this.spinner.hide()}
    }

    this.carteiraService.getAllCarteiras().subscribe(observer);
  }

  public selecionarCarteira(evento): void {
    //alert(evento.target.value);
    //this.carregarAtivos(evento.target.value);
    this.router.navigate([`portifolios/lista/${evento.target.value}`])
  }

}
