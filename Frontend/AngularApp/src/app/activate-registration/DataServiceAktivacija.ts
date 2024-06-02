import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {BrendoviGetAllResponse} from "../admin-site/brendovi/brendovi-get-all-response";
import {MojConfig} from "../../assets/moj-config";

@Injectable({providedIn:'root'})
export class DataServiceAktivacija{
  apiUrl:string=MojConfig.adresa_local;
  constructor(private http:HttpClient) {
  }

  postActivationCode(urlNastavak:string, data:RegisterActivateRequest) {
    let api=this.apiUrl+urlNastavak;
    return this.http.post(api, data);
  }
}
export interface RegisterActivateRequest{
  activateCode:string
}
export interface RegisterActivateResponse{
  uredu:boolean
}
