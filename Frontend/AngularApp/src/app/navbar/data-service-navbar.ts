import { HttpClient } from '@angular/common/http';
import {Injectable} from "@angular/core";
import {MojConfig} from "../../assets/moj-config";
import { ProizvodGetAllResponseList } from "../admin-site/proizvodi/proizvod-get-all-response";
import {ProizvodiSearchResults} from "../pretraga-proizvoda/pretraga-proizvoda-klase";


@Injectable({
  providedIn: 'root'
})

export class DataServiceNavbar {
  private readonly apiUrl = MojConfig.adresa_local;

  constructor(private readonly http: HttpClient) { }

  /*getData(urlNastavak:string) {
    let api=this.apiUrl+urlNastavak;
    return this.http.get<ProizvodGetAllResponseList>(api);
  }

  getProizvodiSearchInput(urlNastavak:string){
    let api=this.apiUrl+urlNastavak;
    return this.http.get<ProizvodiSearchResults[]>(api);
  }*/
  public addKonekciju(userid:string, conenctionId:string){
    var url = MojConfig.adresa_local+'signal-r/add-konekciju';
    var obj:Konekcija = {
      userId: userid,
      connectionId: conenctionId
    }

    return this.http.post(url, obj);
  }
  public removeConnection(userId:any){
    var url = MojConfig.adresa_local+'signal-r/remove-connection?UserId='+userId.toString();
    return this.http.delete(url);
  }
}
export interface Konekcija {
  userId: string
  connectionId: string
}
