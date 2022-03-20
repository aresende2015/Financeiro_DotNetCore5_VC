import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Segmento } from '@app/models/Segmento';
import { Guid } from 'guid-typescript';
import { Observable, take } from 'rxjs';

@Injectable()
export class SegmentoService {

  baseURL = 'https://localhost:5001/api/segmento';

  constructor(private http: HttpClient) { }

  public getSegmentosBySubsetorId(subsetorId: Guid): Observable<Segmento[]> {
    return this
            .http
            .get<Segmento[]>(`${this.baseURL}/${subsetorId}`)
            .pipe(take(1));
  }

  public getSegmentoBySubsetorIdSegmentoId(subsertorId: Guid, segmentoId: Guid): Observable<any> {
    return this.http
        .get(`${this.baseURL}/${subsertorId}/${segmentoId}`)
        .pipe(take(1));
  }

  public putSegmentos(subsetorId: Guid, segmentos: Segmento[]): Observable<Segmento[]> {
    return this.http
      .put<Segmento[]>(`${this.baseURL}/${subsetorId}`, segmentos)
      .pipe(take(1));
  }

  public putSegmento(subsetorId: Guid, segmentoId: Guid, segmento: Segmento): Observable<Segmento> {
    return this.http
      .put<Segmento>(`${this.baseURL}/${subsetorId}/${segmentoId}`, segmento)
      .pipe(take(1));
  }

  public deleteSegmento(subsetorId: Guid, segmentoId: Guid): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${subsetorId}/${segmentoId}`)
      .pipe(take(1));
  }

}
