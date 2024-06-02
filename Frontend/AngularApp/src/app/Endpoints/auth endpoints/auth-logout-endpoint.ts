import {Injectable} from "@angular/core";
import {MyBaseEndpoint} from "../MyBaseEndpoint";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {MojConfig} from "../../../assets/moj-config";


@Injectable({providedIn: 'root'})
export class AuthLogoutEndpoint implements  MyBaseEndpoint<string, void>{
  constructor(public httpClient:HttpClient) { }

  obradi(tokenValue: string): Observable<void> {
    let url=MojConfig.adresa_local+`auth/logout`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'my-auth-token': tokenValue });
    return this.httpClient.post<void>(url, {}, {'headers': headers});
  }


}
