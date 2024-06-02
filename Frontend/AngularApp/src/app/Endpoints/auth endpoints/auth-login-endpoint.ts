import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {MyBaseEndpoint} from "../MyBaseEndpoint";
import {MojConfig} from "../../../assets/moj-config";
import {AutentifikacijaToken} from "../../helper/auth/autentifikacijaToken";

@Injectable({providedIn: 'root'}) //
export class AuthLoginEndpoint implements  MyBaseEndpoint<AuthLoginRequest, AuthLoginResponse>{
  constructor(private readonly httpClient:HttpClient) { }

  obradi(request: AuthLoginRequest): Observable<AuthLoginResponse> {
    let url=MojConfig.adresa_local+`auth/login`;
    return this.httpClient.post<AuthLoginResponse>(url, request);
  }

  isAdminProvjera(){
    let url=MojConfig.adresa_local+`auth/isAdmin`;
    return this.httpClient.get(url, {observe:'response'});
  }
  isAdmin2faProvjera(){
    let url=MojConfig.adresa_local+`auth/isAdmin2fa`;
    return this.httpClient.get(url, {observe:'response'});
  }
  isKupacProvjera(){
    let url=MojConfig.adresa_local+`auth/isKupac`;
    return this.httpClient.get(url, {observe:'response'});
  }
  isKupac2faProvjera(){
    let url=MojConfig.adresa_local+`auth/isKupac2fa`;
    return this.httpClient.get(url, {observe:'response'});
  }
  is2FAProvjera(){
    let url=MojConfig.adresa_local+"auth/is2fa";
    return this.httpClient.get(url, {observe:'response'});
  }
  activate2fa(key:string){
    let url=MojConfig.adresa_local+"twofkey";
    var obj={Key:key};
    return this.httpClient.post(url, obj, {observe:'response'});
  }
}

export interface AuthLoginRequest {
  email: string;
  lozinka: string;
}
export interface AuthLoginResponse {
  autentifikacijaToken: AutentifikacijaToken
  isLogiran: boolean
}
