import { Cliente } from "./Cliente";
import { ClienteCorretora } from "./ClienteCorretora";
export interface Corretora {
  id: number;
  descricao: string;
  imagen: string;
  dataDeCriacao?: Date;
  inativo: boolean;
  clientesCorretoras: ClienteCorretora[];
}
