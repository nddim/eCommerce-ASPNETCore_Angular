import {Injectable} from "@angular/core";
import {AuthLoginEndpoint} from "../../Endpoints/auth endpoints/auth-login-endpoint";
import {AuthGetEndpoint} from "../../Endpoints/auth endpoints/auth-get-endpoint";
import {AuthLogoutEndpoint} from "../../Endpoints/auth endpoints/auth-logout-endpoint";
import {AuthLoginRequest} from "./authLoginRequest";
import {Observable, tap} from "rxjs";
import {AuthLoginResponse} from "./authLoginResponse";
import {AutentifikacijaToken} from "./autentifikacijaToken";
import {EditPostRequest} from "../../Kupac/kupac-postavke/edit-post-request";


@Injectable({providedIn: 'root'})
export class MyAuthService{
  constructor(
    private authLoginEndpoint: AuthLoginEndpoint,
    private authLogoutEndpoint: AuthLogoutEndpoint,
  ) {
  }


  signIn(loginRequest:AuthLoginRequest):Observable<AuthLoginResponse> {
    return this.authLoginEndpoint.obradi(loginRequest)
      .pipe(
        tap(r=>{
          this.setLogiraniKorisnik(r.autentifikacijaToken);
        })
      );
  }



  async signOut(): Promise<void> {
    const token = this.getAuthorizationToken();

    if (token) {
      try {
        await this.authLogoutEndpoint.obradi(token.vrijednost).toPromise();
      } catch (err) {
        // Handle error if needed
      }
      this.setLogiraniKorisnik(null);
    }
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

  isAdmin():boolean {

    return this.getAuthorizationToken()?.korisnickiRacun.isAdmin ?? false
  }
  isKupac():boolean{
    return this.getAuthorizationToken()?.korisnickiRacun.isKupac ?? false
  }



  is2FActive():boolean {
    return this.getAuthorizationToken()?.korisnickiRacun.is2FActive ?? false
  }

  setLogiraniKorisnik(x: AutentifikacijaToken | null) {

    if (x == null){
      window.localStorage.setItem("my-auth-token", '');
    }
    else {
      window.localStorage.setItem("my-auth-token", JSON.stringify(x));
    }
  }

  twofaactivate(key:string){
    return this.authLoginEndpoint.activate2fa(key);
  }

  updateKupac(x:EditPostRequest) {
    var auth=this.getAuthorizationToken();

    auth!.korisnickiRacun.ime=x.ime;
    auth!.korisnickiRacun.prezime=x.prezime;
    auth!.korisnickiRacun.email=x.email;
    auth!.korisnickiRacun.adresa=x.adresa;
    auth!.korisnickiRacun.brojTelefona=x.brojTelefona;
    auth!.korisnickiRacun.saljiNovosti=x.saljiNovosti;
    //console.log("novi:", auth);

    this.setLogiraniKorisnik(null);
    this.setLogiraniKorisnik(auth);
  }
}
