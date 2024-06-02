import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../../assets/moj-config";

@Injectable({ providedIn: 'root' })
export class NarudzbaDetailsDataService {
  constructor(private httpClient: HttpClient) {

  }
  public GetNarudzaDetails(id:string){
    var url = MojConfig.adresa_local+'narudzba/get-details?Id='+id;
    return this.httpClient.get<NarudzbaDetails>(url);
  }
  public GetStavkeNarudzbe(id:string){
    var url = MojConfig.adresa_local+'narudzba-stavke/get-stavke?Id='+id;
    return this.httpClient.get<StavkeNarudzbe>(url);
  }
  ratingInputVrijednost:number=0;
  doodajRating(obj:any){
    var url = MojConfig.adresa_local+'ocjena-dodaj';
    return this.httpClient.post(url, obj);
  }
}
export interface NarudzbaDetails {
  id: number
  kupacId: number
  datumKreiranja: string
  datumPotvrde: any
  datumSlanja: any
  statusNarudzbeId: number
  dostava: string
  ime: string
  prezime: string
  email: string
  adresa: string
  grad: string
  postanskiBroj: string
  drzava: string
  kontaktBroj: string
  komentar: string
  statusPotvrden: boolean
  statusSlanja: boolean
  ukupnaCijena:number
}

export type StavkeNarudzbe = Stavka[]

export interface Stavka {
  kolicina: number
  unitCijena: number
  ukupnaCijena: number
  popust: number
  pocetnaCijena: number
  proizvodId: number
  narudzbaId: number
  proizvodNaziv: string
}

