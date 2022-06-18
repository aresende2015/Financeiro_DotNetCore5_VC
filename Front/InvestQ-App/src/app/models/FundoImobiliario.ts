import { Guid } from "guid-typescript";
import { TipoDeInvestimento } from './TipoDeInvestimento';
import { SegmentoAnbima } from '@app/models/SegmentoAnbima';
import { AdministradorDeFundoImobiliario } from '@app/models/AdministradorDeFundoImobiliario';
import { Ativo } from "./Ativo";
import { TipoDeGestao } from "./Enum/TipoDeGestao.enum";

export class FundoImobiliario {
  id: Guid;
  cnpj: string;
  razaoSocial: string;
  descricao: string;
  dataInicio: Date;
  dataFim: Date;
  tipoDeGestao: TipoDeGestao;
  tipoDeInvestimentoId: Guid;
  tipoDeInvestimento: TipoDeInvestimento;
  segmentoAnbimaId: Guid;
  segmentoAnbima: SegmentoAnbima;
  administradorDeFundoImobiliarioId: Guid;
  adminsitradorDeFundoImobiliario: AdministradorDeFundoImobiliario;
}
