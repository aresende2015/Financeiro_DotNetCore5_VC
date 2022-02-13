import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Setor } from '@app/models/Setor';
import { Observable, take } from 'rxjs';

@Injectable()
export class SetorService {
  baseURL = 'https://localhost:5001/api/setor';

  constructor(private http: HttpClient) { }

  public getAllSetores(): Observable<Setor[]> {
    return this.http.get<Setor[]>(this.baseURL);
  }

  public getSetorByDescricao(descricao: string): Observable<Setor> {
    return this.http.get<Setor>(`${this.baseURL}/${descricao}/descricao`);
  }

  public getSetorById(id: number): Observable<Setor> {
    return this.http.get<Setor>(`${this.baseURL}/${id}`);
  }

  public post(setor: Setor): Observable<Setor> {
    return this.http
      .post<Setor>(this.baseURL, setor)
      .pipe(take(1));
  }

  public put(setor: Setor): Observable<Setor> {
    return this.http
      .put<Setor>(`${this.baseURL}/${setor.id}`, setor)
      .pipe(take(1));
  }

  public deleteSetor(id: number): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

}
