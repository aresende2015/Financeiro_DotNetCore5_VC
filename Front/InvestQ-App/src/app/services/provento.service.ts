import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Observable, take } from 'rxjs';

import { Provento } from '@app/models/Provento';

@Injectable()

export class ProventoService {
  baseURL = 'https://localhost:5001/api/provento';

  constructor(private http: HttpClient) { }

  public getAllProventos(): Observable<Provento[]> {
    return this.http.get<Provento[]>(this.baseURL);
  }

  public getProventosById(id: Guid): Observable<Provento> {
    return this
            .http
            .get<Provento>(`${this.baseURL}/${id}`)
            .pipe(take(1));
  }

  public post(provento: Provento): Observable<Provento> {
    return this.http
      .post<Provento>(this.baseURL, provento)
      .pipe(take(1));
  }

  public put(provento: Provento): Observable<Provento> {
    return this.http
      .put<Provento>(`${this.baseURL}/${provento.id}`, provento)
      .pipe(take(1));
  }

  public deleteProvento(id: Guid): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }
}
