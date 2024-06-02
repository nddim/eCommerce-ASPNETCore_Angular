import { HttpClient } from '@angular/common/http';
import {Injectable} from "@angular/core";
import {MojConfig} from "../../assets/moj-config";
import {
  ProizvodGetAllResponseList
} from "../admin-site/proizvodi/proizvod-get-all-response";
import {
  BrendGetAll,
  PotkategorijeSidebar, ProizvodiSearchResults,
  SortiranjaResponseGetAll,
  SortiranjaResponseList
} from "./pretraga-proizvoda-klase";

@Injectable({
  providedIn: 'root'
})

export class DataServicePretragaProizvoda {
  private readonly apiUrl = MojConfig.adresa_local;

  constructor(private readonly http: HttpClient) { }

  getData(urlNastavak:string) {
    let api=this.apiUrl+urlNastavak;
    return this.http.get<ProizvodGetAllResponseList>(api);
  }

  getBrends(urlNastavak:string){
    let api=this.apiUrl+urlNastavak;
    return this.http.get<BrendGetAll[]>(api);
  }

  getPotkategorije(urlNastavak:string){
    let api=this.apiUrl+urlNastavak;
    return this.http.get<PotkategorijeSidebar[]>(api);
  }

  getSortiranja(urlNastavak:string){
    let api=this.apiUrl+urlNastavak;
    return this.http.get<SortiranjaResponseGetAll>(api);
  }

  getProizvodiSearchInput(urlNastavak:string){
    let api=this.apiUrl+urlNastavak;
    return this.http.get<ProizvodiSearchResults[]>(api);
  }

}
