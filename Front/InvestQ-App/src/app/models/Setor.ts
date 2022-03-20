import { Guid } from "guid-typescript";
import { Subsetor } from "./Subsetor";

export class Setor {
  id: Guid;
  descricao: string;
  subsetores: Subsetor[];
}
