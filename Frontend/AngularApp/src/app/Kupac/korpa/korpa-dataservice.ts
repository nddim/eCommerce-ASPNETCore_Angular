import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../../assets/moj-config";
import {Observable} from "rxjs";

@Injectable({providedIn:'root'})
export class KorpaDataservice{
  constructor(private http:HttpClient) {
  }

  public getAll(){
    var url=MojConfig.adresa_local+"korpa/getall";
    return this.http.get<Korpa>(url);
  }

  public dodajUKorpu(proizvodId:any, kolicina=1):Observable<any>{
    var url=MojConfig.adresa_local+"korpa/dodaj";
    var obj={
      proizvodId:proizvodId,
      kolicina:kolicina
    };
    return this.http.post(url, obj);
  }
  public updateKorpa(proizvodId:any, kolicina:number){
    var url=MojConfig.adresa_local+"korpa/update-kolicina";
    var obj={
      proizvodId:proizvodId,
      kolicina:kolicina
    };
    return this.http.post(url,obj);
  }
  public removeFromKorpa(proizvodId:any){
    var url=MojConfig.adresa_local+"korpa/removefromcart?Id="+proizvodId.toString();
    return this.http.delete(url);
  }
}

export interface Artikal{
  id:number,
  proizvod:any,
  kolicina:number,
  cijenaKolicina:number,
  pocetnaKolicina:number
}

export interface Korpa{
  artikli:Artikal[],
  ukupno:number,
}
