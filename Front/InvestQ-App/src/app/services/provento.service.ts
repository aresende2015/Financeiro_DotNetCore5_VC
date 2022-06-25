import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Observable, take } from 'rxjs';

import { Provento } from '@app/models/Provento';
import { TipoDeMovimentacao } from '@app/models/Enum/TipoDeMovimentacao.enum';
import { TipoDeAtivo } from '@app/models/Enum/TipoDeAtivo.enum';

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
    alert('alex');
    return this.http
      .put<Provento>(`${this.baseURL}/${provento.id}`, provento)
      .pipe(take(1));
  }

  public deleteProvento(id: Guid): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  getTipoDeMovimentacao() {
    return [
      {valor: TipoDeMovimentacao.NaoInformada, desc: 'NaoInformada' },
      {valor: TipoDeMovimentacao.Compra, desc: 'Compra' },
      {valor: TipoDeMovimentacao.Venda, desc: 'Venda' },
      {valor: TipoDeMovimentacao.Deposito, desc: 'Deposito' },
      {valor: TipoDeMovimentacao.Retirada, desc: 'Retirada' },
      {valor: TipoDeMovimentacao.Dividendos, desc: 'Dividendos' },
      {valor: TipoDeMovimentacao.JCP, desc: 'JCP' },
      {valor: TipoDeMovimentacao.Rendimentos, desc: 'Rendimentos' },
      {valor: TipoDeMovimentacao.Bonificacao, desc: 'Bonificacao' }
    ];
  }

  getTipoDeAtivo() {
    return [
      {valor: TipoDeAtivo.NaoInformada, desc: 'NaoInformada' },
      {valor: TipoDeAtivo.Caixa, desc: 'Caixa' },
      {valor: TipoDeAtivo.Acao, desc: 'Acao' },
      {valor: TipoDeAtivo.FundoImobiliario, desc: 'FundoImobiliario' },
      {valor: TipoDeAtivo.TesouroDireto, desc: 'TesouroDireto' }
    ];
  }
}
