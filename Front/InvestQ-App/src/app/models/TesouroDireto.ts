import { Guid } from "guid-typescript";
import { TipoDeInvestimento } from "./TipoDeInvestimento";

export class TesouroDireto {
  id: Guid;
  descricao: string;
  dataDeVencimento: Date;
  jurosSemestrais: boolean;
  tipoDeInvestimentoId: Guid;
  tipoDeInvestimento: TipoDeInvestimento;
}
