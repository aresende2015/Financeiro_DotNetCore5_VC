import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Carteira } from '@app/models/Carteira';
import { CarteiraService } from '@app/services/carteira.service';
import { Guid } from 'guid-typescript';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-portifolios-lista',
  templateUrl: './portifolios-lista.component.html',
  styleUrls: ['./portifolios-lista.component.scss']
})
export class PortifoliosListaComponent implements OnInit {

  public carteiraId =  Guid.createEmpty();
  public carteiraDescricao: string = '';

  constructor(
    private carteiraService: CarteiraService,
    private toastr: ToastrService,
    private activatedRouter: ActivatedRoute,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    this.bucarNomeDaCarteira();
  }

  public bucarNomeDaCarteira(): void {

    this.carteiraId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

    this.carteiraService.getCarteiraById(this.carteiraId).subscribe({
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
}
