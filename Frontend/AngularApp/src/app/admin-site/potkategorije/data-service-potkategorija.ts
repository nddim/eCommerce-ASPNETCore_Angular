import { HttpClient } from '@angular/common/http';
import {Injectable} from "@angular/core";
import {
  PotkategorijaGetAllResponse,
  PotkategorijaPaged,
  PotkategorijaPostRequest
} from "./Potkategorija-get-all-response";
import {MojConfig} from "../../../assets/moj-config";

@Injectable({
  providedIn: 'root'
})
export class DataServicePotkategorija {

  private readonly apiUrl = MojConfig.adresa_local;

  constructor(private readonly http: HttpClient) { }

  getData(urlNastavak:string) {
    let api=this.apiUrl+urlNastavak;
    return this.http.get<PotkategorijaGetAllResponse[]>(api);
  }

  getPaged(naziv="", kategorijaId:number, page=1, pageSize=10){
    let url=this.apiUrl+'potkategorije-paged?Naziv='+
      naziv+ "&KategorijaID="+kategorijaId.toString()+
      "&PageNumber="+page.toString()
      +"&PageSize="+pageSize.toString();
    return this.http.get<PotkategorijaPaged>(url);
  }

  postData(dataToSend:PotkategorijaPostRequest, urlNastavak:string){
    let api=this.apiUrl+urlNastavak;

    return this.http.post(api, dataToSend);
  }

  deleteData(potkategorijId:number){
    let api=this.apiUrl+"potkategorija-obrisi?Id="+potkategorijId.toString();
    return this.http.delete(api);
  }

  editData(potkategorija:PotkategorijaGetAllResponse){
    let api=this.apiUrl+"potkategorija-update";
    return this.http.post(api, potkategorija)
  }



}

