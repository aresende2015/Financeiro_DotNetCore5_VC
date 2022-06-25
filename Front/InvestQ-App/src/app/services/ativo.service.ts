import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Ativo } from '@app/models/Ativo';
import { TipoDeAtivo } from '@app/models/Enum/TipoDeAtivo.enum';
import { environment } from '@environments/environment';
import { Guid } from 'guid-typescript';
import { Observable, take } from 'rxjs';

@Injectable()

export class AtivoService {

  baseURL = environment.apiURL + 'api/Ativo';

  constructor(private http: HttpClient) { }

  public getAllAtivosByTipoDeAtivo(tipoDeAtivo: TipoDeAtivo): Observable<Ativo[]> {
    //alert(tipoDeAtivo.valueOf());
    return this
            .http
            .get<Ativo[]>(`${this.baseURL}/${tipoDeAtivo}`)
            .pipe(take(1));
  }

  public getAtivoByTipoDeAtivoDescricao(tipoDeAtivo: TipoDeAtivo, descricao: string): Observable<Ativo> {
    return this
            .http
            .get<Ativo>(`${this.baseURL}/${tipoDeAtivo}/${descricao}`)
            .pipe(take(1));
  }

  public post(ativo: Ativo): Observable<Ativo> {
    return this.http
      .post<Ativo>(this.baseURL, ativo)
      .pipe(take(1));
  }

  public put(ativo: Ativo): Observable<Ativo> {
    return this.http
      .put<Ativo>(`${this.baseURL}/${ativo.id}`, ativo)
      .pipe(take(1));
  }

  public deleteAtivo(id: Guid): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

}
