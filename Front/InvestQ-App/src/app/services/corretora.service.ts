import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Corretora } from '../models/Corretora';

@Injectable(
  //{providedIn: 'root'}
)
export class CorretoraService {
  baseURL = 'https://localhost:5001/api/corretora';

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

}
