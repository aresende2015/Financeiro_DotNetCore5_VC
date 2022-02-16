import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Segmento } from '@app/models/Segmento';
import { Observable, take } from 'rxjs';
import { Subsetor } from './../models/Subsetor';

@Injectable()
export class SegmentoService {

  baseURL = 'https://localhost:5001/api/segmento';

  constructor(private http: HttpClient) { }

  public getSegmentosBySubsetorId(subsetorId: number): Observable<Segmento[]> {
    return this
            .http
            .get<Segmento[]>(`${this.baseURL}/${subsetorId}`)
            .pipe(take(1));
  }

  public getSegmentoBySubsetorIdSegmentoId(subsertorId: number, segmentoId: number): Observable<any> {
    return this.http
        .get(`${this.baseURL}/${subsertorId}/${segmentoId}`)
        .pipe(take(1));
  }

  public put(subsetorId: number, segmentos: Segmento[]): Observable<Segmento[]> {
    return this.http
      .put<Segmento[]>(`${this.baseURL}/${subsetorId}`, segmentos)
      .pipe(take(1));
  }

  public deleteSegmento(subsetorId: number, segmentoId: number): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${subsetorId}/${segmentoId}`)
      .pipe(take(1));
  }

}
