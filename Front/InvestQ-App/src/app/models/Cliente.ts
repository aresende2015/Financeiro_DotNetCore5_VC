import { Guid } from "guid-typescript";
import { Carteira } from "./Carteira";

export interface Cliente {
  id: Guid;
  cpf: string;
  nomeCompleto: string;
  nome: string;
  sobreNome: string;
  email: string;
  idade: number;
  dataDeNascimento: Date;
  carteria: Carteira[];
}
