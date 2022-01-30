import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cliente } from '../models/Cliente';

@Injectable(
  //{providedIn: 'root'}
)
export class ClienteService {
  baseURL = 'https://localhost:5001/api/cliente';

  constructor(private http: HttpClient) { }

  public getAllClientes(): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(this.baseURL);
  }

  public getClienteById(id: number): Observable<Cliente> {
    return this.http.get<Cliente>(`${this.baseURL}/${id}`);
  }
}
