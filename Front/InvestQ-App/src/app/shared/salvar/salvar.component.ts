// ## Declare esse método no componete pai
// public onClicouEm(evento) {
//   if (evento.botaoClicado === 'cancelar') {
//     this.resetForm();
//   } else {
//     this.salvarAlteracao();
//   }
// }

import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-salvar',
  templateUrl: './salvar.component.html',
  styleUrls: ['./salvar.component.scss']
})
export class SalvarComponent implements OnInit {

  @Input() botaoSalvar: string = '';
  @Input() formValido: boolean = false;

  @Output() clicouEm = new EventEmitter();

  cancelar() {
    this.clicouEm.emit({botaoClicado: this.cancelar.name});
  }

  salvar() {
    this.clicouEm.emit({botaoClicado: this.salvar.name});
  }

  constructor() { }

  ngOnInit(): void {
  }

}
