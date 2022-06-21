import { Guid } from 'guid-typescript';
import { Segmento } from './Segmento';
import { TipoDeInvestimento } from './TipoDeInvestimento';

export class Acao {
  id: Guid;
  descricao: string;
  cnpj: string;
  razaoSocial: string;
  setorId: Guid;
  subSetorId: Guid;
  segmentoId: Guid;
  segmento: Segmento;
  tipoDeInvestimentoId: Guid;
  tipoDeInvestimento: TipoDeInvestimento;
}
