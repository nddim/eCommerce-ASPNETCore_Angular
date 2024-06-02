import {Component, CUSTOM_ELEMENTS_SCHEMA} from '@angular/core';
import { CommonModule } from '@angular/common';
import {NavbarComponent} from "../../navbar/navbar.component";
import {FormsModule} from "@angular/forms";
import {KorpaDataservice} from "../korpa/korpa-dataservice";
import {KorisnikInfo, KupacDataservice} from "./kupac-dataservice";
import {Router, RouterLink} from "@angular/router";
import {FooterComponent} from "../../footer/footer.component";
import {ToastrService} from "ngx-toastr";
import {SignalRService} from "../../helper/signal-r/signal-r.service";
import {GoogleMap, GoogleMapsModule, MapGeocoder} from "@angular/google-maps";
import { BrowserModule } from '@angular/platform-browser';
import { Loader } from "@googlemaps/js-api-loader"
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-kupac-checkout',
  standalone: true,
  imports: [CommonModule, NavbarComponent, FormsModule, RouterLink, FooterComponent, GoogleMap, GoogleMapsModule],
  templateUrl: './kupac-checkout.component.html',
  styleUrl: './kupac-checkout.component.css',
})
export class KupacCheckoutComponent {
  constructor(private korpaDataService: KorpaDataservice,
              private dataService: KupacDataservice,
              private toastr:ToastrService,
              private SignalRService:SignalRService,
              private geoCooder: MapGeocoder,
              private http: HttpClient,
              private router:Router)
  {
    /*geoCooder.geocode({
      address: '1600 Amphitheatre Parkway, Mountain View, CA'
    }).subscribe(({results}) => {
      console.log(results);
    });*/
  }
  latt:any;
  lngg:any;
  center: google.maps.LatLngLiteral = {lat: 44, lng: 17.75};zoom = 7.21;markerOptions: google.maps.MarkerOptions = {draggable: false};markerPositions: google.maps.LatLngLiteral[] = [];
  addMarker(event: google.maps.MapMouseEvent)
  { // @ts-ignore
    if(this.markerPositions.length!=0){
      this.markerPositions.length=0;
      // @ts-ignore
      this.markerPositions.push(event.latLng.toJSON());
    }
    // @ts-ignore
    this.markerPositions.push(event.latLng.toJSON());
    // console.log(event.latLng?.lat(), event.latLng?.lng());
    this.latt=event.latLng?.lat().toString();
    this.lngg=event.latLng?.lng().toString();
    this.geocodeCoordinates(this.latt,this.lngg);
    //console.log(this.latt, this.lngg)

  }
  adresa:any;
  geocodeCoordinates(latitude: string, longitude: string): Promise<string> {
    const apiKey = 'key'; // Replace with your Google Maps API key
    const url = `https://maps.googleapis.com/maps/api/geocode/json?latlng=${latitude},${longitude}&key=${apiKey}`;

    return this.http.get(url)
      .toPromise()
      .then((response: any) => {
        if (response.status === 'OK' && response.results[0]) {
          // return response.results[0].formatted_address;
          const formattedAddress = response.results[0].formatted_address;
          //console.log('Formatted Address:', formattedAddress); // Log the formatted address to the console
          this.adresaInput=formattedAddress.toString();
          this.mapaInput=formattedAddress.toString();
          return formattedAddress;

        } else {
          return 'Address not found';
        }
      })
      .catch(error => {
        console.error('Error geocoding coordinates:', error);
        return 'Error geocoding coordinates';
      });
  }

  ngOnInit(): void {
    this.ucitajKorpaArtikle();
    this.ucitajKorisnikId();
    this.SignalRService.otvori_ws_konekciju();
  }

  prikaziRacun: boolean = false;
  prikaziKarticnoPlacanje: boolean = false;
  artikli: Artikal[] = [];
  ukupno: number = 0;
  korpa: Korpa = {
    artikli: [],
    ukupno: 0
  }
  kupac: KorisnikInfo = {
    id:"",
    ime: "",
    prezime: "",
    email: "",
    adresa: "",
    kontaktBroj: ""
  }
  otvoriMape:boolean=false;
  imeInput:string="";
  prezimeInput:string="";
  emailInput:string="";
  adresaInput:string="";
  mapaInput:string="";
  drzavaInput:string="";
  gradInput:string="";
  postanskiBrojInput:string="";
  kontaktBrojInput:string="";
  dostavaInput:string="Plaćanje po dostavi";
  komentarInput:string="";
  ukupnaCijenaInput:number=this.ukupno;
  kupacIdInput:string='';
  stavkeNarudzbeInput: StavkeNarudzbe[]=[];
  ucitajKorpaArtikle() {
    this.korpaDataService.getAll().subscribe(x => {
      this.korpa = x;
      this.artikli = this.korpa.artikli;
      this.ukupno = this.korpa.ukupno;
      this.ukupnaCijenaInput=this.ukupno;
    })
  }

  staraAdresaBool: boolean = false;
  novaAdresaBool: boolean = false;
  ucitaniKupac:KorisnikInfo={
    id:"",
    ime:"",
    adresa:"",
    kontaktBroj:"",
    email:"",
    prezime:""
  }
  ucitajKorisnikId(){
    this.dataService.getAll().subscribe(x=>{
      this.kupacIdInput=x.id;
      this.ucitaniKupac=x;
     // console.log(x.id);
    });
  }
  /*ucitajKorisnikInfo() {
    if (this.staraAdresaBool) {
      this.dataService.getAll().subscribe(x => {
        this.kupac = x;
      })
      this.imeInput=this.kupac.ime;
      this.prezimeInput=this.kupac.prezime;
      this.emailInput=this.kupac.email;
      this.adresaInput=this.kupac.adresa;
      this.kontaktBrojInput=this.kupac.kontaktBroj;
    }
    else if (this.novaAdresaBool) {
      this.kupac.ime = "";
      this.kupac.prezime = "";
      this.kupac.email = "";
      this.kupac.adresa = "";
      this.kupac.kontaktBroj = "";
    }

  }


  selectedAddressOption: string | null = null;
  onAddressOptionChange() {
    if (this.selectedAddressOption === 'stara') {
      this.staraAdresaBool = true;
      this.novaAdresaBool = false;
      this.ucitajKorisnikInfo(); // Call your method
    } else if (this.selectedAddressOption === 'nova') {
      this.staraAdresaBool = false;
      this.novaAdresaBool = true;
      this.ucitajKorisnikInfo();
    }
  }*/

  dodajNarudzbu(){
    // if(!this.user.ime || !this.user.prezime
    // )
    // {
    //   this.toastr.error("Nedozvoljene vrijednosti!");
    //   return;
    // }
  if(
    !this.imeInput ||
    !this.emailInput ||
    !this.prezimeInput ||
    !this.gradInput ||
    !this.kontaktBrojInput
  )
  {
    this.toastr.error("Prazne vrijednosti!", "Greska");
    return;
  }
    this.stavkeNarudzbeInput = [];

    this.artikli.forEach(stavka=>{
      var stavkaNarudzbe:StavkeNarudzbe = {
        proizvodId:stavka.proizvod.id,
        kolicina:stavka.kolicina,
        ukupnaCijena:stavka.cijenaKolicina,
        unitCijena:stavka.proizvod.pocetnaCijena,
        popust:stavka.proizvod.popust
      };
      this.stavkeNarudzbeInput.push(stavkaNarudzbe);
    });
    var adresaValue = this.otvoriMape ? this.mapaInput : this.adresaInput;
    var noviObj:Narudzba={
      ime:this.imeInput,
      prezime:this.prezimeInput,
      email:this.emailInput,
      adresa:adresaValue,
      grad:this.gradInput,
      drzava:this.drzavaInput,
      postanskiBroj:this.postanskiBrojInput,
      kontaktBroj:this.kontaktBrojInput,
      dostava:this.dostavaInput,
      komentar:this.komentarInput,
      ukupnaCijena:this.ukupnaCijenaInput,
      kupacId:this.kupacIdInput,
      stavkeNarudzbe:this.stavkeNarudzbeInput,
    };
    //console.log(noviObj);

    this.dataService.napraviNarudzbu(noviObj).subscribe(x=>{
      // this.to alert();
      this.toastr.success("Uspješno kreirana narudžba", "Informacija");
      this.router.navigateByUrl("/kupac-panel/pregled-narudzbi");

    }, error=>{
      this.toastr.error("Greška sa dodavanjem narudzbe");
    })
  }

  popuniVrijednosti() {
    if(this.ucitaniKupac.id!=""){
     this.kupac=this.ucitaniKupac;
      this.imeInput=this.kupac.ime;
      this.prezimeInput=this.kupac.prezime;
      this.emailInput=this.kupac.email;
      this.adresaInput=this.kupac.adresa;
      this.kontaktBrojInput=this.kupac.kontaktBroj;
      this.drzavaInput="Bosna i Hercegovina";
    }

  }
  // google maps api



  // google maps kraj








}
export interface Artikal{
  id:number,
  proizvod:any,
  kolicina:number,
  cijenaKolicina:number
}
export interface Korpa{
  artikli:Artikal[],
  ukupno:number
}
// export interface KorisnikInfo {
//   id:number
//   ime: string
//   prezime: string
//   email: string
//   adresa: string
//   kontaktBroj: string
// }

export interface Narudzba {
  ime: string
  prezime: string
  email: string
  adresa: string
  grad: string
  postanskiBroj: string
  kontaktBroj: string
  drzava: string
  ukupnaCijena: number
  dostava: string
  kupacId: string
  komentar: string
  stavkeNarudzbe: StavkeNarudzbe[]
}

export interface StavkeNarudzbe {
  proizvodId: number
  kolicina: number
  unitCijena: number
  ukupnaCijena: number
  popust: number
}
export interface MapGeocoderResponse {
  status: google.maps.GeocoderStatus;
  results: google.maps.GeocoderResult[];
}
