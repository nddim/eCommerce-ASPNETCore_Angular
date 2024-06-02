import {Component, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA, OnInit} from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';

import {RouterLink, RouterOutlet} from "@angular/router";
import {NavbarComponent} from "../../navbar/navbar.component";
import {FooterComponent} from "../../footer/footer.component";
import {NarudzbaDataService} from "./narudzbe-dataservice";
import {readSpanComment} from "@angular/compiler-cli/src/ngtsc/typecheck/src/comments";
import {NgxPaginationModule} from "ngx-pagination";
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {ToastrService} from "ngx-toastr";
@Component({
  selector: 'app-kupac-narudzbe',
  standalone: true,
  imports: [CommonModule, RouterOutlet, NavbarComponent, FooterComponent, NgOptimizedImage, RouterLink, NgxPaginationModule, MatProgressSpinnerModule],
  templateUrl: './kupac-narudzbe.component.html',
  styleUrl: './kupac-narudzbe.component.css'
})
export class KupacNarudzbeComponent  {
  page: number = 1;
  count: number = 100;
  tableSize: number = 10;

  constructor(private dataService: NarudzbaDataService,
              private toastr:ToastrService) {
  }
  ngOnInit(){
    this.ucitajNarudzbePaged();
  }
  // ucitajKorpaArtikle() {
  //   this.dataService.getAll().subscribe(x => {
  //     this.korpa = x;
  //     this.artikli = this.korpa.artikli;
  //     this.ukupno = this.korpa.ukupno;
  //   })
  // }
    narudzbe:Narudzbe=[];
    ucitajNarudzbe(){
      this.dataService.getNaruzbe().subscribe(x=>{
        this.narudzbe=x;
      });
    }
    ucitajNarudzbePaged(){
      this.dataService.getPaged(this.page, this.tableSize).subscribe(x=>{
        this.narudzbe=x.narudzbe;
        this.page=x.currentPage;
        this.count=x.totalCount;
        this.tableSize=x.pageSize;
      })
    }
    otkaziNarudzbu(id:number){
      this.dataService.otkNarudzbu(id).subscribe({
        next: () => {
          this.toastr.info("Otkazana narudzba");
          this.page=1;
          this.ucitajNarudzbePaged();
        },
        error: (error) => {
          console.error("Greška prilikom otkazivanja narudžbe:", error);
          this.toastr.error("Došlo je do greške prilikom otkazivanja narudžbe.");
        }
      });
      this.page=1;
      this.ucitajNarudzbePaged();
    }

  preuzmiRacun(id: number) {
      this.overlayPrikazi=true;
    this.dataService.getRacun(id).subscribe(x=>{
      const base64data=x.file;

      const link=document.createElement('a');
      link.href='data:application/pdf;base64,'+base64data;
      link.download=x.filename;
      this.overlayPrikazi=false;
      link.click();
    }, error => {
      this.overlayPrikazi=false;
    })
  }
  overlayPrikazi=false;
  onTableDataChange(event: number) {
    this.page=event;
    // console.log(this.page);
    this.ucitajNarudzbePaged();
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }


}
export type Narudzbe = Narudzba[]

export interface Narudzba {
  id: number
  datumKreiranja: string
  dostava: string
  statusNarudzbe: string
  ukupnaCijena: number
}
