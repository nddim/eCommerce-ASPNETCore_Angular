import {Injectable} from "@angular/core";
import {jwtDecode} from "jwt-decode";

@Injectable({providedIn:'root'})
export class AuthJwtHelper{
  constructor() {
  }
  storeJwtToken(token:string){
    localStorage.setItem('jwt', token);
  }

  storeRefreshToken(token:string){
    localStorage.setItem('refresh', token);
  }

  getToken=():string | null=>localStorage.getItem("jwt") || '';
  getRefreshToken=():string =>localStorage.getItem("refresh") || '';
  removeToken=():void=> localStorage.removeItem("jwt");
  removeRefreshToken=():void=> localStorage.removeItem("refresh");

  isLoggedIn=():boolean=>{
    const token=this.getToken();
    if(!token) return false;

    return true;
  }

  getId(){
    var user= this.getUserDetail();
    if (typeof user === 'object' && user !== null && 'id' in user) {
      return user.id;
    }
    return "";
  }
  isTokenExpired(){
    const token=this.getToken();
    if(!token) return true;

    const decoded=jwtDecode(token);
    const isTokenExpired=Date.now()>=decoded['exp']!*1000;
    if(isTokenExpired) this.removeToken();
    return isTokenExpired;
  }

  getRole(){
    const token=this.getToken();
    if(token==null || token =="") return "";
    const decodedToken:any=jwtDecode(token);
    const role=decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || "";
    return role.toString();
  }

  getUserDetail=()=>{
    const token=this.getToken();
    if(!token)return true;
    const decodedToken:any=jwtDecode(token);
    const korisnik={
      id:decodedToken.nameid,
      name:decodedToken.name,
      email:decodedToken.email,
      role:decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
    }
    return korisnik;
  }

  getPanel() {
    var role=this.getRole();
    if(role==null || role=="")
      return "/login";
    return "/"+role.toLowerCase()+"-panel";
  }

  getEmail(){
    var user= this.getUserDetail();
    if (typeof user === 'object' && user !== null && 'email' in user) {
      const email = user.email;
      return email;
    }
    return "";
  }
  getIme(){
    var user= this.getUserDetail();
    if (typeof user === 'object' && user !== null && 'name' in user) {
      const name = user.name;
      return name;
    }
    return "";
  }

  signOut(){
    this.removeToken();
    this.removeRefreshToken();
  }
}
