import { Setor } from "./Setor";

export class Subsetor {
  id: number;
  descricao: string;
  dataDeCriacao?: Date;
  inativo: boolean;
  setorId: number;
  setor: Setor;
}
