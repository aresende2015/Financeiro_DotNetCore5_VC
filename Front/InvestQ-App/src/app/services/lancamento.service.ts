import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TipoDeAtivo } from '@app/models/Enum/TipoDeAtivo.enum';
import { TipoDeMovimentacao } from '@app/models/Enum/TipoDeMovimentacao.enum';
import { Lancamento } from '@app/models/Lancamento';
import { environment } from '@environments/environment';
import { Guid } from 'guid-typescript';
import { Observable, take } from 'rxjs';

@Injectable()
export class LancamentoService {
  baseURL = environment.apiURL + 'api/lancamento';

  constructor(private http: HttpClient) { }

  public getAllLancamentosByCarteiraId(carteiraId: Guid): Observable<Lancamento[]> {
    return this
              .http
              .get<Lancamento[]>(`${this.baseURL}/${'carteiraid'}/${carteiraId}`)
              .pipe(take(1));
  }

  public getLancamentoById(id: Guid): Observable<Lancamento> {
    return this
              .http
              .get<Lancamento>(`${this.baseURL}/${id}`)
              .pipe(take(1));
  }

  public getLancamentoByCarteiraIdAtivoId(carteiraId: Guid, ativoId: Guid): Observable<any> {
    return this.http
        .get(`${this.baseURL}/${carteiraId}/${ativoId}`)
        .pipe(take(1));
  }

  public post(lancamento: Lancamento): Observable<Lancamento> {
    alert("alex")
    console.log(lancamento);
    return this.http
      .post<Lancamento>(this.baseURL, lancamento)
      .pipe(take(1));
  }

  public put(lancamento: Lancamento): Observable<Lancamento> {
    return this.http
      .put<Lancamento>(`${this.baseURL}/${lancamento.id}`, lancamento)
      .pipe(take(1));
  }

  public deleteLancamento(id: Guid): Observable<any> {
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
