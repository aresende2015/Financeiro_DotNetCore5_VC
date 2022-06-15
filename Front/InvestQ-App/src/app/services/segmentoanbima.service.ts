import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { Guid } from 'guid-typescript';
import { Observable, take } from 'rxjs';
import { SegmentoAnbima } from './../models/SegmentoAnbima';

@Injectable()

export class SegmentoanbimaService {

  baseURL = environment.apiURL + 'api/segmentoanbima';

  constructor(private http: HttpClient) { }

  public getAllSegmentosAnbimas(): Observable<SegmentoAnbima[]> {
    return this.http.get<SegmentoAnbima[]>(this.baseURL);
  }

  public getSegmentoAnbimaById(id: Guid): Observable<SegmentoAnbima> {
    return this.http.get<SegmentoAnbima>(`${this.baseURL}/${id}`);
  }

  public post(segmentoAnbima: SegmentoAnbima): Observable<SegmentoAnbima> {
    return this.http
      .post<SegmentoAnbima>(this.baseURL, segmentoAnbima)
      .pipe(take(1));
  }

  public put(segmentoAnbima: SegmentoAnbima): Observable<SegmentoAnbima> {
    return this.http
      .put<SegmentoAnbima>(`${this.baseURL}/${segmentoAnbima.id}`, segmentoAnbima)
      .pipe(take(1));
  }

  public deleteSegmentoAnbima(id: Guid): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

}
