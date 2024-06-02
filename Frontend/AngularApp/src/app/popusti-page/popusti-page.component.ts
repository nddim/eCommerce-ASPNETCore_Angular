import { Component } from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import {ActivatedRoute, Router, RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";
import {ProizvodGetAllPretragaObject, ProizvodGetAllResponse} from "../admin-site/proizvodi/proizvod-get-all-response";
import {DataServiceProizvod} from "../admin-site/proizvodi/data-service-proizvod";
import {FormsModule} from "@angular/forms";
import {NgxPaginationModule} from "ngx-pagination";
import {HttpClientModule} from "@angular/common/http";
import {PopustiDataservice} from "./popusti-dataservice";
import {NavbarComponent} from "../navbar/navbar.component";
import {FooterComponent} from "../footer/footer.component";
import {SortiranjaResponseList} from "../pretraga-proizvoda/pretraga-proizvoda-klase";
import {KorpaDataservice} from "../Kupac/korpa/korpa-dataservice";
import {SnackBar} from "../helper/mat-snackbar/snack-bar";
import {DataServicePretragaProizvoda} from "../proizvod-pretraga-naziv/data-service-pretraga";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatOptionModule} from "@angular/material/core";
import {MatSelectModule} from "@angular/material/select";
import {LazyLoadImageModule} from "ng-lazyload-image";

@Component({
  selector: 'app-popusti-page',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive, FormsModule, NgxPaginationModule, HttpClientModule, NavbarComponent, FooterComponent, NgOptimizedImage, MatFormFieldModule, MatOptionModule, MatSelectModule, LazyLoadImageModule],
  providers:[DataServiceProizvod],
  templateUrl: './popusti-page.component.html',
  styleUrl: './popusti-page.component.css'
})
export class PopustiPageComponent {
  page: number = 1;
  count: number = 6;
  tableSize: number = 6;

  selectedValue: string ="";
  constructor(private readonly dataServiceProizvod: PopustiDataservice,
              private router:Router,
              private activatedRoute:ActivatedRoute,
              private korpaDataService:KorpaDataservice,
              private snackbar:SnackBar,
              private dataService:DataServicePretragaProizvoda) {
  }

  public trenutnoSortiranje=1;
  public sortiranja:SortiranjaResponseList[]=[];
  public grid:boolean=true;
  public proizvodiPrikaz:ProizvodGetAllPretragaObject[]=[];
  public naziv="";


  ngOnInit() {

    this.activatedRoute.params.subscribe(params=>{
      window.scrollTo({top:0, behavior:'smooth' })
      this.trenutnoSortiranje=parseInt(params["sort"]);
      this.page=parseInt(params["page"]);
      this.tableSize=parseInt(params["tableSize"]);
      this.ucitajSortiranja();
      this.ucitajProizvode();

    })

  }

  proizvodi:ProizvodGetAllPretragaObject[]=[];
  ucitajProizvode(){
    this.dataServiceProizvod.getPaged('proizvodPopusti', this.page, this.tableSize, this.trenutnoSortiranje).subscribe(x=>{
      this.proizvodi=x.proizvodi;
      this.proizvodiPrikaz=this.proizvodi;
      this.page=x.currentPage;
      this.tableSize=x.pageSize;
      this.count=x.totalCount;
    })
  }

  ucitajSortiranja(){
    this.dataService.getSortiranja("sortiranja-getall").subscribe(x=>{
      this.sortiranja=x.sortiranja;
      let sort=this.trenutnoSortiranje;
      this.trenutnoSortiranje=sort;
    })
  }

  onTableDataChange(event: any){
    this.page=event;
    //console.log(this.page);
    this.ucitajStranicu();
    // this.ucitajProizvode();
  }
  ucitajStranicu(){
    this.router.navigate(['/popusti', this.trenutnoSortiranje, this.page, this.tableSize]);
  }
   dodajUKorpu(prId:any, kol:number){
    this.korpaDataService.dodajUKorpu(prId, kol).subscribe(x=>{
      this.snackbar.openSnackBarPlaviKorpa("Uspješno ste dodali proizvod u korpu", "Pogledajte korpu");
    })
  }

  promijeniSortiranje() {
    this.page=1;
    this.ucitajStranicu();
  }
}
