import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {MojConfig} from "../../../assets/moj-config";
import {VijestPaged} from "../../vijest/vijest-dataservice";

@Injectable({providedIn: 'root'})
export class AdminVijestDataService {
  constructor(private http: HttpClient) {
  }

  public GetVijestiAll(){
    var url = MojConfig.adresa_local+'vijesti/getall';
    return this.http.get<Vijesti>(url);
  }
  public DodajVijest(vijest:VijestDodaj){
    var url = MojConfig.adresa_local+'vijesti/dodaj';
    return this.http.post(url, vijest);
  }
  public editVijest(vijest:VijestEdit){
    var url = MojConfig.adresa_local+'vijesti/edit';
    return this.http.post(url, vijest);
  }
  public GetVijestBydId(id:number){
    var url = MojConfig.adresa_local+'vijest/'+id.toString();
    return this.http.get<VijestId>(url);
  }
  public editSlika(slika:VijestSlika){
    let url = MojConfig.adresa_local+'vijesti/slika-update';
    return this.http.post(url, slika);
  }
  public deleteVijest(id:number){
    let url = MojConfig.adresa_local+'vijest/delete/'+id.toString();
    return this.http.delete(url);
  }
  public getPaged(page=1, pageSize=10){
    let url=MojConfig.adresa_local+"vijesti/getall?PageNumber="+page.toString()
      +"&PageSize="+pageSize.toString();
    return this.http.get<VijestPaged>(url);
  }
}
export type Vijesti = Vijest[];
export interface Vijest{
  id:number
  naziv: string
  datum: string
  tekst: string
  slikaUrl: string
  brojKlikova: number
}
export interface VijestDodaj {
  naziv: string
  tekst: string
  slika: string
}

export interface VijestSlika{
  id:number
  slika:string
}
export interface VijestEdit {
  id: number
  naziv: string
  tekst: string
}
export interface VijestId {
  id: number
  naziv: string
  datum: string
  tekst: string
  autor: string
  slikaUrl: string
  brojKlikova: number
}
