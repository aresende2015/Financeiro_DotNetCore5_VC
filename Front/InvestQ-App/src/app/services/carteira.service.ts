import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Carteira } from '@app/models/Carteira';
import { environment } from '@environments/environment';
import { Guid } from 'guid-typescript';
import { Observable, take } from 'rxjs';

@Injectable()

export class CarteiraService {

  baseURL = environment.apiURL + 'api/carteira';

  constructor(private http: HttpClient) { }

  public getAllCarteiras(): Observable<Carteira[]> {
    return this.http.get<Carteira[]>(this.baseURL);
  }

  public getCarteiraById(id: Guid): Observable<Carteira> {
    return this.http.get<Carteira>(`${this.baseURL}/${id}`);
  }

  public getCarteirasByClienteId(clienteId: Guid): Observable<Carteira[]> {

    return this
            .http
            .get<Carteira[]>(`${this.baseURL}/${'cliente'}/${clienteId}`)
            .pipe(take(1));
  }

  public post(carteira: Carteira): Observable<Carteira> {
    console.log(carteira);
    return this.http
      .post<Carteira>(this.baseURL, carteira)
      .pipe(take(1));
  }

  public put(carteira: Carteira): Observable<Carteira> {
    console.log(carteira);
    return this.http
      .put<Carteira>(`${this.baseURL}/${carteira.id}`, carteira)
      .pipe(take(1));
  }

  public deleteCarteira(id: Guid): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

}
