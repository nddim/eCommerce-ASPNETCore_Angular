import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../../assets/moj-config";

@Injectable({providedIn:'root'})
export class NarudzbaDataService {
  constructor(private http: HttpClient) { }

  public getNaruzbe(){
    var url = MojConfig.adresa_local+'narudzba/getusernarudzbe';
    return this.http.get<Narudzbe>(url);
  }
  public getPaged(page:number=1, tableSize:number=10){
    var url=MojConfig.adresa_local+"narudzba/getusernarudzbe?"+
      "Page="+page.toString()+"&TableSize="+tableSize.toString();
    return this.http.get<NarudzbaPaged>(url);
  }
  public otkNarudzbu(id:number){
    var url = MojConfig.adresa_local+'narudzbe/'+id.toString();
    return this.http.post(url, null);
  }

  public getRacun(id:number){
    var url=MojConfig.adresa_local+"narudzba/racun?NarudzbaId="+id.toString();
    return this.http.get<NarudzbaRacun>(url);
  }
}
export type Narudzbe = Narudzba[]

export interface Narudzba {
  id: number
  datumKreiranja: string
  dostava: string
  statusNarudzbe: string
  ukupnaCijena: number
}

export interface NarudzbaRacun{
  file:string,
  filename:string
}

export interface NarudzbaPaged{
  narudzbe:Narudzba[],
  currentPage: number
  totalPages: number
  pageSize: number
  totalCount: number
}
