import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TipoDeInvestimento } from '../models/TipoDeInvestimento';
import { environment } from '@environments/environment';
import { Observable, take } from 'rxjs';
import { Guid } from 'guid-typescript';

@Injectable()

export class TipodeinvestimentoService {

  baseURL = environment.apiURL + 'api/tipodeinvestimento';

  constructor(private http: HttpClient) { }

  public getAllTiposDeInvestimentos(): Observable<TipoDeInvestimento[]> {
    return this.http.get<TipoDeInvestimento[]>(this.baseURL);
  }

  public getTipoDeInvestimentoById(id: Guid): Observable<TipoDeInvestimento> {
    return this.http.get<TipoDeInvestimento>(`${this.baseURL}/${id}`);
  }

  public post(tipoDeInvestimento: TipoDeInvestimento): Observable<TipoDeInvestimento> {
    return this.http
      .post<TipoDeInvestimento>(this.baseURL, tipoDeInvestimento)
      .pipe(take(1));
  }

  public put(tipoDeInvestimento: TipoDeInvestimento): Observable<TipoDeInvestimento> {
    return this.http
      .put<TipoDeInvestimento>(`${this.baseURL}/${tipoDeInvestimento.id}`, tipoDeInvestimento)
      .pipe(take(1));
  }

  public deleteTipoDeInvestimento(id: Guid): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

}
