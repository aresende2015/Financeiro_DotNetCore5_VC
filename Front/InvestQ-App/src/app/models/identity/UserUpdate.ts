import { TipoDeUsuario } from "../Enum/TipoDeUsuario.enum";

export class UserUpdate {
  username: string;
  primeiroNome: string;
  ultimoNome: string;
  email: string;
  phoneNumber: string;
  funcao: TipoDeUsuario;
  password: string;
  token: string;
}
