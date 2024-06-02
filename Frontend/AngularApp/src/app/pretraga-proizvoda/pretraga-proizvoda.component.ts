import {Component, OnInit, Output, EventEmitter, ViewChild, ElementRef, ChangeDetectorRef} from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import {ActivatedRoute, Router, RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";
import {DataServicePretragaProizvoda} from "./data-service-pretraga";

import {
  BrendChecks,
  BrendGetAll, BrojProizvodaStranica,
  PotkategorijeSidebar, ProizvodiSearchResults,
  SortiranjaResponseList
} from "./pretraga-proizvoda-klase";
import {MatSliderModule} from '@angular/material/slider';
import {MatExpansionModule} from '@angular/material/expansion';
import {FormsModule} from "@angular/forms";
import {ProizvodGetAllPretragaObject} from "../admin-site/proizvodi/proizvod-get-all-response";
import './myscript.js'
import {NgxPaginationModule} from "ngx-pagination";
import {HttpClientModule} from "@angular/common/http";
import {NavbarComponent} from "../navbar/navbar.component";
import {MyAuthService} from "../helper/auth/MyAuthService";
import {KorpaDataservice} from "../Kupac/korpa/korpa-dataservice";
import {MatInputModule} from "@angular/material/input";
import {MatSelectModule} from "@angular/material/select";
import {SnackBar} from "../helper/mat-snackbar/snack-bar";
import {FooterComponent} from "../footer/footer.component";
import {LazyLoadImageModule} from "ng-lazyload-image";

@Component({
  selector: 'app-pretraga-proizvoda',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, RouterOutlet, NgOptimizedImage, MatSliderModule, MatExpansionModule, FormsModule, NgxPaginationModule, HttpClientModule, NavbarComponent, MatInputModule, MatSelectModule, FooterComponent, LazyLoadImageModule],
  providers:[DataServicePretragaProizvoda, MyAuthService],
  templateUrl: './pretraga-proizvoda.component.html',
  styleUrl: './pretraga-proizvoda.component.css'
})
export class PretragaProizvodaComponent implements OnInit{
  POSTS: any;
  page: number = 1;
  count: number = 6;
  tableSize: number = 6;
  tableSizeObj:BrojProizvodaStranica |undefined={
    id:1,
    tableSize:6
  }
  brojProizvodaList:BrojProizvodaStranica[]=[];
  tableSizeId=1;
  @Output() startThumbChange = new EventEmitter<number>();
  @Output() endThumbChange = new EventEmitter<number>();

  @ViewChild("startThumb", { static: true }) startThumbInput!: ElementRef;
  @ViewChild("endThumb", { static: true }) endThumbInput!: ElementRef;

  public searchInput:string="";
  public searchByNameResults:ProizvodiSearchResults[]=[];
  public trenutnoSortiranje:number=1;
  public potk:any;
  public proizvodiPrikaz:ProizvodGetAllPretragaObject[]=[];
  public brendoviSidebar:BrendGetAll[]=[];
  public brendoviKopija:BrendGetAll[]=[];
  public brendoviChecked:BrendChecks[]=[];
  public potkategorijePrikaz:PotkategorijeSidebar[]=[];
  public sortiranja:SortiranjaResponseList[]=[];
  public grid:boolean=true;

  constructor(public activatedRoute:ActivatedRoute, private dataServiceProizvod:DataServicePretragaProizvoda,
              private cdr: ChangeDetectorRef,
              private korpaDataService:KorpaDataservice,
              private router:Router,
              private snackbar:SnackBar) {
    this.minZadani=0;
    this.maxZadani=1000;
    this.brojProizvodaList=[];

    const obj1:BrojProizvodaStranica={id:1, tableSize:6};
    const obj2:BrojProizvodaStranica={id:2, tableSize:12};
    const obj3:BrojProizvodaStranica={id:3, tableSize:24};

    this.brojProizvodaList.push(obj1);
    this.brojProizvodaList.push(obj2);
    this.brojProizvodaList.push(obj3);
  }

  brendoviNaziv:string[]=[];

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params=>{
      window.scrollTo({ top: 0, behavior: 'smooth' });
      this.potk=params["potk"];
      this.trenutnoSortiranje=parseInt(params["sort"]);
      this.page=params["page"];
      this.tableSize=params["tableSize"];
      this.tableSizeObj=this.brojProizvodaList.find((x)=>x.tableSize==this.tableSize);
      this.tableSizeId=this.tableSizeObj!.id;
      this.minZadani=parseInt(params["min"]);
      this.maxZadani=parseInt(params["max"]);
      let gridTrue=parseInt(params["grid"]);
      if(gridTrue==1){
        this.grid=true;
      }
      else{
        this.grid=false;
      }
      const brendoviString=params["brendovi"];
      this.brendoviNaziv=brendoviString.split(',');

      this.pocetnoUcitavanje();
    })
  }
  onTableDataChange(event: any){
    this.page=event;
    window.scrollTo({ top: 0, behavior: 'smooth' });
    this.ucitajStranicu();

  }

  private async UcitajBrendove(oninit:boolean=false) {

    var nastavak = '';
    if (!oninit) {
      this.brendoviKopija = JSON.parse(JSON.stringify(this.brendoviSidebar));
      nastavak = '&min=' + this.minZadani.toString() + '&max=' + this.maxZadani.toString();
    }
  const data= await this.dataServiceProizvod.getBrends('brend-pretraga-bypotk?PotkategorijaID=' +
    this.potk.toString() + nastavak).toPromise();
    this.brendoviSidebar=[];
    if(data){
      this.brendoviSidebar=data;
    }
    if (oninit) {
      this.brendoviSidebar.forEach(brend => {
      if(this.brendoviNaziv.find((x)=>x===brend.brendNaziv)!=null){
          brend.isChecked=true;
        }
        else{
          brend.isChecked = false;
        }

      });
    } else {
      for (let index = 0; index < this.brendoviSidebar.length; index++) {
        var obj = this.brendoviKopija.find(x => x.brendId == this.brendoviSidebar[index].brendId);
        if (obj) {
          if (obj.isChecked) {
            this.brendoviSidebar[index].isChecked = true;
          } else {
            this.brendoviSidebar[index].isChecked = false;
          }
        } else {
          this.brendoviSidebar[index].isChecked = false;
        }
      }
    }
  }

  ucitajProizvode()
  {
    this.potk=this.activatedRoute.snapshot.params["potk"];
    var linkBrendovi='';
    for (let index=0;index<this.brendoviSidebar.length;index++){
      if(this.brendoviSidebar[index].isChecked){
        linkBrendovi+='Brendovi='+this.brendoviSidebar[index].brendId.toString()+'&';
      }
    }
    this.dataServiceProizvod.getData('proizvod-pretraga-all?'+linkBrendovi+'PotkategorijaID='+
      this.potk.toString()+'&Min='+this.minZadani.toString()
    +'&Max='+this.maxZadani.toString()+'&SortID='+this.trenutnoSortiranje.toString()
    +"&PageNumber="+this.page+"&PageSize="+this.tableSize).subscribe(
      data => {
        this.proizvodiPrikaz = data.proizvodi;
        this.count=data.totalCount;
        this.cdr.detectChanges();
        if((data.min!=data.max)&& data.min!=0){
          this.minimum=data.min;
          this.maximum=data.max;
        }
        else if((data.min==data.max)&&data.min!=0){
          this.minimum=data.min;
          this.maximum=data.max;
        }
        else if(data.min==data.max && data.min==0){
          this.minimum=this.minZadani;
          this.maximum=this.maxZadani;
        }

        if(this.minZadani==0 && this.maxZadani==0){
          this.minZadani=this.minimum;
          this.maxZadani=this.maximum;
        }
        if(this.minZadani<this.minimum)
        {
          this.minZadani=this.minimum;
        }
        if(this.maxZadani>this.maximum)
        {
          this.maxZadani=this.maximum;
        }
      });
  }
  updateBrend(brendId: number) {
    this.page =1;
    const brend = this.brendoviSidebar.find(x => x.brendId === brendId);

    if (brend) {
      brend.isChecked = !brend.isChecked;
    }

    var odabraniBrendovi=this.brendoviSidebar.filter(brend=>brend.isChecked);

    var naziviBrendova=odabraniBrendovi.map(brend=>brend.brendNaziv);
    this.brendoviNaziv=naziviBrendova;

    this.ucitajStranicu();
  }

  ucitajPotkategorije(){
    this.dataServiceProizvod.getPotkategorije('potkategorija-pretragabypotk?PotkategorijaID='+
      this.potk.toString()).subscribe(
      data => {
        this.potkategorijePrikaz=data;
      });
  }

  promijeniPotkategoriju(potkategorijaID: number) {
    this.potk=potkategorijaID;
   this.pocetnoUcitavanje();
  }
  async pocetnoUcitavanje(){
    await this.UcitajBrendove(true);
    this.ucitajPotkategorije();
    this.ucitajSortiranja();
    this.ucitajProizvode();
  }
  startThumbValue: number = 2;  // Initial value for display

  minimum: number=0;
  maximum: number=100000;
  minZadani: number=1;
  maxZadani: number=1000;

  promjenaCijeneMin(event:Event) {
    this.page=1;
  this.minZadani=Number((event.target as HTMLInputElement).value);
  this.ucitajStranicu();
  }
  promjenaCijeneMax(event:Event) {
    this.page=1;
    this.maxZadani=Number((event.target as HTMLInputElement).value);
    this.ucitajStranicu();
  }

  ucitajSortiranja(){
    this.dataServiceProizvod.getSortiranja("sortiranja-getall").subscribe(data=>{
      this.sortiranja=data.sortiranja;
    })
  }

  promijeniSortiranje() {
    this.ucitajStranicu();
  }
  ucitajStranicu(){
    var spojeniNazivi="s";
    if(this.brendoviNaziv.length>0){
      spojeniNazivi=this.brendoviNaziv.join(',');
    }
    var grid=1;
    if(!this.grid){
      grid=2;
    }
    this.router.navigate(['/pretraga-proizvoda', this.potk, this.trenutnoSortiranje,
      this.page, this.tableSize, this.minZadani, this.maxZadani, grid, spojeniNazivi]);

  }
  public prikaziSearch:boolean=false;

  dodajUKorpu(prId:any, kol:number){
    this.korpaDataService.dodajUKorpu(prId, kol).subscribe(x=>{
      this.snackbar.openSnackBarPlaviKorpa("Uspješno ste dodali proizvod u korpu", "Pogledajte korpu");
    }, error=>{
      this.snackbar.openSnackBarCrveni(JSON.parse(JSON.stringify(error.error)));
    })
  }
  promijeniBrojProizvoda(){
    this.tableSizeObj=this.brojProizvodaList.find((x)=>x.id==this.tableSizeId);
    this.tableSize=this.tableSizeObj!.tableSize;
    this.page=1;
    this.ucitajStranicu();
  }
}
