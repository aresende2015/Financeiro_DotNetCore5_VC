import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TesouroDireto } from '@app/models/TesouroDireto';
import { environment } from '@environments/environment';
import { Guid } from 'guid-typescript';
import { Observable, take } from 'rxjs';

@Injectable()
export class TesourodiretoService {

  baseURL = environment.apiURL + 'api/tesourodireto';

  constructor(private http: HttpClient) { }

  public getAllTesourosDiretos(): Observable<TesouroDireto[]> {
    return this.http.get<TesouroDireto[]>(this.baseURL);
  }

  public getTesouroDiretoById(id: Guid): Observable<TesouroDireto> {
    return this.http.get<TesouroDireto>(`${this.baseURL}/${id}`);
  }

  public getTesouroDiretoByDescricao(descricao: string): Observable<TesouroDireto> {
    return this.http.get<TesouroDireto>(`${this.baseURL}/${descricao}/descricao`);
  }

  public getTesourosDiretosByTipoDeInvestimentoId(tipoDeInvestimentoId: Guid): Observable<TesouroDireto[]> {
    return this
            .http
            .get<TesouroDireto[]>(`${this.baseURL}/${tipoDeInvestimentoId}`)
            .pipe(take(1));
  }

  public post(tesouroDireto: TesouroDireto): Observable<TesouroDireto> {
    return this.http
      .post<TesouroDireto>(this.baseURL, tesouroDireto)
      .pipe(take(1));
  }

  public put(tesouroDireto: TesouroDireto): Observable<TesouroDireto> {
    return this.http
      .put<TesouroDireto>(`${this.baseURL}/${tesouroDireto.id}`, tesouroDireto)
      .pipe(take(1));
  }

  public deleteTesouroDireto(id: Guid): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  getJurosSemestrais() {
    return [
      {valor: true, desc: 'Sim' },
      {valor: false, desc: 'Não' }
    ];
  }

}
