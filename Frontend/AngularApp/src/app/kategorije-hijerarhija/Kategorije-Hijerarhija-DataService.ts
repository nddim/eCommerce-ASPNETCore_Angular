import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../assets/moj-config";

@Injectable({providedIn:'root'})
export class KategorijeHijerarhijaDataService{
  constructor(private http:HttpClient) {
  }
  private apiUrl=MojConfig.adresa_local;

  public getKategorije(){
    let url=this.apiUrl+"kategorije/hijerarhija";
    return this.http.get<GlavnaKategorijaResponse>(url);
  }
}

export interface GlavnaKategorijaResponse {
  glavneKategorije: GlavnaKategorija[];
}

export interface GlavnaKategorija {
  id: number;
  naziv: string;
  kategorije: Kategorija[];
}

export interface Kategorija {
  id: number;
  naziv: string;
  potkategorije: Potkategorija[];
}

export interface Potkategorija {
  id: number;
  naziv: string;
}
