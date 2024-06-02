import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {
  AuthJwtLogin2faRequest,
  AuthJwtLoginRequest,
  AuthJwtLoginResponse,
  AuthJwtRegisterRequest,
  AuthJwtRegisterResponse
} from "./Interfaces/AuthJwtInterfaces";
import {map, Observable} from "rxjs";
import {MojConfig} from "../../assets/moj-config";
import {AuthJwtHelper} from "./AuthJwtHelper";

@Injectable({providedIn:'root'})
export class AuthJwtDataService{
  apiUrl=MojConfig.adresa_local;
  constructor(private http:HttpClient, private helper:AuthJwtHelper) {
  }

  login(loginReq:AuthJwtLoginRequest):Observable<AuthJwtLoginResponse>{
    var url=this.apiUrl+"api/Authentication/Login";
    return this.http.post<AuthJwtLoginResponse>(url, loginReq).pipe(
      map(response=>{
        if(response.success && response.status!="2fa"){
          this.helper.storeJwtToken(response.token);
          this.helper.storeRefreshToken(response.refreshToken);
        }
        return response;
      }))
    }

  login2fa(loginReq:AuthJwtLogin2faRequest):Observable<AuthJwtLoginResponse>{
    var url=this.apiUrl+"api/Authentication/Login-2fa";
    return this.http.post<AuthJwtLoginResponse>(url, loginReq).pipe(
      map(response=>{
        if(response.success){
          this.helper.storeJwtToken(response.token);
          this.helper.storeRefreshToken(response.refreshToken);
        }
        return response;
      }))
  }
    register(registerReq:AuthJwtRegisterRequest):Observable<AuthJwtRegisterResponse>{
    var url=this.apiUrl+"api/Authentication/Register";
    return this.http.post<AuthJwtRegisterResponse>(url, registerReq).pipe(
      map(response=>{
        return response;
      })
    );
    }

  refreshToken(){
    var url=this.apiUrl+"api/Authentication/refresh";
    return this.http.get(url);
  }
  forgotPassword(email:string){
    let api=MojConfig.adresa_local+"auth/forgot-password";
    let obj={
      Email:email
    };
    return this.http.post(api, obj);
  }

  revokeToken(){
    var url=this.apiUrl+"api/Authentication/revoke?id="+this.helper.getId();
    this.helper.signOut();
    return this.http.delete(url);
  }
  removeConnection(){
    var url = this.apiUrl+"api/Authentication/remove-connection";
    return this.http.get(url);
  }
  }

