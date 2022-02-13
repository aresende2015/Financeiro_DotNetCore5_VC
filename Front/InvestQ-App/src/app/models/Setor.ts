import { Subsetor } from "./Subsetor";

export class Setor {
  id: number;
  descricao: string;
  dataDeCriacao?: Date;
  inativo: boolean;
  subsetores: Subsetor[];
}
