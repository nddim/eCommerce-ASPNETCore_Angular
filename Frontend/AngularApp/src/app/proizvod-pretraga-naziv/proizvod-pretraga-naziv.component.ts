import {Component, ElementRef, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import {NavbarComponent} from "../navbar/navbar.component";
import {NgxPaginationModule} from "ngx-pagination";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {SortiranjaResponseList} from "../pretraga-proizvoda/pretraga-proizvoda-klase";
import {ActivatedRoute, Router, RouterLink} from "@angular/router";
import {ProizvodGetAllPretragaObject} from "../admin-site/proizvodi/proizvod-get-all-response";
import {KorpaDataservice} from "../Kupac/korpa/korpa-dataservice";
import {DataServicePretragaProizvoda} from "./data-service-pretraga";
import {SnackBar} from "../helper/mat-snackbar/snack-bar";
import {FooterComponent} from "../footer/footer.component";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatOptionModule} from "@angular/material/core";
import {MatSelectModule} from "@angular/material/select";
@Component({
  selector: 'app-proizvod-pretraga-naziv',
  standalone: true,
  imports: [CommonModule, NavbarComponent, NgxPaginationModule, ReactiveFormsModule, RouterLink, FormsModule, NgOptimizedImage, FooterComponent, MatFormFieldModule, MatOptionModule, MatSelectModule],
  templateUrl: './proizvod-pretraga-naziv.component.html',
  styleUrl: './proizvod-pretraga-naziv.component.css'
})
export class ProizvodPretragaNazivComponent implements OnInit {
  page: number = 1;
  count: number = 6;
  tableSize: number = 6;
  @Output() startThumbChange = new EventEmitter<number>();
  @Output() endThumbChange = new EventEmitter<number>();

  @ViewChild("startThumb", { static: true }) startThumbInput!: ElementRef;
  @ViewChild("endThumb", { static: true }) endThumbInput!: ElementRef;


  constructor(private korpaDataService:KorpaDataservice, private activatedRoute:ActivatedRoute, private dataService:DataServicePretragaProizvoda,
              private snackbar:SnackBar,
              private router:Router) {
  }

  public trenutnoSortiranje:number=1;
  public sortiranja:SortiranjaResponseList[]=[];
  public grid:boolean=true;
  public proizvodiPrikaz:ProizvodGetAllPretragaObject[]=[];
  public naziv="";

  ngOnInit(): void {
    window.scrollTo({top:0, behavior:'smooth' })

    this.activatedRoute.params.subscribe(params=>{
      this.naziv=params['naziv'];
      this.trenutnoSortiranje=parseInt(params["sort"]);
      this.page=params["page"];
      this.tableSize=params["tableSize"];
      this.ucitajProizvode();
      this.ucitajSortiranja();
    })
  }

  ucitajSortiranja(){
    this.dataService.getSortiranja("sortiranja-getall").subscribe(x=>{
      this.sortiranja=x.sortiranja;
    })
  }
  promijeniSortiranje() {
    this.page=1;
    this.ucitajStranicu();
  }
  ucitajProizvode(){
    this.proizvodiPrikaz=[];
    this.dataService.getProizvodiNaziv(this.naziv, this.trenutnoSortiranje,this.page, this.tableSize).subscribe(x=>{
      this.proizvodiPrikaz=x.proizvodi;
      this.count=x.totalCount;
    })
  }

  dodajUKorpu(prId:any, kol:number){
    this.korpaDataService.dodajUKorpu(prId, kol).subscribe(x=>{
      this.snackbar.openSnackBarPlaviKorpa("Uspješno ste dodali proizvod u korpu", "Pogledajte korpu");
    })
  }

  onTableDataChange(event: any){
    this.page=event;
    this.ucitajStranicu();
  }

  ucitajStranicu(){
    this.router.navigate(['/pretraga-naziv', this.naziv, this.trenutnoSortiranje,
      this.page, this.tableSize]);
  }


}
