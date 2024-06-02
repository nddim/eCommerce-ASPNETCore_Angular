import {CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA} from '@angular/core';
import {Component, ElementRef, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import {ActivatedRoute, RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";
import {DataServiceProizvod} from "../admin-site/proizvodi/data-service-proizvod";
import {ProizvodGetAllResponse} from "../admin-site/proizvodi/proizvod-get-all-response";
import {Korpa, KorpaDataservice} from "../Kupac/korpa/korpa-dataservice";
import {NavbarComponent} from "../navbar/navbar.component";
import {RatingModule} from "primeng/rating";
import {defineComponents, IgcRatingComponent} from 'igniteui-webcomponents';
import {FormsModule} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../assets/moj-config";
import {FooterComponent} from "../footer/footer.component";
import {CarouselModule} from "primeng/carousel";
import {TagModule} from "primeng/tag";
import {ButtonModule} from "primeng/button";
import {ToastrService} from "ngx-toastr";

defineComponents(IgcRatingComponent);
@Component({
  selector: 'app-proizvod-page',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive, NavbarComponent, RatingModule, FormsModule, NgOptimizedImage, FooterComponent, CarouselModule, TagModule, ButtonModule],
  providers: [DataServiceProizvod],
  templateUrl: './proizvod-page.component.html',
  styleUrl: './proizvod-page.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})

export class ProizvodPageComponent implements OnInit{
  page: number = 1;
  count: number = 6;
  tableSize: number = 6;
  @Output() startThumbChange = new EventEmitter<number>();
  @Output() endThumbChange = new EventEmitter<number>();

  @ViewChild("startThumb", { static: true }) startThumbInput!: ElementRef;
  @ViewChild("endThumb", { static: true }) endThumbInput!: ElementRef;
  responsiveOptions: any[] | undefined;
  responsiveOptionsSlike: any[] | undefined;

  proizvodi: ProizvodGetAllResponse[] = [];
  proizvodiPrikaz: any[] = [];
  proizvodId:string="";
  ratingValue:any;
  ratingValueAvg:any;

  constructor(private route: ActivatedRoute,private toastr:ToastrService, private readonly dataServiceProizvod: DataServiceProizvod, private korpaDataService:KorpaDataservice, private http:HttpClient ) {
  }
  isSlikaPromijenjena: boolean = false;

  dodatneSlike:string[]=[];
  ngOnInit(){
     window.scrollTo({ top: 0, behavior: 'smooth' });

    this.route.params.subscribe((params)=> {
      window.scrollTo({ top: 0, behavior: 'smooth' });

      this.proizvodId = params['id'];
      this.getAllProizvod();
      this.getSimilarProducts();
      this.getRatingAvg();

    })
    this.responsiveOptions = [
      {
        breakpoint: '1199px',
        numVisible: 2,
        numScroll: 1
      },
      {
        breakpoint: '991px',
        numVisible: 2,
        numScroll: 1
      },
      {
        breakpoint: '767px',
        numVisible: 1,
        numScroll: 1
      }
    ];
    this.responsiveOptionsSlike = [
      {
        breakpoint: '1199px',
        numVisible: 3,
        numScroll: 3
      },
      {
        breakpoint: '991px',
        numVisible: 3,
        numScroll: 3
      },
      {
        breakpoint: '767px',
        numVisible: 3,
        numScroll: 3
      }
    ];
  }
  proizvod:ProizvodGetAllResponse={
    id:0,
    slikaUrl:"",
    brendID:0,
    brendNaziv:"",
    naziv:"",
    opis:"",
    brojKlikova:0,
    pocetnaCijena:0,
    popust:0,
    potkategorijaNaziv:"",
    pocetnaKolicina:0,
    potkategorijaID:0,
    slikeGalerija:[]
  }

  odabranaSlika:string=""
  getAllProizvod(){
    this.dataServiceProizvod.getById('proizvod-pretragaById?Id='+this.proizvodId+'&View=true').subscribe(
      data => {
        this.proizvod = data;
        this.odabranaSlika=this.proizvod.slikaUrl;
        this.dodatneSlike=this.proizvod.slikeGalerija;
        //this.proizvodiPrikaz = this.proizvodi;
      });
  }
  dodajUKorpu(prId:any, kol:number){
    this.korpaDataService.dodajUKorpu(prId, kol).subscribe(x=>{
      this.toastr.success("Uspjesno ste dodali proizvod u korpu");
    })
  }

  getRatingAvg(){
    var url = MojConfig.adresa_local+'ocjena/avg?ProizvodId='+this.proizvodId;
    return this.http.get<number>(url).subscribe(data=>{
      if(data!=0){
        this.ratingValue=data;
        this.ratingValueAvg=this.ratingValue;
      }
    }, error => {
    });

  }
  ratingInputVrijednost:number=0;

  protected readonly console = console;
slicniProizvodi:ProizvodGetAllResponse[]=[];
  private getSimilarProducts() {
    this.dataServiceProizvod.getSimilarProducts(this.proizvodId).subscribe(x=>{
      this.slicniProizvodi=x;
    })
  }

  odaberiSliku(s: string) {
    this.odabranaSlika=s;
    this.isSlikaPromijenjena = true;

  }
}
export interface Ocjena {
  id: number
  vrijednost: number
  proizvodId: number
  kupacId: number
}
