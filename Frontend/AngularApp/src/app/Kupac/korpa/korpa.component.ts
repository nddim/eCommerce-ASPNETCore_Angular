import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {NavbarComponent} from "../../navbar/navbar.component";
import {KorpaDataservice} from "./korpa-dataservice";
import {RouterLink} from "@angular/router";
import {FormsModule} from "@angular/forms";
import {FooterComponent} from "../../footer/footer.component";
import {ToastrService} from "ngx-toastr";
import {MatSnackBar} from "@angular/material/snack-bar";
import {SnackBar} from "../../helper/mat-snackbar/snack-bar";
//import {propertyIsEnumerable} from "jasmine";

@Component({
  selector: 'app-korpa',
  standalone: true,
  imports: [CommonModule, NavbarComponent, RouterLink, FormsModule, FooterComponent],
  templateUrl: './korpa.component.html',
  styleUrl: './korpa.component.css'
})
export class KorpaComponent implements OnInit {
    constructor(private dataService: KorpaDataservice,
                private toastr:ToastrService,
                private _snackBar: MatSnackBar,
                private snackbar:SnackBar) {
    }

    korpa: Korpa = {
        artikli: [],
        ukupno: 0
    }
    artikli: Artikal[] = [];
    ukupno: number = 0;
    ngOnInit(): void {
        this.ucitajKorpaArtikle();
    }

    ucitajKorpaArtikle() {
        this.dataService.getAll().subscribe(x => {
            this.korpa = x;
            this.artikli = this.korpa.artikli;
            this.ukupno = this.korpa.ukupno;
        })
    }

    ukloniIzKorpe(id: any) {
        this.dataService.removeFromKorpa(id).subscribe(x => {
            this.ucitajKorpaArtikle();
        })
    }
    updateKorpa(prId:any, kol:number, pockol:number){
      if(kol==pockol){
        // this.toastr.info("Trenutno možete naručiti maksimalno "+pockol.toString()+" komada ovog proizvoda", "Informacija");
        this.snackbar.openSnackBarCrveni("Trenutno možete naručiti maksimalno "+pockol.toString()+" komada ovog proizvoda");
      }

        this.dataService.updateKorpa(prId,kol).subscribe(x=>{
            //dodati obavjestenje
            this.ucitajKorpaArtikle();
        }, error=>{
          this.toastr.error(JSON.parse(JSON.stringify(error.error)), "Greška");
        });

    }
}
export interface Artikal{
id:number,
  proizvod:any,
  kolicina:number,
  cijenaKolicina:number,
  pocetnaKolicina:number
}

export interface Korpa{
  artikli:Artikal[],
  ukupno:number
}

