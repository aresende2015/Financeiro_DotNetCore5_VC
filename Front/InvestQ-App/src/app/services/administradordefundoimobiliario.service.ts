import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AdministradorDeFundoImobiliario } from '@app/models/AdministradorDeFundoImobiliario';
import { environment } from '@environments/environment';
import { Guid } from 'guid-typescript';
import { Observable, take } from 'rxjs';

@Injectable()

export class AdministradordefundoimobiliarioService {

  baseURL = environment.apiURL + 'api/administradordefundoimobiliario';

  constructor(private http: HttpClient) { }

  public getAllAdministradoresDeFundosImobiliarios(): Observable<AdministradorDeFundoImobiliario[]> {
    return this.http.get<AdministradorDeFundoImobiliario[]>(this.baseURL);
  }

  public getAdministradorDeFundoImobiliarioById(id: Guid): Observable<AdministradorDeFundoImobiliario> {
    return this.http.get<AdministradorDeFundoImobiliario>(`${this.baseURL}/${id}`);
  }

  public post(administradorDeFundoImobiliario: AdministradorDeFundoImobiliario): Observable<AdministradorDeFundoImobiliario> {
    return this.http
      .post<AdministradorDeFundoImobiliario>(this.baseURL, administradorDeFundoImobiliario)
      .pipe(take(1));
  }

  public put(administradorDeFundoImobiliario: AdministradorDeFundoImobiliario): Observable<AdministradorDeFundoImobiliario> {
    return this.http
      .put<AdministradorDeFundoImobiliario>(`${this.baseURL}/${administradorDeFundoImobiliario.id}`, administradorDeFundoImobiliario)
      .pipe(take(1));
  }

  public deleteAdministradorDeFundoImobiliario(id: Guid): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

}
