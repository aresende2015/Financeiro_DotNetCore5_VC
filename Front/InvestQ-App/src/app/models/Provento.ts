import { Guid } from "guid-typescript";
import { Ativo } from "./Ativo";

export class Provento {
  id: Guid;
  dataCom: Date;
  dataEx: Date;
  valor: number;
  ativoId: Guid;
  ativo: Ativo;
  //tipoDeMovimento: TipoDeMovimento;
}
