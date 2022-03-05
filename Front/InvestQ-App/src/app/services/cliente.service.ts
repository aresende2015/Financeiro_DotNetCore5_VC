import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaginatedResult } from '@app/models/pagination/Pagination';
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

  // sem paginação
  // public getAllClientes(): Observable<Cliente[]> {
  //   return this.http
  //     .get<Cliente[]>(this.baseURL )
  //     .pipe(take(1));
  // }

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
