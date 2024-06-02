import { HttpClient } from '@angular/common/http';
import {Injectable} from "@angular/core";
import {KategorijaGetAllResponse, KategorijaPaged, KategorijaPostRequest} from "./Kategorija-get-all-response";
import {MojConfig} from "../../../assets/moj-config";

@Injectable({
  providedIn: 'root'
})
export class DataServiceKategorija {

  private readonly apiUrl = MojConfig.adresa_local;

  constructor(private readonly http: HttpClient) { }

  getData(urlNastavak:string) {
    let api=this.apiUrl+urlNastavak;
    return this.http.get<KategorijaGetAllResponse[]>(api);
  }
  getPaged(naziv="", id:number, page=1, pageSize=10){
    let api=this.apiUrl+'kategorije-paged?Naziv='+naziv+'&GlavnaKategorijaID='+id.toString()+
      "&PageNumber="+page.toString()
      +"&PageSize="+pageSize.toString();
    return this.http.get<KategorijaPaged>(api);
  }
  postData(dataToSend:KategorijaPostRequest, urlNastavak:string){
    let api=this.apiUrl+urlNastavak;

    return this.http.post(api, dataToSend);
  }

  deleteData(kategorijaId:number){
    let api=this.apiUrl+"kategorija-obrisi?Id="+kategorijaId.toString();
    return this.http.delete(api);
  }

  editData(glavnaKategorija:KategorijaGetAllResponse){
    let api=this.apiUrl+"kategorija-update";
    return this.http.post(api, glavnaKategorija)
  }

  getKategorije(){
    let api=this.apiUrl+"potkategorije/pocetna";
    return this.http.get<KategorijaResponse[]>(api);
  }


}

export interface KategorijaResponse{
  id:number,
  naziv:string
}

