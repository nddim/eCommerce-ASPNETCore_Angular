import { HttpClient } from '@angular/common/http';
import {Injectable} from "@angular/core";
import {MojConfig} from "../../assets/moj-config";
import {
  ProizvodGetAllPretragaObject,
  ProizvodGetAllResponseList
} from "../admin-site/proizvodi/proizvod-get-all-response";
import {
  BrendGetAll,
  PotkategorijeSidebar, ProizvodiSearchResults,
  SortiranjaResponseGetAll
} from "../pretraga-proizvoda/pretraga-proizvoda-klase";


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

  getProizvodiNaziv(naziv:string, sortiranje:any, pageNumber:any, pageSize:any) {
    let api=this.apiUrl+"proizvod/getbynaziv?Naziv="+naziv+"&SortId="+sortiranje+
    "&PageNumber="+pageNumber+"&PageSize="+pageSize;
    return this.http.get<ProizvodGetAllResponseListPaged>(api);
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
export interface ProizvodGetAllResponseListPaged{
  proizvodi:ProizvodGetAllPretragaObject[],
  currentPage:number,
  totalPages:number,
  pageSize:number,
  totalCount:number
}
