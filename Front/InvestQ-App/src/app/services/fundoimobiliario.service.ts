import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FundoImobiliario } from '@app/models/FundoImobiliario';
import { PaginatedResult } from '@app/models/pagination/Pagination';
import { environment } from '@environments/environment';
import { Guid } from 'guid-typescript';
import { map, Observable, take } from 'rxjs';

@Injectable()

export class FundoimobiliarioService {
  baseURL = environment.apiURL + 'api/fundoimobiliario';

  constructor(private http: HttpClient) { }

  public getAllFundosImobiliarios(page?: number, itemsPerPage?: number, term?: string): Observable<PaginatedResult<FundoImobiliario[]>> {
    const paginatedResult: PaginatedResult<FundoImobiliario[]> = new PaginatedResult<FundoImobiliario[]>();

    let params = new HttpParams;

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (term !== null && term !== '')
      params = params.append('term', term);

    return this.http
      .get<FundoImobiliario[]>(this.baseURL, {observe: 'response', params} )
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

  public getFundoImobiliarioById(id: Guid): Observable<FundoImobiliario> {
    return this.http.get<FundoImobiliario>(`${this.baseURL}/${id}`);
  }

  public post(fundoImobiliario: FundoImobiliario): Observable<FundoImobiliario> {
    return this.http
      .post<FundoImobiliario>(this.baseURL, fundoImobiliario)
      .pipe(take(1));
  }

  public put(fundoImobiliario: FundoImobiliario): Observable<FundoImobiliario> {
    return this.http
      .put<FundoImobiliario>(`${this.baseURL}/${fundoImobiliario.id}`, fundoImobiliario)
      .pipe(take(1));
  }

  public deleteFundoImobiliario(id: Guid): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }
}
