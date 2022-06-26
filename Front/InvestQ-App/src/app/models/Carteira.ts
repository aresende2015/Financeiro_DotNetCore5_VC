import { Cliente } from "./Cliente";
import { Corretora } from "./Corretora";
import { Guid } from 'guid-typescript';

export interface Carteira {
  id: Guid;
  Descricao: string;
  saldo: number;
  dataDeAtualizadoDoSaldo: Date;
  clienteId: Guid;
  cliente: Cliente;
  corretoraId: Guid;
  corretora: Corretora;
}
