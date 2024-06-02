import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../../assets/moj-config";

@Injectable({providedIn:'root'})
export class KorisniciDataservice{
  constructor(private http:HttpClient) {  }

  getAllUsers(){
    let url=MojConfig.adresa_local+"korisnici";
    return this.http.get<KorisnikGetAll[]>(url);
  }
  getAllAdmins(){
    let url=MojConfig.adresa_local+"admini";
    return this.http.get<KorisnikGetAll[]>(url);
  }
  deleteUser(id:string){
    let url=MojConfig.adresa_local+"korisnici/"+id.toString();
    return this.http.delete(url);
  }

  editUser(s:KorisnikEdit){
    let url=MojConfig.adresa_local+"korisnici";

    // console.log("Uredujemo studenta: "+s.id+s.ime+s.prezime+s.brojTelefona+s.email+s.saljiNovosti+s.isActivated);
    return this.http.put(url, s);
  }
  addUser(s:KorisnikPost){
    let url=MojConfig.adresa_local+"korisnici";
    return this.http.post(url, s);
  }


}


export interface KorisnikGetAll {
  id: string
  ime: string
  prezime: string
  datumKreiranja: string
  email: string
  brojTelefona: string
  isActivated: boolean
  saljiNovosti: boolean
  is2fa:boolean
}

export interface KorisnikEdit {
  id: string
  ime: string
  prezime: string
  email: string
  brojTelefona: string
  isActivated: boolean
  saljiNovosti: boolean
  is2fa:boolean
}
export interface NoviKorisnik {
  ime: string
  prezime: string
  email: string
  brojTelefona: string
  isActivated: boolean
  saljiNovosti: boolean
  is2fa:boolean
}

export interface KorisnikPost {
  ime: string
  prezime: string
  email: string
  isActivated: boolean
  saljiNovosti: boolean
  isAdmin: boolean
  isKupac: boolean
  is2fa:boolean
}
