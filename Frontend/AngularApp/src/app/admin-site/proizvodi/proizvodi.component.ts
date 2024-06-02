import {Component, ElementRef, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import {FormsModule} from "@angular/forms";
import {ProizvodGetAllResponse, ProizvodPostRequest} from "./proizvod-get-all-response";
import {PotkategorijaGetAllResponse} from "../potkategorije/Potkategorija-get-all-response";
import {BrendoviGetAllResponse} from "../brendovi/brendovi-get-all-response";
import {
  Dani,
  DataServiceProizvod,
  ProizvodAnalitika,
  ProizvodEdit,
  ProizvodGetById,
  ProizvodSlika, SlikaDTO, SlikaGalleryPost
} from "./data-service-proizvod";
import {DataServicePotkategorija} from "../potkategorije/data-service-potkategorija";
import {DataServiceBrend} from "../brendovi/data-service";
import {GlavnaKategorijaGetAllResponse} from "../glavne-kategorije/glavna-kategorija-get-all-response";
import {DataServiceKategorija} from "../kategorije/data-service-kategorija";
import {DataServiceGlavnaKategorija} from "../glavne-kategorije/data-service-glavna-kategorija";
import {KategorijaGetAllResponse} from "../kategorije/Kategorija-get-all-response";
import {RouterLink, RouterOutlet} from "@angular/router";
import {EditorComponent} from "@tinymce/tinymce-angular";
import {NgxPaginationModule} from "ngx-pagination";
import {ToastrService} from "ngx-toastr";
import {Chart} from "chart.js/auto";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatOptionModule} from "@angular/material/core";
import {MatSelectModule} from "@angular/material/select";
import {SortiranjaResponseList} from "../../pretraga-proizvoda/pretraga-proizvoda-klase";
import {DataServicePretragaProizvoda} from "../../pretraga-proizvoda/data-service-pretraga";


@Component({
  selector: 'app-proizvodi',
  standalone: true,
  imports: [CommonModule, FormsModule, NgOptimizedImage, RouterLink, RouterOutlet, EditorComponent, NgxPaginationModule, MatFormFieldModule, MatOptionModule, MatSelectModule],
  providers:[DataServiceProizvod, DataServicePotkategorija, DataServiceBrend, DataServiceKategorija, DataServiceGlavnaKategorija,],
  templateUrl: './proizvodi.component.html',
  styleUrl: './proizvodi.component.css'
})
export class ProizvodiComponent implements OnInit {
  page: number = 1;
  count: number = 100;
  tableSize: number = 10;
  @Output() startThumbChange = new EventEmitter<number>();
  @Output() endThumbChange = new EventEmitter<number>();

  @ViewChild("startThumb", { static: true }) startThumbInput!: ElementRef;
  @ViewChild("endThumb", { static: true }) endThumbInput!: ElementRef;


  constructor(private readonly dataService: DataServiceProizvod,
              private readonly dataServicePotkategorija: DataServicePotkategorija,
              private readonly dateServiceKategorija: DataServiceKategorija,
              private readonly dataServiceGlavnaKategorija: DataServiceGlavnaKategorija,
              private readonly dataServiceBrend: DataServiceBrend,
              private readonly toastr:ToastrService,
              private dataServiceProizvod:DataServicePretragaProizvoda) {

  }
  ngOnInit() {
    this.getAllProizvodi();
    this.getAllPotkategorije();
    this.getAllBrendovi();
    this.getAllGlavneKategorije();
    this.getAllKategorije();
    this.getAllPotkategorijeFilter();
    this.ucitajSortiranja();
  }
  public trenutnoSortiranje:number=6;

  proizvodi: ProizvodGetAllResponse[] = [];
  proizvodiPrikaz: any[] = [];
  potkategorije: PotkategorijaGetAllResponse[] = [];
  brendovi: BrendoviGetAllResponse[] = [];
  proizvod: ProizvodPostRequest = {
    naziv: "",
    pocetnaKolicina: 0,
    pocetnaCijena: 0,
    opis: "",
    brojKlikova: 0,
    potkategorijaID: 0,
    brendID: 0,
    popust:0
  }
  oldProizvodObj: any;

  //glavne kategorije
  glavneKategorije: GlavnaKategorijaGetAllResponse[] = [];
  glavnaKategorijaFilter: GlavnaKategorijaGetAllResponse = {
    id: 0,
    naziv: ""
  }
  //kategorije
  kategorije: KategorijaGetAllResponse[] = [];
  kategorijaFilter: KategorijaGetAllResponse = {
    id: 0,
    naziv: "",
    glavnaKategorijaID: 0,
    nazivGlavneKategorije: ""
  }
  //potkategorije
  filterPotkategorija: PotkategorijaGetAllResponse = {
    id: 0,
    naziv: "",
    kategorijaID: 0,
    kategorijaNaziv: ""
  }
  potkategorijeFilter: PotkategorijaGetAllResponse[] = [];
  potkategorijaFilterPretraga: PotkategorijaGetAllResponse = {
    id: 0,
    naziv: "",
    kategorijaID: 0,
    kategorijaNaziv: ""
  }
  zadnjiProizvod: any;
  proizvodId: string = "0";





  getAllProizvodi() {

    this.dataService.getData(this.naziv, this.trenutnoSortiranje,this.page, this.tableSize).subscribe(
      data => {
        this.proizvodi = data.proizvodi;
        this.proizvodiPrikaz = this.proizvodi;
        this.count=data.totalCount;
      });
  }

  getAllProizvodiFilter() {
    this.dataService.getDataFilter('proizvod-pretraga?Naziv='+this.naziv+'&PotkategorijaID=' +
      this.potkategorijaFilterPretraga.id.toString() + '&BrendID=0', this.trenutnoSortiranje,this.page, this.tableSize).subscribe(
      data => {
        this.proizvodi = data.proizvodi;
        this.proizvodiPrikaz = this.proizvodi;
        this.count=data.totalCount;
      });
  }

  getAllPotkategorijeFilter() {
    this.dataServicePotkategorija.getData('potkategorija-pretraga?Naziv=' +
      '&KategorijaID=' + this.kategorijaFilter.id.toString()).subscribe(data => {
      this.potkategorijeFilter = data;
    });
  }

  getAllPotkategorije() {
    this.dataServicePotkategorija.getData('potkategorija-pretraga?Naziv=' +
      this.filterPotkategorija.naziv + '&KategorijaID=0').subscribe(data => {
      this.potkategorije = data;
    });
  }

  getAllKategorije() {
    this.dateServiceKategorija.getData('kategorija-pretraga?Naziv=' +
      '&GlavnaKategorijaID=' + this.glavnaKategorijaFilter.id.toString()).subscribe(data => {
      this.kategorije = data;
    });
  }

  private getAllGlavneKategorije() {
    this.dataServiceGlavnaKategorija.getData('glavna-kategorija-pretraga').subscribe(data => {
      this.glavneKategorije = data;
    });
  }

  getAllBrendovi() {
    this.dataServiceBrend.getData('brend-pretraga').subscribe(data => {
      this.brendovi = data;
    });
  }

  sendData() {
    const dataToSend: ProizvodPostRequest = {
      naziv: this.proizvod.naziv,
      pocetnaCijena: this.proizvod.pocetnaCijena,
      pocetnaKolicina: this.proizvod.pocetnaKolicina,
      opis: this.proizvod.opis,
      brojKlikova: this.proizvod.brojKlikova,
      potkategorijaID: this.proizvod.potkategorijaID,
      brendID: this.proizvod.brendID,
      popust:this.proizvod.popust,
    }
    this.dataService.postData(dataToSend, 'proizvod-dodaj').subscribe(response => {
        this.zadnjiProizvod = response;
        this.proizvodId = this.zadnjiProizvod.id.toString();
        this.postFile(this.selectedFiles);
        this.getAllProizvodi();
      },
      error => this.toastr.error('Greška pri slanju na API: ' + JSON.parse(JSON.stringify(error.error)))
    )
    this.proizvod.naziv = "";
    this.proizvod.pocetnaKolicina = 0;
    this.proizvod.pocetnaCijena = 0;
    this.proizvod.opis = "";
    this.proizvod.brojKlikova = 0;
  }

  deleteProizvod(proizvod: any) {
    if (confirm("Jeste li sigurni da želite obrisati proizvod: " + proizvod.naziv + "?")) {
      this.dataService.deleteData(proizvod.id).subscribe(response => {
          this.getAllProizvodi()
        },
        error => {
          console.error('Greška prilikom brisanja objekta: ', JSON.parse(JSON.stringify(error.error)));
        })
    }
  }

  promijeniGlavnuKategoriju(glavnaKategorijaId: number) {
    if (this.glavneKategorije.find((obj) => obj.id == glavnaKategorijaId) != null) {
      this.glavnaKategorijaFilter.id = glavnaKategorijaId;
      // this.getAllProizvodiGlavneKategorijeFilter();

    } else {
      this.glavnaKategorijaFilter.id = 0;
      this.glavnaKategorijaFilter.naziv = "";
    }
    //this.getAllProizvodiFilter();
    this.getAllKategorijeFilter();
  }


  promijeniKategoriju(kategorijaID: number) {
    if (this.kategorije.find((obj) => obj.id == kategorijaID) != null) {
      this.kategorijaFilter.id = kategorijaID;
      //this.getAllProizvodiKategorijeFilter();

    } else {
      this.kategorijaFilter.id = 0;
      this.kategorijaFilter.naziv = "";
    }
   // this.getAllProizvodiFilter();
      this.getAllPotkategorijeByKategorija();
  }

  promijeniPotkategoriju(id: number) {
    if (this.potkategorijeFilter.find((obj) => obj.id == id) != null) {
      this.potkategorijaFilterPretraga.id = id;
      this.page=1;
      //this.getAllProizvodiPotkategorijeFilter();

    } else {
      this.potkategorijaFilterPretraga.id = 0;
      this.potkategorijaFilterPretraga.naziv = "";
    }
    this.getAllProizvodiFilter();
  }

    getAllKategorijeFilter(){
      this.dataService.getKategorijeFilter(this.glavnaKategorijaFilter.id).subscribe(x=>{
        this.kategorije=x;
      })
    }

    getAllPotkategorijeByKategorija(){
      this.dataService.getPotkategorijeFilter(this.kategorijaFilter.id).subscribe(x=>{
        this.potkategorijeFilter=x;
      })
    }


  private getAllProizvodiGlavneKategorijeFilter() {
    this.dataServiceGlavnaKategorija.getData('proizvod-pretragaByGlavnaKategorija?GlavnaKategorijaID=' +
      this.glavnaKategorijaFilter.id.toString()).subscribe(data => {
      this.proizvodiPrikaz = data;
      this.getAllKategorije();
      //this.kategorijePrikaz=this.kategorije;
    });
  }

  resetFiltere() {
    this.promijeniGlavnuKategoriju(0);
    this.promijeniKategoriju(0);
    this.promijeniPotkategoriju(0);
    this.getAllProizvodiFilter();
  }


  //UPLOAD SLIKA -----------------------------------------------
  public selectedFiles: File[] = [];
  public imageUrl:string="";

  private addFileToQueue(file: File) {
    this.selectedFiles.push(file);
  }
  slika:any=null;
  postFile(files: File[]) {
    if (files) {
      for (let index = 0; index < files.length; index++) {
        if (files[index]) {
          this.dataService.postFile(files[index], this.proizvodId).subscribe(() => {
            // console.log ("Uspjesno poslata slika");
          });
        }
      }

    }

  this.selectedFiles=[];


  }

  dodajNovi=false;
  novaSlika:any;
  generisiPreviewSlike(){
    // @ts-ignore
    let f=document.getElementById("slika-input").files[0];

    if(f && this.noviProizvod){
      let fileReader=new FileReader();

      fileReader.onload = async (e:any) => {
        try{
          const originalImageUrl=fileReader.result!.toString();
          const resizedImageUrl=await this.resizeImage(originalImageUrl);

          this.adjustImageToSquare(resizedImageUrl, (adjustedImage: string) => {
            this.noviProizvod.slika=adjustedImage;
            this.novaSlika=this.noviProizvod.slika;
          });
        } catch (error){
        }
      };
      fileReader.readAsDataURL(f);
    }
  }
  mijenjanjeSlike=false;
  editovanaSlika:any;
  generisiPreviewSlikeEdit(){
    // @ts-ignore
    let f=document.getElementById("slika-edit").files[0];

    if(f && this.odabraniProizvod){
      let fileReader=new FileReader();
      fileReader.onload = async (e:any) => {
        try{
          const originalImageUrl=fileReader.result!.toString();
          const resizedImageUrl=await this.resizeImage(originalImageUrl);

          this.adjustImageToSquare(resizedImageUrl, (adjustedImage: string) => {
            this.proizvodSlika.id=this.odabraniProizvod.id;
            this.proizvodSlika.slika=adjustedImage;
            this.editovanaSlika=this.proizvodSlika.slika;
            this.mijenjanjeSlike=true;
          });
        } catch (error){
        }
      };
      fileReader.readAsDataURL(f);
    }
  }

  adjustImageToSquare(imageDataUrl: string, callback: (adjustedImage: string) => void) {
    const img = new Image();
    img.src = imageDataUrl;

    img.onload = () => {
      const canvas = document.createElement('canvas');
      const ctx = canvas.getContext('2d');
      const maxDimension = Math.max(img.width, img.height);

      canvas.width = maxDimension;
      canvas.height = maxDimension;

      const offsetX = (maxDimension - img.width) / 2;
      const offsetY = (maxDimension - img.height) / 2;

      ctx!.fillStyle = '#ffffff'; // Bijela boja
      ctx!.fillRect(0, 0, maxDimension, maxDimension);
      ctx!.drawImage(img, offsetX, offsetY, img.width, img.height);
      const imageType = imageDataUrl.match(/^data:image\/([a-z]+);/i)?.[1] || 'png'; // Ako tip nije pronađen, koristi 'png' kao zadani

      const adjustedImageDataUrl = canvas.toDataURL('image/'+imageType); // Možete koristiti 'image/png' ako želite PNG format

      callback(adjustedImageDataUrl);
    };
  }


  async resizeImage(imageUrl: string): Promise<string> {
    // Učitaj sliku koristeći Image objekt
    const img = new Image();
    img.src = imageUrl;

    // Sačekaj da se slika učita
    await new Promise(resolve => {
      img.onload = resolve;
    });

    // Ako je najveća dimenzija veća od 800, smanji veličinu slike srazmjerno
    let targetWidth, targetHeight;
    if (Math.max(img.width, img.height) > 800) {
      if (img.width > img.height) {
        targetWidth = 800;
        targetHeight = Math.round((800 / img.width) * img.height);
      } else {
        targetHeight = 800;
        targetWidth = Math.round((800 / img.height) * img.width);
      }
    } else {
      targetWidth = img.width;
      targetHeight = img.height;
    }

    // Stvori canvas za smanjenje veličine slike
    const canvas = document.createElement('canvas');
    canvas.width = targetWidth;
    canvas.height = targetHeight;
    const ctx = canvas.getContext('2d');

    // Smanji sliku na canvasu
    ctx!.drawImage(img, 0, 0, targetWidth, targetHeight);

    // Pretvori canvas u URL slike
    const imageType = imageUrl.match(/^data:image\/([a-z]+);/i)?.[1] || 'png'; // Ako tip nije pronađen, koristi 'png' kao zadani

    const resizedImageUrl = canvas.toDataURL('image/'+imageType); // Možete koristiti 'image/png' ako želite PNG format

    return resizedImageUrl;
  }



  dodajProizvod(){
    this.dataService.postProizvod(this.noviProizvod).subscribe(x=>{
      this.toastr.success("Uspjesno dodat novi proizvod!", "Ok");
      this.dodajNovi=false;
      this.resetujNoviProizvod();
      this.getAllProizvodiFilter();
      this.novaSlika="";
      this.novaSlikaGalerija="";
      this.slikeGalerija=[];
    }, error=>{
      this.toastr.error("Greška sa dodavanjem proizvoda", "Greška")
    })
  }
  resetujNoviProizvod(){
    this.noviProizvod = {
      naziv: "",
      brendID: 0,
      opis: "",
      brojKlikova: 0,
      pocetnaCijena: 0,
      potkategorijaID: 0,
      pocetnaKolicina: 0,
      slika: "",
      popust: 0,
      slikeGalerija:[]
    };
  }
  noviProizvod:ProizvodPost={
    naziv:"",
    brendID:0,
    opis:"",
    brojKlikova:0,
    pocetnaCijena:0,
    potkategorijaID:0,
    pocetnaKolicina:0,
    slika:"",
    popust:0,
    slikeGalerija:[]
  };

  edit=false;
  naziv="";
  pocetnaKolicina=0;
  pocetnaCijena=0;
  brojKlikova=0;
  potkategorijaId=0;
  brendId=0;
  popust=0;

  odabraniProizvod:ProizvodGetById={
    naziv:"",
    brendID:0,
    opis:"",
    brojKlikova:0,
    id:0,
    pocetnaCijena:0,
    potkategorijaID:0,
    pocetnaKolicina:0,
    slikaUrl:"",
    popust:0,
    slikeGalerija:[]
  };
  odaberiProizvod(id:any) {
    this.dataService.getProizvod(id).subscribe(x=>{
      this.odabraniProizvod=x;
      this.editovanaSlika=this.odabraniProizvod.slikaUrl;
      this.edit=true;
    })
  }
  proizvodSlika:ProizvodSlika={
  slika:"",
    id:0
}
  prikaziSliku: boolean=false;
  togglePrikaziSliku() {
    this.prikaziSliku = !this.prikaziSliku;
  }
  editujProizvod() {
  var editovani:ProizvodEdit={
    naziv:this.odabraniProizvod.naziv,
    id:this.odabraniProizvod.id,
    brendID:this.odabraniProizvod.brendID,
    opis:this.odabraniProizvod.opis,
    brojKlikova:this.odabraniProizvod.brojKlikova,
    pocetnaCijena:this.odabraniProizvod.pocetnaCijena,
    pocetnaKolicina:this.odabraniProizvod.pocetnaKolicina,
    popust:this.odabraniProizvod.popust,
    potkategorijaID:this.odabraniProizvod.potkategorijaID
    };
    this.dataService.editProizvod(editovani).subscribe(x=>{
      this.toastr.success("Uspješno editovan proizvod", "Editovanje proizvoda");
      this.edit=false;
      this.getAllProizvodiFilter();
    })

    this.novaSlikaUrediGalerija="";
    if(this.mijenjanjeSlike){
      this.dataService.editSlika(this.proizvodSlika).subscribe(x=>{
        this.toastr.success("Uspješno uređena slika", "Editovanje proizvoda");
        this.getAllProizvodiFilter();
        this.mijenjanjeSlike=false;
      })
    }
  }

  onTableDataChange(event: any){
    this.page=event;
    this.getAllProizvodiFilter();
  }


  protected readonly console = console;

  proizvodiAnalitika:ProizvodAnalitika={
    naziv:"",
    proizvodId:0,
    brojKlikova:[],
    datumi:[],
    dani:[]
  }

  datumi:string[]=[];
  klikovi:number[]=[];
  dani:Dani[]=[];
  graf=false;
  proizvodGrafId:any;
  odaberiProizvodGraf(id:any, datumId=1) {
    this.proizvodGrafId=id;
    this.graf=true;
    this.dataService.getAnalitika(id, datumId).subscribe(x=>{
      this.proizvodiAnalitika=x;
      this.datumi=this.proizvodiAnalitika.datumi;
      this.klikovi=this.proizvodiAnalitika.brojKlikova;
      this.dani=this.proizvodiAnalitika.dani;
      this.createChart1();
    })
  }

  createChart1(){
    const existingChart = Chart.getChart('vrijeme');
    if (existingChart) {
      existingChart.destroy(); // Uništite prethodni grafik ako postoji
    }

    var myChart = new Chart('vrijeme', {
      type: 'line',
      data: {
        labels: this.datumi,
        datasets: [
          {
            label: 'Broj klikova',
            data: this.klikovi,
            type: 'line', // Ovo postavlja tip na bar za glavni set podataka
            //borderWidth: 1,
            order: 1, // Redosled crtanja za barove
            //backgroundColor: 'rgba(75, 192, 192, 0.2)', // Boja ispune barova
            borderColor: 'rgba(75, 192, 192, 1)', // Boja linija oko barova
          },
        ],
      },
      options: {
        plugins:{
          title:{
            display:true,
            text:'Broj klikova po danu'
          }
        },
        scales: {
          y: {
            beginAtZero: true,
            ticks:{
              stepSize:1
            }
          },
        },
      },
    });
  }
  datumId=1;
  novaSlikaGalerija: any;
  slikeGalerija:string[]=[];

  promijeniPeriod() {
    this.odaberiProizvodGraf(this.proizvodGrafId, this.datumId);
  }

  generisiPreviewSlikeGalerija() {
    // @ts-ignore
    let f=document.getElementById("galerija-input").files[0];

    if(f && this.noviProizvod){
      let fileReader=new FileReader();

      fileReader.onload = async (e:any) => {
        try{
          const originalImageUrl=fileReader.result!.toString();
          const resizedImageUrl=await this.resizeImage(originalImageUrl);

          this.adjustImageToSquare(resizedImageUrl, (adjustedImage: string) => {
            this.novaSlikaGalerija=adjustedImage;

            this.slikeGalerija.push(this.novaSlikaGalerija);
            this.noviProizvod.slikeGalerija=this.slikeGalerija;
          });

        } catch (error){

        }
      };
      fileReader.readAsDataURL(f);
    }
  }

  novaSlikaUrediGalerija: any;
  generisiPreviewSlikeGalerijaUredi() {
    // @ts-ignore
    let f=document.getElementById("uredi-galerija-input").files[0];

    if(f && this.noviProizvod){
      let fileReader=new FileReader();

      fileReader.onload = async (e:any) => {
        try{
          const originalImageUrl=fileReader.result!.toString();
          const resizedImageUrl=await this.resizeImage(originalImageUrl);

          this.adjustImageToSquare(resizedImageUrl, (adjustedImage: string) => {
            this.novaSlikaUrediGalerija=adjustedImage;
            let obj:SlikaGalleryPost={
              proizvodId:this.odabraniProizvod.id,
              slika:this.novaSlikaUrediGalerija
            };
            this.dataService.addGalleryPhoto(obj).subscribe(x=>{
              this.getGallerySlike(this.odabraniProizvod.id);
            })
          });
        } catch (error){
        }
      };
      fileReader.readAsDataURL(f);
    }
  }

  ukloniSliku( s: string ) {
    this.slikeGalerija=this.slikeGalerija.filter(element=>element!==s);
  }

  obrisiSliku(id: number) {
    this.dataService.deleteGalleryPhoto(id).subscribe(x=>{
      this.getGallerySlike(this.odabraniProizvod.id);
    })
  }
  getGallerySlike(id:number){
    this.dataService.getGalleryPhotos(id).subscribe(x=>{
      this.odabraniProizvod.slikeGalerija=x;
    })
  }
  public sortiranja:SortiranjaResponseList[]=[];

  promijeniSortiranje() {
    this.page=1;
    this.getAllProizvodiFilter();
  }
  ucitajSortiranja(){
    this.dataServiceProizvod.getSortiranja("sortiranja-getall").subscribe(data=>{
      this.sortiranja=data.sortiranja;
    })
  }



  scroll(pretragaScroll: HTMLDivElement) {
    pretragaScroll.scrollIntoView({behavior:'smooth'});
  }
}

export interface ProizvodPost {
  naziv: string
  pocetnaKolicina: number
  pocetnaCijena: number
  opis: string
  brojKlikova: number
  potkategorijaID: number
  brendID: number
  popust:number
  slika:string|null
  slikeGalerija:string[]|null
}


