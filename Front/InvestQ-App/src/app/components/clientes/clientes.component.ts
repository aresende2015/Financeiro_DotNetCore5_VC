import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.scss']
})
export class ClientesComponent implements OnInit {

  public clientes: any = [];
  public clientesFiltrados: any = [];

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.clientesFiltrados = this.filtroLista ? this.filtrarClientes(this.filtroLista) : this.clientes;
  }

  filtrarClientes(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.clientes.filter(
      ( cliente: { nomeCompleto: string; cpf: string; }) =>
            cliente.nomeCompleto.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
            cliente.cpf.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getClientes();
  }

  public getClientes(): void {
    this.http.get('https://localhost:5001/api/cliente').subscribe(
      response => {
        this.clientes = response
        this.clientesFiltrados = this.clientes;
      },
      error => console.log(error)
    );
  }

}
