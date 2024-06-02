import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../assets/moj-config";
import {ActivatedRoute} from "@angular/router";

@Injectable({providedIn:'root'})
export class  VijestpageDataservice{

  constructor(private http:HttpClient) { }

  public getVijest(id:string){
    var url = MojConfig.adresa_local+'vijest/'+id;
    return this.http.get<Vijest>(url);
  }
  public getVijestLista(){
    var url = MojConfig.adresa_local+'vijesti/getvijesti-bydate';
    return this.http.get<Vijest2>(url);
  }
}
export interface Vijest {
  id: number
  naziv: string
  datum: string
  tekst: string
  autor: string
  slikaUrl: string
  brojKlikova: number
}

export type Vijest2 = VijestLista[]

export interface VijestLista {
  id: number
  naziv: string
  slikaUrl: string
  datum: string
}
