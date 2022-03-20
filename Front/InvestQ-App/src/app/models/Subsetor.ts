import { Guid } from "guid-typescript";
import { Segmento } from "./Segmento";
import { Setor } from "./Setor";

export class Subsetor {
  id: Guid;
  descricao: string;
  setorId: Guid;
  setor: Setor;
  segmentos: Segmento[];
}
