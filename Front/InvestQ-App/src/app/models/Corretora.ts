import { Cliente } from "./Cliente";
import { ClienteCorretora } from "./ClienteCorretora";
import { Guid } from 'guid-typescript';
export interface Corretora {
  id: Guid;
  descricao: string;
  imagen: string;
  clientesCorretoras: ClienteCorretora[];
}
