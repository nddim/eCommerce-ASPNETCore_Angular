import {Injectable} from "@angular/core";
import {MyBaseEndpoint} from "../MyBaseEndpoint";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {MojConfig} from "../../../assets/moj-config";
import {AutentifikacijaToken} from "../../helper/auth/autentifikacijaToken";


@Injectable({providedIn: 'root'})
export class AuthGetEndpoint implements  MyBaseEndpoint<void, AuthGetResponse>{
  constructor(public httpClient:HttpClient) { }
  obradi(): Observable<AuthGetResponse> {
    let url=MojConfig.adresa_local+`/auth/get`;
    return this.httpClient.get<AuthGetResponse>(url);
  }
}
export interface AuthLoginRequest {
  korisnickoIme: string;
  lozinka: string;
}
export interface AuthGetResponse {
  autentifikacijaToken: AutentifikacijaToken
  isLogiran: boolean
}
