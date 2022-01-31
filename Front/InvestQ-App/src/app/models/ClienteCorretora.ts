import { Cliente } from "./Cliente";
import { Corretora } from "./Corretora";

export interface ClienteCorretora {

  clienteId: number;
  cliente: Cliente;
  corretoraId: number;
  corretora: Corretora;
}
