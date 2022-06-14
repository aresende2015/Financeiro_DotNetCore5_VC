import { Guid } from "guid-typescript";
//import { FundoImobiliario } from "./FundoImobiliario";

export interface AdministradorDeFundoImobiliario {
  id: Guid;
  cnpj: string;
  razaoSocial: string;
  telefone: string;
  site: string;
  email: string;
  //fundosImobiliarios: FundoImobiliario[];
}
