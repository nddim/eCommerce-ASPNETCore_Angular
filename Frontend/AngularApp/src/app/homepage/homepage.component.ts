import {Component, ElementRef} from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import {RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";
import {DataServiceProizvod} from "../admin-site/proizvodi/data-service-proizvod";
import {ProizvodGetAllResponse} from "../admin-site/proizvodi/proizvod-get-all-response";

import {DataServiceGlavnaKategorija} from "../admin-site/glavne-kategorije/data-service-glavna-kategorija";
import {GlavnaKategorijaGetAllResponse} from "../admin-site/glavne-kategorije/glavna-kategorija-get-all-response";
import {KategorijaGetAllResponse} from "../admin-site/kategorije/Kategorija-get-all-response";
import {DataServiceKategorija, KategorijaResponse} from "../admin-site/kategorije/data-service-kategorija";
import {HttpClientModule} from "@angular/common/http";
import {DataServicePotkategorija} from "../admin-site/potkategorije/data-service-potkategorija";
import {PotkategorijaGetAllResponse} from "../admin-site/potkategorije/Potkategorija-get-all-response";
import {NavbarComponent} from "../navbar/navbar.component";
import {MyAuthService} from "../helper/auth/MyAuthService";
import {PopustiDataservice} from "../popusti-page/popusti-dataservice";
import {KorpaDataservice} from "../Kupac/korpa/korpa-dataservice";
import {FooterComponent} from "../footer/footer.component";
import {ToastrService} from "ngx-toastr";
import {SnackBar} from "../helper/mat-snackbar/snack-bar";
import {LazyLoadImageModule} from "ng-lazyload-image";

@Component({
  selector: 'app-homepage',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterOutlet, RouterLinkActive, NgOptimizedImage, HttpClientModule, NavbarComponent, FooterComponent, LazyLoadImageModule],
  providers:[DataServiceProizvod, DataServiceGlavnaKategorija, DataServiceKategorija, DataServicePotkategorija, MyAuthService],
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.css'
})
export class HomepageComponent {
  proizvodi: ProizvodGetAllResponse[] = [];
  proizvodiPrikaz: any[] = [];
  glavneKategorije: GlavnaKategorijaGetAllResponse[] = [];
  glavneKategorijePrikaz: any[] = [];
  kkategorije: KategorijaGetAllResponse[] = [];
  kategorijePrikaz: any[] = [];
  ppotkategorije: PotkategorijaGetAllResponse[] = [];
  potkategorijePrikaz: any[] = [];
  proizvodiPopularni: ProizvodGetAllResponse[] = [];
  proizvodiPopularniPrikaz: any[] = [];
  constructor(private readonly dataServiceProizvod: PopustiDataservice,
              private readonly dataServiceGlavnaKategorija: DataServiceGlavnaKategorija,
              private readonly dataServiceKategorija: DataServiceKategorija,
              private readonly dataServicePotkategorija: DataServicePotkategorija,
              private readonly korpaDataService:KorpaDataservice,
              private readonly toastr:ToastrService,
              private readonly snackbar:SnackBar,
              {nativeElement}:ElementRef<HTMLImageElement>) {
    const supports='loading' in HTMLImageElement.prototype;
    if(supports){
      nativeElement.setAttribute('loading', 'lazy');
    }
  }
  ngOnInit() {
    this.getAllNoviProizvod();
    this.getAllPopularniProizvod();
    this.getAllKategorije();
  }
hovered="";
  kategorije:KategorijaResponse[]=[];
  getAllKategorije(){
    this.dataServiceKategorija.getKategorije().subscribe(x=>{
      this.kategorije=x;
    })
  }
  getAllNoviProizvod() {
    this.dataServiceProizvod.getData('proizvod-novi-pretraga').subscribe(
      data => {
        this.proizvodi = data;
        this.proizvodiPrikaz = this.proizvodi.slice(0,8);
      });
  }




  getAllPopularniProizvod() {
    this.dataServiceProizvod.getData('proizvod-popularni-pretraga').subscribe(
      data => {
        this.proizvodiPopularni = data;
        this.proizvodiPopularniPrikaz = this.proizvodiPopularni.slice(0,8);
      });
  }
  naziv="";
  dodajUKorpu(prId:any, kol:number){
    this.korpaDataService.dodajUKorpu(prId, kol).subscribe(x=>{
      this.naziv=x.naziv;
      this.snackbar.openSnackBarPlaviKorpa("Proizvod: "+x.naziv+" je uspješno dodat u korpu.", "Uđi u korpu");
    }, error=>{
      this.snackbar.openSnackBarCrveni(JSON.parse(JSON.stringify(error.error)));
    })
  }
}
