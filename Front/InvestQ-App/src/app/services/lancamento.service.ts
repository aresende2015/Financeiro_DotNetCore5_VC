import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TipoDeAtivo } from '@app/models/Enum/TipoDeAtivo.enum';
import { TipoDeMovimentacao } from '@app/models/Enum/TipoDeMovimentacao.enum';
import { Lancamento } from '@app/models/Lancamento';
import { PaginatedResult } from '@app/models/pagination/Pagination';
import { environment } from '@environments/environment';
import { Guid } from 'guid-typescript';
import { map, Observable, take } from 'rxjs';

@Injectable()
export class LancamentoService {
  baseURL = environment.apiURL + 'api/lancamento';

  constructor(private http: HttpClient) { }

  public getAllLancamentosByCarteiraId(carteiraId: Guid, page?: number, itemsPerPage?: number, term?: string): Observable<PaginatedResult<Lancamento[]>> {

    const paginatedResult: PaginatedResult<Lancamento[]> = new PaginatedResult<Lancamento[]>();

    let params = new HttpParams;

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (term !== null && term !== '')
      params = params.append('term', term);

    return this
              .http
              .get<Lancamento[]>(`${this.baseURL}/${'carteiraid'}/${carteiraId}`, {observe: 'response', params})
              .pipe(
                take(1),
                map((response) => {
                  paginatedResult.result = response.body;
                  if(response.headers.has('Pagination')) {
                    paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
                  }
                  return paginatedResult;
                })
              );
  }

  public getLancamentoById(id: Guid): Observable<Lancamento> {
    return this
              .http
              .get<Lancamento>(`${this.baseURL}/${id}`)
              .pipe(take(1));
  }

  public getLancamentoByCarteiraIdAtivoId(carteiraId: Guid, ativoId: Guid, page?: number, itemsPerPage?: number, term?: string): Observable<PaginatedResult<Lancamento[]>> {

    const paginatedResult: PaginatedResult<Lancamento[]> = new PaginatedResult<Lancamento[]>();

    let params = new HttpParams;

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (term !== null && term !== '')
      params = params.append('term', term);

    return this.http
        .get<Lancamento[]>(`${this.baseURL}/${carteiraId}/${ativoId}`, {observe: 'response', params})
        .pipe(
          take(1),
          map((response) => {
            paginatedResult.result = response.body;
            if(response.headers.has('Pagination')) {
              paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
            }
            return paginatedResult;
          })
        );
  }

  public getPossuiLancamentoByCarteiraId(carteiraId: Guid, possuiLancamento: boolean): Observable<any> {
    return this.http
              .get(`${this.baseURL}/${'possuilancamento'}/${carteiraId}/${possuiLancamento}`)
              .pipe(take(1));
  }

  public post(lancamento: Lancamento): Observable<Lancamento> {
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
      // {valor: TipoDeMovimentacao.Deposito, desc: 'Deposito' },
      // {valor: TipoDeMovimentacao.Retirada, desc: 'Retirada' },
      // {valor: TipoDeMovimentacao.Dividendos, desc: 'Dividendos' },
      // {valor: TipoDeMovimentacao.JCP, desc: 'JCP' },
      // {valor: TipoDeMovimentacao.Rendimentos, desc: 'Rendimentos' },
      // {valor: TipoDeMovimentacao.Bonificacao, desc: 'Bonificacao' }
    ];
  }

  getTipoDeAtivo() {
    return [
      {valor: TipoDeAtivo.NaoInformada, desc: 'NaoInformada' },
      //{valor: TipoDeAtivo.Caixa, desc: 'Caixa' },
      {valor: TipoDeAtivo.Acao, desc: 'Acao' },
      {valor: TipoDeAtivo.FundoImobiliario, desc: 'FundoImobiliario' },
      {valor: TipoDeAtivo.TesouroDireto, desc: 'TesouroDireto' }
    ];
  }
}
