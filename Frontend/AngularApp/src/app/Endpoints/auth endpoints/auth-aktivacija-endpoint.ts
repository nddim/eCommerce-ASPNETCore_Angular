import {MyBaseEndpoint} from "../MyBaseEndpoint";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {MojConfig} from "../../../assets/moj-config";
@Injectable({providedIn: 'root'})
export class AuthAktivacijaEndpoint implements  MyBaseEndpoint<AuthAktivacijaRequest, void>{
  constructor(public httpClient:HttpClient) { }

  obradi(request: AuthAktivacijaRequest): Observable<void> {
    let url=MojConfig.adresa_local+`/auth/aktivacija`;
    return this.httpClient.post<void>(url,request);
  }
}
export interface AuthAktivacijaRequest {
  nesto:string
}
