// ## Colocar no componente pai
// public onFiltroAcionado(evento: any) {
//   this.filtrarClientes(evento.filtro)   ou this.filtroLista = evento.filtro;
// }
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-filtrar',
  templateUrl: './filtrar.component.html',
  styleUrls: ['./filtrar.component.scss']
})
export class FiltrarComponent implements OnInit {

  @Input() rotaLink: string = '';
  @Input() botaoNovo = true;

  @Output() filtroAcionado = new EventEmitter();

  filtrar(evt: any) {
    this.filtroAcionado.emit({filtro: evt.value});
  }

  constructor() { }

  ngOnInit(): void {
  }

}
