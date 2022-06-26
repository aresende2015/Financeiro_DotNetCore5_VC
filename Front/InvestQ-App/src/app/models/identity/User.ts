import { TipoDeUsuario } from "../Enum/TipoDeUsuario.enum";

export class User {
  username: string;
  email: string;
  password: string;
  funcao: TipoDeUsuario;
  primeiroNome: string;
  ultimoNome: string;
  token: string;
}
