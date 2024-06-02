import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../../assets/moj-config";
import {Observable} from "rxjs";

@Injectable({providedIn:'root'})
export class GoogleDataService{
  constructor(private http:HttpClient) {
  }

  externalLogin(obj:GoogleAuthDto):Observable<GoogleAuthResponse>{
    var url=MojConfig.adresa_local+"api/Authentication/google-login";
    return this.http.post<GoogleAuthResponse>(url, obj);
  }


}

export interface GoogleAuthDto{
  provider: string;
  idToken: string;
}
export interface GoogleAuthResponse{
  token: string;
  isAuthSuccessful: string;
  refreshToken:string
}
