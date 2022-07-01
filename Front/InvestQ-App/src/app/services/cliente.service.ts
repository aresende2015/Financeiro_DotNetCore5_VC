import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaginatedResult } from '@app/models/pagination/Pagination';
import { Guid } from 'guid-typescript';
import { map, Observable, take } from 'rxjs';
import { Cliente } from '../models/Cliente';

@Injectable(
  //{providedIn: 'root'}
)
export class ClienteService {
  baseURL = 'https://localhost:5001/api/cliente';

  constructor(private http: HttpClient) { }

  public getAllClientes(page?: number, itemsPerPage?: number, term?: string): Observable<PaginatedResult<Cliente[]>> {
    const paginatedResult: PaginatedResult<Cliente[]> = new PaginatedResult<Cliente[]>();

    let params = new HttpParams;

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (term !== null && term !== '')
      params = params.append('term', term);

    return this.http
      .get<Cliente[]>(this.baseURL, {observe: 'response', params} )
      .pipe(
        take(1),
        map((response) => {
          paginatedResult.result = response.body;
          if(response.headers.has('Pagination')) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        })
      );
  }

  public getAllClientesUser(usuarioLogado: string): Observable<Cliente[]> {
    return this.http
      .get<Cliente[]>(`${this.baseURL}/usuariologado/${usuarioLogado}`)
      .pipe(take(1));
  }

  public getClienteById(id: Guid): Observable<Cliente> {
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

  public deleteCliente(id: Guid): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

}
