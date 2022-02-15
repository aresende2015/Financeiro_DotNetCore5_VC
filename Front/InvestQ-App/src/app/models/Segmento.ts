import { Subsetor } from "./Subsetor";

export class Segmento {
  id: number;
  descricao: string;
  dataDeCriacao?: Date;
  inativo: boolean;
  subsetorId: number;
  subsetor: Subsetor;
}
