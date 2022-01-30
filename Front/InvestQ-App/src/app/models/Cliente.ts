import { Corretora } from "./Corretora";

export interface Cliente {
  id: number;
  cpf: string;
  nomeCompleto: string;
  nome: string;
  sobreNome: string;
  email: string;
  idade: number;
  dataDeNascimento: Date;
  dataDeCriacao?: Date;
  inativo: boolean;
  clientesCorretoras: Corretora[];
}
