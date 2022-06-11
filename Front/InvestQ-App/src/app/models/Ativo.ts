import { Guid } from "guid-typescript";
import { TipoDeAtivo } from "./Enum/TipoDeAtivo.enum";
import { TesouroDireto } from '@app/models/TesouroDireto';

export class Ativo {
  id: Guid;
  //tipoDeAtivo: TipoDeAtivo;
  //acaoId?: Guid;
  //acao: Acao;
  //fundoImobiliarioId?: Guid;
  //fundoImobiliario: FundoImobiliario;
  tesouroDiretoId?: Guid;
  //tesouroDireto: TesouroDireto;
}
