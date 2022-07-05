import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Lancamento } from '@app/models/Lancamento';
import { environment } from '@environments/environment';
import { Guid } from 'guid-typescript';
import { Observable, take } from 'rxjs';

@Injectable()
export class LancamentoService {
  baseURL = environment.apiURL + 'api/lancamento';

  constructor(private http: HttpClient) { }

  public getAllLancamentosByCarteiraId(carteiraId: Guid): Observable<Lancamento[]> {
    return this
              .http
              .get<Lancamento[]>(`${this.baseURL}/${'carteiraid'}/${carteiraId}`)
              .pipe(take(1));
  }

  public getLancamentoById(id: Guid): Observable<Lancamento> {
    return this
              .http
              .get<Lancamento>(`${this.baseURL}/${id}`)
              .pipe(take(1));
  }

  public getLancamentoByCarteiraIdAtivoId(carteiraId: Guid, ativoId: Guid): Observable<any> {
    return this.http
        .get(`${this.baseURL}/${carteiraId}/${ativoId}`)
        .pipe(take(1));
  }

  public post(lancamento: Lancamento): Observable<Lancamento> {
    return this.http
      .post<Lancamento>(this.baseURL, lancamento)
      .pipe(take(1));
  }

  public put(lancamento: Lancamento): Observable<Lancamento> {
    return this.http
      .put<Lancamento>(`${this.baseURL}/${lancamento.id}`, lancamento)
      .pipe(take(1));
  }

  public deleteLancamento(id: Guid): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }
}
