import {inject, Injectable} from "@angular/core";
import {CanActivateFn, Router} from "@angular/router";
import {PermissionsService} from "../../guards/auth.guard";
import {AuthJwtHelper} from "../AuthJwtHelper";
import {ToastrService} from "ngx-toastr";
import {AuthJwtDataService} from "../AuthJwtDataService";
import {error} from "@angular/compiler-cli/src/transformers/util";

@Injectable({providedIn:'root'})
export class AuthGuard{
    constructor() {
    }
}

export const AuthJwtGuardKupac: CanActivateFn = (route, state) => {
    var authHelper=inject(AuthJwtHelper);
    var dataService=inject(AuthJwtDataService);
    if(authHelper.getRole() === "Kupac") //!authHelper.isTokenExpired() &&
    {
      if(authHelper.isTokenExpired()){
        dataService.refreshToken().subscribe({
          next:(x:any)=>{
            authHelper.storeJwtToken(x.token);
            if(authHelper.isTokenExpired()){
              inject(ToastrService).error("Nemate potrebne permisije!", "Greška");
              inject(Router).navigateByUrl("/login");
              return false;
            }
            return true;
          },
          error:(err)=>{
            authHelper.signOut();
            dataService.revokeToken().subscribe();
            inject(ToastrService).error("Nemate potrebne permisije!", "Greška");
            inject(Router).navigateByUrl("/login");
          }
        })

      }
      else{
        return true;
      }
      return true; //, inject(route), inject(state)
    }
    inject(ToastrService).error("Nemate potrebne permisije!", "Greška");
    inject(Router).navigateByUrl("/login");
    dataService.revokeToken().subscribe();
    authHelper.signOut();
    return false;
};

export const AuthJwtGuardAdmin: CanActivateFn = (route, state) => {
  var authHelper=inject(AuthJwtHelper);
  var dataService=inject(AuthJwtDataService);
  if(authHelper.getRole() === "Admin") //!authHelper.isTokenExpired() &&
  {
    if(authHelper.isTokenExpired()){
      dataService.refreshToken().subscribe({
        next:(x:any)=>{
          authHelper.storeJwtToken(x.token);
          if(authHelper.isTokenExpired()){
            // console.log("token istekao stvarno");
            inject(ToastrService).error("Nemate potrebne permisije!", "Greška");
            inject(Router).navigateByUrl("/login");
            return false;
          }
          return true;
        },
        error:(err)=>{
          authHelper.signOut();
          dataService.revokeToken().subscribe();
          inject(ToastrService).error("Nemate potrebne permisije!", "Greška");
          inject(Router).navigateByUrl("/login");
        }
      })

    }
    else{
      return true;
    }
    return true;
  }
  // console.log("Role nije admin, ELSEEEEE");
  inject(ToastrService).error("Nemate potrebne permisije!", "Greška");
  inject(Router).navigateByUrl("/login");
  dataService.revokeToken().subscribe();
  authHelper.signOut();
  return false;
};
