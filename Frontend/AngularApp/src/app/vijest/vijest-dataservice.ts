import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../assets/moj-config";

@Injectable({providedIn:'root'})
export class VijestDataservice{
  constructor(private http: HttpClient) {
  }
  public getVijesti(){
    var url = MojConfig.adresa_local+'vijesti/getall';
    return this.http.get<Vijesti>(url);
  }
  public getVijestById(id:number){
    var url = MojConfig.adresa_local+'vijest/'+id.toString();
    return this.http.get<VijestId>(url);
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
  tekst: string
  slikaUrl: string
  datum:string
  brojKlikova: number
}

export interface VijestPaged{
  vijesti:Vijest[],
  currentPage: number
  totalPages: number
  pageSize: number
  totalCount: number
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

