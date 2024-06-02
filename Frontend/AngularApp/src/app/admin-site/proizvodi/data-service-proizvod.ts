import {MojConfig} from "../../../assets/moj-config";
import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {ProizvodGetAll, ProizvodGetAllResponse, ProizvodPostRequest} from "./proizvod-get-all-response";
import {PotkategorijaGetAllResponse, PotkategorijaPostRequest} from "../potkategorije/Potkategorija-get-all-response";
import {Observable} from "rxjs";
import {FormsModule} from "@angular/forms";
import {ProizvodPost} from "./proizvodi.component";
import {KategorijaGetAllResponse} from "../kategorije/Kategorija-get-all-response";
@Injectable({
  providedIn: 'root'
})
export class DataServiceProizvod{

  private readonly apiUrl = MojConfig.adresa_local;

  constructor(private readonly http: HttpClient) {

  }
  getById(urlNastavak:string){
    let api=this.apiUrl+urlNastavak;
    return this.http.get<ProizvodGetAllResponse>(api);
  }
  getData(naziv:string, sortiranje:any, pageNumber:any, pageSize:any){
    let api=this.apiUrl+"proizvod-pretraga?Naziv="+naziv+"&SortId="+sortiranje+
      "&PageNumber="+pageNumber+"&PageSize="+pageSize;
    return this.http.get<ProizvodGetAll>(api);
  }
  getDataFilter(url:string, sortiranje:number, pageNumber:any, pageSize:any){
    let api=this.apiUrl+url+"&SortId="+sortiranje+
      "&PageNumber="+pageNumber+"&PageSize="+pageSize;
    return this.http.get<ProizvodGetAll>(api);
  }
  postData(dataToSend:ProizvodPostRequest, urlNastavak:string){
    let api=this.apiUrl+urlNastavak;
    return this.http.post(api, dataToSend);
  }
  deleteData(proizvodId:number){
    let api=this.apiUrl+"proizvod-obrisi?Id="+proizvodId.toString();
    return this.http.delete(api);
  }
  editData(proizvod:ProizvodGetAllResponse){
    let api=this.apiUrl+"proizvod-update";
    return this.http.post(api, proizvod)
  }

  postFile(fileToUpload:File, id:string){
    const formData=new FormData();
    formData.append('Slika', fileToUpload);
    formData.append('Id', id);

    const headers=new HttpHeaders().append('Content-Disposition', 'multipart/form-data'); //Disposition

    return this.http.post(this.apiUrl+"upload-slike", formData, {headers});
  }

  getProizvod(id:any){
    var url=MojConfig.adresa_local+"proizvod/getbyid?Id="+id.toString();
    return this.http.get<ProizvodGetById>(url);
  }

  editProizvod(proizvod:ProizvodEdit){
    let api=this.apiUrl+"proizvod-update";
    return this.http.post(api, proizvod)
  }

  postProizvod(proizvod:ProizvodPost){
    let url=MojConfig.adresa_local+"proizvod/dodaj";
    return this.http.post(url, proizvod);
  }

  editSlika(proizvod:ProizvodSlika){
    let url=MojConfig.adresa_local+"proizvod/slika-update";
    return this.http.post(url, proizvod);
  }

  getKategorijeFilter(idGlavna:number){
    let url=MojConfig.adresa_local+"kategorija-pretraga?GlavnaKategorijaID="+idGlavna.toString();
    return this.http.get<KategorijaGetAllResponse[]>(url);
  }

    getPotkategorijeFilter(idKategorija:number){
        let url=MojConfig.adresa_local+"potkategorija-pretraga?KategorijaID="+idKategorija.toString();
        return this.http.get<PotkategorijaGetAllResponse[]>(url);
    }

    getAnalitika(id:any, brojDanaId=1){
      let url=MojConfig.adresa_local+"proizvodi/analitika?ProizvodId="+id.toString()+"&BrojDanaId="+brojDanaId.toString();
      return this.http.get<ProizvodAnalitika>(url);
    }

    getSimilarProducts(id:any){
    let url=MojConfig.adresa_local+"similarproducts?Id="+id.toString();
    return this.http.get<ProizvodGetAllResponse[]>(url);
    }

    deleteGalleryPhoto(id:any){
    let url=MojConfig.adresa_local+"proizvodslika?Id="+id.toString();
    return this.http.delete(url);
    }
    addGalleryPhoto(obj:SlikaGalleryPost){
    let url=MojConfig.adresa_local+"proizvodslika";
    return this.http.post(url, obj);
    }
  getGalleryPhotos(id:any){
    let url=MojConfig.adresa_local+"proizvodslika?ProizvodId="+id.toString();
    return this.http.get<SlikaDTO[]>(url);
  }
}

export interface ProizvodGetById {
  id: number
  naziv: string
  pocetnaKolicina: number
  pocetnaCijena: number
  opis: string
  brojKlikova: number
  potkategorijaID: number
  brendID: number
  slikaUrl: string
  popust:number,
  slikeGalerija:SlikaDTO[]|null
}
export interface SlikaDTO{
  id:number,
  slikaUrl:string
}
export interface SlikaGalleryPost{
  proizvodId:number,
  slika:string
}
export interface ProizvodSlika{
  id:number
  slika:string
}
export interface ProizvodEdit {
  id: number
  naziv: string
  pocetnaKolicina: number
  pocetnaCijena: number
  opis: string
  brojKlikova: number
  potkategorijaID: number
  brendID: number
  popust:number
}

export interface ProizvodAnalitika {
  proizvodId: number
  naziv: string
  datumi: string[]
  brojKlikova: number[]
  dani:Dani[]
}

export interface Dani{
  id:number,
  naziv:string,
  datumPocetka:string
}
