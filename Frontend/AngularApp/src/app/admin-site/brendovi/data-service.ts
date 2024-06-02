import { HttpClient } from '@angular/common/http';
import {Injectable} from "@angular/core";
import {BrendoviGetAllResponse, BrendoviGetPaged, BrendoviPostRequest} from "./brendovi-get-all-response";
import {MojConfig} from "../../../assets/moj-config";

@Injectable({
  providedIn: 'root'
})
export class DataServiceBrend {

  private readonly apiUrl = MojConfig.adresa_local;

  constructor(private readonly http: HttpClient) { }

  getData(urlNastavak:string) {
    let api=this.apiUrl+urlNastavak;
    return this.http.get<BrendoviGetAllResponse[]>(api);
  }
  getDataPaged(naziv="",pageNumber=1, pageSize=10 ){
    let url=this.apiUrl+"brendovi-paged?Naziv="+naziv+"&PageNumber="+pageNumber.toString()
    +"&PageSize="+pageSize.toString();
    return this.http.get<BrendoviGetPaged>(url);
  }
  postData(dataToSend:BrendoviPostRequest, urlNastavak:string){
    let api=this.apiUrl+urlNastavak;

    return this.http.post(api, dataToSend);
  }

  deleteData(brendId:number){
    let api=this.apiUrl+"brend-obrisi?BrendId="+brendId.toString();
    return this.http.delete(api);
  }

  editData(brend:BrendoviGetAllResponse){
    let api=this.apiUrl+"brend-update";
    return this.http.post(api, brend)
  }



}

