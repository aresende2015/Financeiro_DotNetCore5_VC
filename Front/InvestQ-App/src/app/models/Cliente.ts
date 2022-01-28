import { Corretora } from "./Corretora";

export interface Cliente {
  id: number;
  cpf: string;
  nome: string;
  sobreNome: string;
  email: string;
  dataDeNascimento: Date;
  dataDeCriacao?: Date;
  inativo: boolean;
  clientesCorretoras: Corretora[];
}
