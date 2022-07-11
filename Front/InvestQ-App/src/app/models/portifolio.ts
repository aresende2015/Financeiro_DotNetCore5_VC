import { Guid } from "guid-typescript";
import { Ativo } from "./Ativo";
import { Carteira } from "./Carteira";

export class Portifolio {
  id: Guid;
  ativoId: Guid;
  quantidade: number;
  codigoDoAtivo: string;
  precoMedio: number;
  ativo: Ativo;
  carteiraId: Guid;
  carteira: Carteira;
}
