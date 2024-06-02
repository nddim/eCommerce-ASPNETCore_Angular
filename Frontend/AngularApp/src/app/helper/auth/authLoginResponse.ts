import {AutentifikacijaToken} from "./autentifikacijaToken";

export interface AuthLoginResponse {
  autentifikacijaToken: AutentifikacijaToken
  isLogiran: boolean
}
