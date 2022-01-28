import { Cliente } from "./Cliente";

export interface Corretora {
  id: number;
  descricao: string;
  imagen: string;
  dataDeCriacao?: Date;
  inativo: boolean;
  clientesCorretoras: Cliente[];
}
