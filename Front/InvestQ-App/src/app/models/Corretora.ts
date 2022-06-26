import { Guid } from 'guid-typescript';
import { Carteira } from "./Carteira";
export interface Corretora {
  id: Guid;
  descricao: string;
  imagen: string;
  carteiras: Carteira[];
}
