import { Guid } from "guid-typescript";
import { Corretora } from "./Corretora";

export interface Cliente {
  id: Guid;
  cpf: string;
  nomeCompleto: string;
  nome: string;
  sobreNome: string;
  email: string;
  idade: number;
  dataDeNascimento: Date;
  clientesCorretoras: Corretora[];
}
