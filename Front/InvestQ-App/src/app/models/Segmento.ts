import { Subsetor } from "./Subsetor";
import { Guid } from 'guid-typescript';

export class Segmento {
  id: Guid;
  descricao: string;
  subsetorId: Guid;
  subsetor: Subsetor;
}
