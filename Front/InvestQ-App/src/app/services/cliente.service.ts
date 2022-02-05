import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Cliente } from '../models/Cliente';

@Injectable(
  //{providedIn: 'root'}
)
export class ClienteService {
  baseURL = 'https://localhost:5001/api/cliente';

  constructor(private http: HttpClient) { }

  public getAllClientes(): Observable<Cliente[]> {
    return this.http
      .get<Cliente[]>(this.baseURL)
      .pipe(take(1));
  }

  public getClienteById(id: number): Observable<Cliente> {
    return this.http
      .get<Cliente>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  public post(cliente: Cliente): Observable<Cliente> {
    return this.http
      .post<Cliente>(this.baseURL, cliente)
      .pipe(take(1));
  }

  public put(cliente: Cliente): Observable<Cliente> {
    return this.http
      .put<Cliente>(`${this.baseURL}/${cliente.id}`, cliente)
      .pipe(take(1));
  }

  public deleteCliente(id: number): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

}
