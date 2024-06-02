import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../../assets/moj-config";
import {Narudzba} from "./kupac-checkout.component";

@Injectable({providedIn:'root'})
export class KupacDataservice {
  constructor(private http: HttpClient) {
  }
  public getAll(){
    var url=MojConfig.adresa_local+"narudzba/get-userinfo";
    return this.http.get<KorisnikInfo>(url);
  }
  public napraviNarudzbu(obj:Narudzba){
    //console.log(obj);
    var url=MojConfig.adresa_local+"narudzba/dodaj";
    return this.http.post<Narudzba>(url, obj);
  }
}

export interface KorisnikInfo {
  id:string
  ime: string
  prezime: string
  email: string
  adresa: string
  kontaktBroj: string
}


