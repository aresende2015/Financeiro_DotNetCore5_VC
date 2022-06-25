import { Guid } from "guid-typescript";
import { Ativo } from "./Ativo";
import { TipoDeMovimentacao } from "./Enum/TipoDeMovimentacao.enum";
import { TipoDeAtivo } from '@app/models/Enum/TipoDeAtivo.enum';

export class Provento {
  id: Guid;
  dataCom: Date;
  dataEx: Date;
  valor: number;
  ativoId: Guid;
  ativo: Ativo;
  tipoDeAtivo: TipoDeAtivo;
  tipoDeMovimentacao: TipoDeMovimentacao;
}
