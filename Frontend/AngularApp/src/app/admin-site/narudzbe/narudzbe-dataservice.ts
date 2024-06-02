import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../../assets/moj-config";

@Injectable({providedIn:'root'})
export class AdministratorNarudzbeDataService{
  constructor(private http: HttpClient) { }

  public getNaruzbe(){
    var url = MojConfig.adresa_local+'narudzbe/getall';
    return this.http.get<Narudzbe>(url);
  }
  public obrisiNarudzbu(id:number){
    var url = MojConfig.adresa_local+'narudzba-izbrisi/'+id.toString();
    return this.http.post(url, null);
  }
  public getStatus(){
    var url = MojConfig.adresa_local+'status-narudzbe/get';
    return this.http.get<Status>(url);
  }
  public getUserStatus(id:number){
    var url = MojConfig.adresa_local+'status-narudzbe/'+id.toString();
    return this.http.get(url);
  }
  public getKupacNarudzba(id:number){
      var url = MojConfig.adresa_local+'narudzba-admin/'+id.toString();
      return this.http.get<narudzbaKupac>(url);
  }
  public editNarudzba(body:Edit){
    var url = MojConfig.adresa_local+'narudzba/edit';
    return this.http.post(url, body);
  }

  public getNarudzbePaged(page:number=1, tableSize:number=5, korisnikIme:string=""){
    let url=MojConfig.adresa_local+"narudzbe/getall?Page="+page.toString()
    +"&TableSize="+tableSize.toString()+"&Korisnik="+korisnikIme;
    return this.http.get<NarudzbeGetPaged>(url);
  }
}
export type Status = Statusi[]
export interface Edit {
    id: number
    statusNarudzbeId: number
}
export interface Statusi {
    id: number
    status: string
}

export type Narudzbe = Narudzba[]

export interface Narudzba {
  narudzbaId: number
  kupacId: number
  ime: string
  prezime: string
  email: string
  datumKreiranja: string
  datumIsporuke: any
  datumPotvrde: any
  ukupnaCijena: number
  narudzbaStatus: string
}

export interface narudzbaKupac {
    narudzbaId: number
    kupacId: number
    ime: string
    prezime: string
    email: string
}

//------
export interface NarudzbeGetPaged {
  narudzbe: NarudzbePaged[]
  currentPage: number
  totalPages: number
  pageSize: number
  totalCount: number
}

export interface NarudzbePaged {
  narudzba: NarudzbaPaged
  stavkeNarudzbe: StavkeNarudzbePaged[]
}

export interface NarudzbaPaged {
  narudzbaId: number
  kupacId: string
  ime: string
  prezime: string
  email: string
  datumKreiranja: string
  datumIsporuke?: string
  datumPotvrde?: string
  ukupnaCijena: number
  narudzbaStatus: string
}

export interface StavkeNarudzbePaged {
  narudzbaId:number,
  id: number
  proizvodId: number
  naziv: string
  kolicina: number
  cijenaProizvod: number
  zbirnoCijena: number
}
