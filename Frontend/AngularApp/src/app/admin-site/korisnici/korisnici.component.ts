import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {KorisniciDataservice, KorisnikEdit, KorisnikGetAll, KorisnikPost, NoviKorisnik} from "./korisnici-dataservice";
import {FormsModule} from "@angular/forms";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-korisnici',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './korisnici.component.html',
  styleUrl: './korisnici.component.css'
})
export class KorisniciComponent implements OnInit{
  constructor(private dataService:KorisniciDataservice,
              private toastr:ToastrService) {
  }
  zadnjiKorisnici=true;
  ngOnInit(): void {
    this.ucitajKorisnike();
  }

  public korisnici:KorisnikGetAll[]=[];
  edit=false;
  odabraniKorisnik:KorisnikEdit={
    id:"",
    isActivated:false,
    saljiNovosti:false,
    brojTelefona:'',
    email:'',
    ime:'',
    prezime:'',
    is2fa:false
  }
  ucitajKorisnike() {
    this.dataService.getAllUsers().subscribe(x=>{
      this.korisnici=x;
      this.zadnjiKorisnici=true;
    })
  }

  odaberiKorisnika(s:KorisnikGetAll) {
    this.odabraniKorisnik.id=s.id;
    this.odabraniKorisnik.ime=s.ime;
    this.odabraniKorisnik.prezime=s.prezime;
    this.odabraniKorisnik.email=s.email;
    this.odabraniKorisnik.isActivated=s.isActivated;
    this.odabraniKorisnik.brojTelefona=s.brojTelefona;
    this.odabraniKorisnik.saljiNovosti=s.saljiNovosti;
    this.odabraniKorisnik.is2fa=s.is2fa;
    this.edit=true;
  }

  obrisiKorisnika(id: string) {
    this.dataService.deleteUser(id.toString()).subscribe(x=>{
      this.toastr.success("Uspjesno obrisan korisnik!");
      this.ucitavanjeKorisnika();
    })
  }

  editujKorisnika() {
    this.dataService.editUser(this.odabraniKorisnik).subscribe(x=>{
      this.toastr.success("Uspjesno modifikovan korisnik");
      this.edit=false;
      this.ucitavanjeKorisnika();
    }, error => {
      this.toastr.error("Desio se neki problem sa ažuriranjem korisnika.");
    })
  }

  novi=false;
  noviKorisnik:KorisnikPost={
    email:"",
    ime:"",
    isActivated:false,
    isAdmin:false,
    isKupac:false,
    saljiNovosti:false,
    prezime:"",
    is2fa:false
  }
  dodajKorisnika(){
    if(this.noviKorisnik.isKupac!=this.noviKorisnik.isAdmin){
      this.dataService.addUser(this.noviKorisnik).subscribe(x=>{
        this.ucitavanjeKorisnika();
        this.novi=false;
        this.toastr.success("Uspjesno dodan korisnik");
      })
    }
    else{
      this.toastr.info("Ne može biti i kupac i admin istovremeno!");
    }

  }

  async onEnter($event: any) {
    this.editujKorisnika();
  }

  ucitajAdmine() {
    this.dataService.getAllAdmins().subscribe(x=>{
      this.korisnici=x;
      this.zadnjiKorisnici=false;
    })
  }

  ucitavanjeKorisnika(){
    if(this.zadnjiKorisnici)
    {
      this.ucitajKorisnike();
    }
    else{
      this.ucitajAdmine();
    }
  }
}
