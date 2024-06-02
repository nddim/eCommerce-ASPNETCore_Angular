import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {EditPostRequest} from "./edit-post-request";
import {MojConfig} from "../../../assets/moj-config";
import {MyAuthService} from "../../helper/auth/MyAuthService";

@Injectable({providedIn:'root'})
export class UpdateKorisnickiRacun{
  private readonly apiUrl=MojConfig.adresa_local;
  constructor(private http:HttpClient) {
  }
  updateAccount(dataToSend:AuthGetDetail){
    let api=this.apiUrl+'auth/update-kupac';
    return this.http.post(api, dataToSend);
  }
  getAccountInfo(){
    let api=this.apiUrl+"api/Authentication/detail";
    return this.http.get<AuthGetDetail>(api);
  }
}
export interface AuthGetDetail {
  ime: string
  prezime: string
  email: string
  brojTelefona: string
  adresa: string
  saljiNovosti: boolean
}

