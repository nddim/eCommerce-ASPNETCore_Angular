import { HttpClient } from '@angular/common/http';
import {Injectable} from "@angular/core";
import {GlavnaKategorijaGetAllResponse, GlavnaKategorijaPostRequest} from "./glavna-kategorija-get-all-response";
import {MojConfig} from "../../../assets/moj-config";

@Injectable({
  providedIn: 'root'
})
export class DataServiceGlavnaKategorija {

  private readonly apiUrl = MojConfig.adresa_local;

  constructor(private readonly http: HttpClient) { }

  getData(urlNastavak:string) {
    let api=this.apiUrl+urlNastavak;
    return this.http.get<GlavnaKategorijaGetAllResponse[]>(api);
  }

  postData(dataToSend:GlavnaKategorijaPostRequest, urlNastavak:string){
    let api=this.apiUrl+urlNastavak;

    return this.http.post(api, dataToSend);
  }

  deleteData(glavnaKategorijaId:number){
    let api=this.apiUrl+"glavna-kategorija-obrisi?Id="+glavnaKategorijaId.toString();
    return this.http.delete(api);
  }

  editData(glavnaKategorija:GlavnaKategorijaGetAllResponse){
    let api=this.apiUrl+"glavna-kategorija-update";
    return this.http.post(api, glavnaKategorija)
  }



}

