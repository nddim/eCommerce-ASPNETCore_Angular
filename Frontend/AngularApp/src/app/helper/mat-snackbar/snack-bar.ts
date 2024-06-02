import {Injectable} from "@angular/core";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Router} from "@angular/router";
import {appConfig} from "../../app.config";

@Injectable({providedIn:'root'})
export class SnackBar{
  constructor(private _snackBar: MatSnackBar, private router:Router) {
  }

  openSnackBarPlavi(poruka:string, action="") {
    this._snackBar.open(poruka,action, {
      duration:5000,
      verticalPosition:'top',
      horizontalPosition:"center",
      panelClass:['plavi-snackbar'],
    })
  }
  openSnackBarPlaviKorpa(poruka:string, action="korpa") {
    this._snackBar.open(poruka,action, {
      duration:5000,
      verticalPosition:'top',
      horizontalPosition:"center",
      panelClass:['plavi-snackbar'],
    }).onAction().subscribe(()=>this.router.navigateByUrl("korpa"));
  }

  openSnackBarPlaviSignal(poruka:string, action="") {
    this._snackBar.open(poruka,action, {
      duration:7500,
      verticalPosition:'top',
      horizontalPosition:"center",
      panelClass:['plavi-snackbar'],
    }).onAction().subscribe(()=>this.router.navigateByUrl('kupac-panel/pregled-narudzbi'));
  }
  openSnackBarZeleni(poruka:string, action="") {
    this._snackBar.open(poruka,action, {
      duration:5000,
      verticalPosition:'top',
      horizontalPosition:"center",
      panelClass:['zeleni-snackbar'],
    })
  }
  openSnackBarCrveni(poruka:string, action="") {
    this._snackBar.open(poruka,action, {
      duration:5000,
      verticalPosition:'top',
      horizontalPosition:"center",
      panelClass:['crveni-snackbar'],
    })
  }
  openSnackBarCrveniKorpa(poruka:string, action="") {
    this._snackBar.open(poruka,action, {
      duration:3500,
      verticalPosition:'top',
      horizontalPosition:"center",
      panelClass:['crveni-snackbar'],
    }).onAction().subscribe(()=>this.router.navigateByUrl(''));
  }
  openSnackBarZuti(poruka:string, action="") {
    this._snackBar.open(poruka, action, {
      duration:5000,
      verticalPosition:'top',
      horizontalPosition:"center",
      panelClass:['zuti-snackbar'],
    })
  }
}
