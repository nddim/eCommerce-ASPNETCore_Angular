import {ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot} from '@angular/router';
import {inject, Injectable} from "@angular/core";
import {GetAuth} from "../helper/auth/get-auth";
import {SnackBar} from "../helper/mat-snackbar/snack-bar";

@Injectable({providedIn:'root'})
export class PermissionsService {
  constructor(private router:Router,
              private getAuth:GetAuth,
              private snackBar:SnackBar) {
  }
  async canActivateAdmin(): Promise<boolean> {
    const isAdmin=await this.getAuth.isAdmin();
    if(isAdmin){
      return true;
    }
    else{
        this.router.navigate(['/login']);
        return false;
    }

    return false;

  }
  async canActivateKupac(): Promise<boolean> {
    const isKupac=await this.getAuth.isKupac();
    if(isKupac){
      return true;
    }
    else{
      this.router.navigate(['/login']);
      return false;
    }
    return false;

  }
  async canActivateKorpa(): Promise<boolean> {
    const isKorpa=await this.getAuth.provjeriKorpu();
    if(isKorpa){
      return true;
    }
    else if (!isKorpa){
      this.snackBar.openSnackBarCrveniKorpa("Korpa je prazna!", );
      this.router.navigate(['/']);
      return false;
    }
    return false;

  }

}

export const authGuardAdmin: CanActivateFn = (route, state) => {
  return inject(PermissionsService).canActivateAdmin(); //, inject(route), inject(state)
};
export const authGuardKupac: CanActivateFn = (route, state) => {
  return inject(PermissionsService).canActivateKupac(); //, inject(route), inject(state)
};
export const authKorpa: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  return inject(PermissionsService).canActivateKorpa();
};
