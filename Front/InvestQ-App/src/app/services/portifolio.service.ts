import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TipoDeAtivo } from '@app/models/Enum/TipoDeAtivo.enum';
import { Portifolio } from '@app/models/portifolio';
import { environment } from '@environments/environment';
import { Guid } from 'guid-typescript';
import { Observable, take } from 'rxjs';

@Injectable()

export class PortifolioService {
  baseURL = environment.apiURL + 'api/portifolio';

  constructor(private http: HttpClient) { }

  public getAllPortifoliosByCarteiraId(carteiraId: Guid): Observable<Portifolio[]> {
    return this
              .http
              .get<Portifolio[]>(`${this.baseURL}/${'carteiraid'}/${carteiraId}`)
              .pipe(take(1));
  }

  public getAllPortifoliosByCarteiraIdTipoDeAtivo(carteiraId: Guid, tipoDeAtivo: TipoDeAtivo): Observable<Portifolio[]> {
    return this
              .http
              .get<Portifolio[]>(`${this.baseURL}/${'carteiraid'}/${carteiraId}/${tipoDeAtivo}`)
              .pipe(take(1));
  }

}
