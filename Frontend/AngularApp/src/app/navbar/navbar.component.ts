import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {Router, RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";
import {ProizvodiSearchResults} from "../pretraga-proizvoda/pretraga-proizvoda-klase";
// import './myscript.js'
import {DataServicePretragaProizvoda} from "../pretraga-proizvoda/data-service-pretraga";
import {GetAuth} from "../helper/auth/get-auth";
import {MyAuthService} from "../helper/auth/MyAuthService";
import {AuthJwtHelper} from "../Auth/AuthJwtHelper";
import {AuthJwtDataService} from "../Auth/AuthJwtDataService";
import {SignalRService} from "../helper/signal-r/signal-r.service";
import {MojConfig} from "../../assets/moj-config";
import {HttpClient} from "@angular/common/http";
import {DataServiceNavbar} from "./data-service-navbar";
import {SnackBar} from "../helper/mat-snackbar/snack-bar";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, RouterLinkActive, RouterOutlet, FormsModule],
  providers:[DataServicePretragaProizvoda],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit{
  constructor(private dataServiceProizvod:DataServicePretragaProizvoda,
              private cdr: ChangeDetectorRef,
              private getAuth:GetAuth,
              private myAuthService:MyAuthService,
              private router:Router,
              private jwt:AuthJwtHelper,
              private auth:AuthJwtDataService,
              private signalRService:SignalRService,
              private http:HttpClient,
              private dataService:DataServiceNavbar,
              private snackBar:SnackBar) {
    this.prikaziProfil=false;
  }
  public searchInput:string="";
  public searchByNameResults:ProizvodiSearchResults[]=[];
  public prikaziProfil:boolean=false;
  ngOnInit(): void {
    if(this.jwt.isLoggedIn()){
      this.prikaziProfil=true;
      this.signalRService.otvori_ws_konekciju();
      // this.dodajKonekciju();
    }
    else{
      this.prikaziProfil=false;
    }
  }

  public izbrisiKonekciju(){
    let userId:string=this.jwt.getId();
    this.dataService.removeConnection(userId).subscribe(x=>{

    });
  }
  public prikaziSearch:boolean=false;
  async ucitajSearchProizvode() {
    if(this.searchInput.length>=3){
      const data= await this.dataServiceProizvod.getProizvodiSearchInput('proizvod-pretraga-search?Naziv='+
        this.searchInput).toPromise();
      this.searchByNameResults=[];
      if(data){
        this.searchByNameResults=data;
      }
      //console.log(this.searchByNameResults);
      this.prikaziSearch=true;
      this.cdr.detectChanges();
    }
    else{
      this.searchByNameResults=[];
      this.prikaziSearch=false;
    }
  }

  public logOut () {
    this.izbrisiKonekciju();
    this.prikaziProfil=false;
    this.auth.revokeToken().subscribe({});
    this.router.navigate(["/login"]);
  }

  async onEnter($event: any) {
    this.router.navigate(['/pretraga-naziv', this.searchInput]);
    this.prikaziSearch=false;
  }

  isLoggedIn(){
    return this.jwt.isLoggedIn();
  }

  getPanel() {
    return this.jwt.getPanel();
  }
}
