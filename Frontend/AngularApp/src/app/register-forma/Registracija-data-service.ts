import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../assets/moj-config";

@Injectable({providedIn:'root'})
export class RegistracijaDataService{
  private readonly apiUrl = MojConfig.adresa_local;
  constructor(private httpClient:HttpClient) { }
  registerAccount(dataToSend:RegisterPostRequest){
    let api=this.apiUrl+'auth/register';
    return this.httpClient.post(api, dataToSend);
  }
}
export interface RegisterPostRequest {
  ime:string,
  prezime:string,
  email:string,
  lozinka:string,
  potvrdiLozinku:string
}

export interface RegisterPostResponse {
  uredu:boolean
}
