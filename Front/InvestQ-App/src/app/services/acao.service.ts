import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Acao } from '@app/models/Acao';
import { environment } from '@environments/environment';
import { Guid } from 'guid-typescript';
import { Observable, take } from 'rxjs';

@Injectable()

export class AcaoService {

  baseURL = environment.apiURL + 'api/acao';

  constructor(private http: HttpClient) { }

  public getAllAcoes(): Observable<Acao[]> {
    return this.http.get<Acao[]>(this.baseURL);
  }

  public getAcaoById(id: Guid): Observable<Acao> {
    return this.http.get<Acao>(`${this.baseURL}/${id}`);
  }

  public getAcaoByDescricao(descricao: string): Observable<Acao> {
    return this.http.get<Acao>(`${this.baseURL}/${descricao}/descricao`);
  }

  public getAcaoByTipoDeInvestimentoId(tipoDeInvestimentoId: Guid): Observable<Acao[]> {
    return this
            .http
            .get<Acao[]>(`${this.baseURL}/${tipoDeInvestimentoId}`)
            .pipe(take(1));
  }

  public post(acao: Acao): Observable<Acao> {
    return this.http
      .post<Acao>(this.baseURL, acao)
      .pipe(take(1));
  }

  public put(acao: Acao): Observable<Acao> {
    //alert(tesouroDireto.id);
    return this.http
      .put<Acao>(`${this.baseURL}/${acao.id}`, acao)
      .pipe(take(1));
  }

  public deleteAcao(id: Guid): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

}
