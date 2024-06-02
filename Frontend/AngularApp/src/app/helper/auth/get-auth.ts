import {AutentifikacijaToken} from "./autentifikacijaToken";
import {Injectable} from "@angular/core";
import {AuthLoginEndpoint} from "../../Endpoints/auth endpoints/auth-login-endpoint";
import {KorpaDataservice} from "../../Kupac/korpa/korpa-dataservice";

@Injectable({providedIn:'root'})
export class GetAuth{
  constructor(private dataService:AuthLoginEndpoint, private korpaDataService: KorpaDataservice) {
  }
  getAuthorizationToken():AutentifikacijaToken | null {
    let tokenString = window.localStorage.getItem("my-auth-token")??"";
    try {
      return JSON.parse(tokenString);
    }
    catch (e){
      return null;
    }
  }

  isLogiran():boolean{
    return this.getAuthorizationToken() != null;
  }

  async isAdmin ():Promise<boolean> {
    var jelAdmin=false;

    const response=await this.dataService.isAdminProvjera().toPromise();

    //console.log(response?.status);
    if(response){
      if(response.status==200)
      {
        jelAdmin=true;
      }
      else if(response.status==401){
        jelAdmin=false;
      }
      else {
      }
    }

    //console.log("jel admin: ", jelAdmin);
    var jelAdminProvjera=this.getAuthorizationToken()?.korisnickiRacun.isAdmin ?? false;

    return (jelAdmin && jelAdminProvjera);
  }
  async isAdmin2fa ():Promise<boolean> {
    var jelAdmin=false;

    const response=await this.dataService.isAdmin2faProvjera().toPromise();

    if(response){
      if(response.status==200)
      {
        if(response)
        jelAdmin=true;
      }
      else if(response.status==210){
        jelAdmin=false;
      }
      else {
      }
    }



    return (jelAdmin);
  }

  async isKupac():Promise<boolean>{
    var jelKupac=false;

    const response=await this.dataService.isKupacProvjera().toPromise();

    //console.log(response?.status);
    if(response){
      if(response.status==200)
      {
        jelKupac=true;
      }
      else if(response.status==401){
        jelKupac=false;
      }
      else {
      }
    }

    // console.log("jel kupac: ", jelKupac);
    var jelKupacProvjera=this.getAuthorizationToken()?.korisnickiRacun.isKupac ?? false;

    return (jelKupac && jelKupacProvjera);
  }

  async isKupac2fa():Promise<boolean>{
    var jelKupac=false;

    const response=await this.dataService.isKupac2faProvjera().toPromise();

    // console.log(response?.status);
    if(response){
      if(response.status==200)
      {
        jelKupac=true;
      }
      else if(response.status==210){
        jelKupac=false;
      }
      else {
      }
    }
    return (jelKupac);
  }

  async is2FA(){
    var jel2fa=false;

    const response=await this.dataService.is2FAProvjera().toPromise();

    if(response){
      if(response.status==200)
      {
        jel2fa=true;
      }
      else if(response.status==210){
        jel2fa=false;
      }
      else {
      }
    }

    return (jel2fa );
  }

  artikli:[]=[];
  korpa:any;
  async provjeriKorpu(): Promise<boolean> {
    return new Promise<boolean>((resolve, reject) => {
      this.korpaDataService.getAll().subscribe(x => {
        this.korpa = x;
        this.artikli = this.korpa.artikli;
        // console.log(this.artikli.length);
        if (this.artikli.length > 0) {
          resolve(true);
        } else {
          resolve(false);
        }
      }, error => {
        reject(error); // Ukoliko se dogodi greška prilikom dohvaćanja podataka
      });
    });
  }

  is2FActive():boolean {
    return this.getAuthorizationToken()?.korisnickiRacun.is2FActive ?? false
  }
}
