import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Corretora } from '../models/Corretora';
import { environment } from '@environments/environment';

@Injectable(
  //{providedIn: 'root'}
)
export class CorretoraService {
  baseURL = environment.apiURL + 'api/corretora';

  constructor(private http: HttpClient) { }

  public getAllCorretoras(): Observable<Corretora[]> {
    return this.http.get<Corretora[]>(this.baseURL);
  }

  public getCorretoraByDescricao(descricao: string): Observable<Corretora> {
    return this.http.get<Corretora>(`${this.baseURL}/${descricao}/descricao`);
  }

  public getCorretoraById(id: number): Observable<Corretora> {
    return this.http.get<Corretora>(`${this.baseURL}/${id}`);
  }

  public post(corretora: Corretora): Observable<Corretora> {
    return this.http
      .post<Corretora>(this.baseURL, corretora)
      .pipe(take(1));
  }

  public put(corretora: Corretora): Observable<Corretora> {
    return this.http
      .put<Corretora>(`${this.baseURL}/${corretora.id}`, corretora)
      .pipe(take(1));
  }

  public deleteCorretora(id: number): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  public postUpload(corretoraId: number, file: File): Observable<Corretora> {
    const fileToUpload = file[0] as File;

    const formData = new FormData();

    formData.append('file', fileToUpload);

    return this.http
      .post<Corretora>(`${this.baseURL}/upload-image/${corretoraId}`, formData)
      .pipe(take(1));
  }

}
