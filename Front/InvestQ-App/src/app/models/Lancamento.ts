import { Guid } from "guid-typescript";
import { Carteira } from "./Carteira";
import { Ativo } from "./Ativo";
import { TipoDeMovimentacao } from "./Enum/TipoDeMovimentacao.enum";
import { TipoDeOperacao } from "./Enum/TipoDeOperacao.enum";

export class Lancamento {
  id: Guid;
  valorDaOperacao: number;
  dataDaOperacao: Date;
  quantidade: number;
  tipoDeMovimentacao: TipoDeMovimentacao;
  tipoDeOperacao: TipoDeOperacao;
  ativoId: Guid;
  ativo: Ativo;
  carteiraId: Guid;
  carteira: Carteira;
}
