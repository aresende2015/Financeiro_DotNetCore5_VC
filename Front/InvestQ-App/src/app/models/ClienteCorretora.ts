import { Cliente } from "./Cliente";
import { Corretora } from "./Corretora";
import { Guid } from 'guid-typescript';

export interface ClienteCorretora {

  clienteId: Guid;
  cliente: Cliente;
  corretoraId: Guid;
  corretora: Corretora;
}
