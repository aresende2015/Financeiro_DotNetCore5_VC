import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-corretoras',
  templateUrl: './corretoras.component.html',
  styleUrls: ['./corretoras.component.scss']
})
export class CorretorasComponent implements OnInit {

  public corretoras: any = [];
  largudaImagem: number = 100;
  margemImagem: number = 2;
  mostrarImagem: boolean = true;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getCorretoras();
  }

  mostrarOcultarImagen() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  public getCorretoras(): void {
    this.http.get('https://localhost:5001/api/corretora').subscribe(
      response => this.corretoras = response,
      error => console.log(error)
    );
  }

}
