import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subsetor } from '@app/models/Subsetor';
import { Guid } from 'guid-typescript';
import { Observable, take } from 'rxjs';

@Injectable()
export class SubsetorService {

  baseURL = 'https://localhost:5001/api/subsetor';

  constructor(private http: HttpClient) { }

  public getSubsetoresBySetorId(setorId: Guid): Observable<Subsetor[]> {

    return this
            .http
            .get<Subsetor[]>(`${this.baseURL}/${'setor'}/${setorId}`)
            .pipe(take(1));
  }

  public getSubsetorById(subsetorId: Guid): Observable<Subsetor> {
    return this
            .http
            .get<Subsetor>(`${this.baseURL}/${subsetorId}`)
            .pipe(take(1));
  }

  public put(setorId: Guid, subsetores: Subsetor[]): Observable<Subsetor[]> {
    return this.http
      .put<Subsetor[]>(`${this.baseURL}/${setorId.toString()}`, subsetores)
      .pipe(take(1));
  }

  public deleteSubsetor(setorId: Guid, subsetorId: Guid): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${setorId}/${subsetorId}`)
      .pipe(take(1));
  }

}
