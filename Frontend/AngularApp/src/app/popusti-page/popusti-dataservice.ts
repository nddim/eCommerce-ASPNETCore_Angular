import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {ProizvodGetAll, ProizvodGetAllResponse} from "../admin-site/proizvodi/proizvod-get-all-response";
import {MojConfig} from "../../assets/moj-config";
import {ProizvodGetAllResponseListPaged} from "../proizvod-pretraga-naziv/data-service-pretraga";

@Injectable({providedIn:'root'})
export class PopustiDataservice{
  constructor(private http:HttpClient) {
  }

  getData(naziv:string){
    let api=MojConfig.adresa_local+naziv;
    return this.http.get<ProizvodGetAllResponse[]>(api);
  }


  getPaged(proizvodPopusti: string, page: number=1, tableSize: number=6, trenutnoSortiranje: number) {
    let api=MojConfig.adresa_local+proizvodPopusti+"?Page="+page.toString()
    +"&TableSize="+tableSize.toString()+"&SortID="+trenutnoSortiranje.toString();
    return this.http.get<ProizvodGetAllResponseListPaged>(api);
  }
}
