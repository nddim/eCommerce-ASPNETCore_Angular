import { Component } from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import {PaginatorModule} from "primeng/paginator";
import {AdminVijestDataService, Vijesti, VijestSlika} from "./adminvijest-dataservice";
import {EditorComponent} from "@tinymce/tinymce-angular";
import {ProizvodSlika} from "../proizvodi/data-service-proizvod";
import {ToastrService} from "ngx-toastr";
import {NgxPaginationModule} from "ngx-pagination";
import {Vijest} from "../../vijest/vijest-dataservice";

@Component({
  selector: 'app-admin-vijest',
  standalone: true,
  imports: [CommonModule, PaginatorModule, NgOptimizedImage, EditorComponent, NgxPaginationModule],
  templateUrl: './admin-vijest.component.html',
  styleUrl: './admin-vijest.component.css'
})
export class AdminVijestComponent {
  page: number = 1;
  count: number = 100;
  tableSize: number = 10;
  constructor(private dataService:AdminVijestDataService,
              private readonly toastr:ToastrService) {
  }
  vijestId:string = "0";

  dodajNova:boolean=false;
  edit:boolean=false;
  ngOnInit(): void {
    this.getVijesti();
  }
  vijesti:Vijest[]=[];
  getVijesti(){
    return this.dataService.getPaged(this.page, this.tableSize).subscribe(x=>{
      this.vijesti = x.vijesti;
      this.page=x.currentPage;
      this.count=x.totalCount;
      this.tableSize=x.pageSize;
    })
  }
  novaSlika:any;
  novaVijest:VijestDodaj={
   naziv:"",
    tekst:"",
    slika:"",
  }

  //UPLOAD SLIKA -----------------------------------------------
  public selectedFiles: File[] = [];
  public imageUrl:string="";
  private addFileToQueue(file: File) {
    this.selectedFiles.push(file);
  }

  slika:any=null;

  generisiPreviewSlike() {
    // @ts-ignore
    let f = document.getElementById("slika-input").files[0];

    if (f && this.novaVijest) {
      let fileReader = new FileReader();
      fileReader.onload = async (e: any) => {
        try {
          const originalImageUrl = fileReader.result!.toString();
          const resizedImageUrl = await this.resizeImage(originalImageUrl);

          this.adjustImageToSquare(resizedImageUrl, (adjustedImage: string) => {
            this.novaVijest.slika = adjustedImage;
            this.novaSlika = this.novaVijest.slika;
          });

        } catch (error) {

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

      const adjustedImageDataUrl = canvas.toDataURL('image/png'); // Možete koristiti 'image/png' ako želite PNG format

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
    const resizedImageUrl = canvas.toDataURL('image/png'); // Možete koristiti 'image/png' ako želite PNG format

    return resizedImageUrl;
  }
  mijenjanjeSlike=false;
  editovanaSlika:any;

  generisiPreviewSlikeEdit(){
    // @ts-ignore
    let f=document.getElementById("slika-edit").files[0];
    if(f && this.odabranaVijest){
      let fileReader=new FileReader();

      fileReader.onload = async (e:any) => {
        try{
          const originalImageUrl=fileReader.result!.toString();
          const resizedImageUrl=await this.resizeImage(originalImageUrl);

          this.adjustImageToSquare(resizedImageUrl, (adjustedImage: string) => {
            this.vijestSlika.id=this.odabranaVijest.id;
            this.vijestSlika.slika=adjustedImage;
            this.editovanaSlika=this.vijestSlika.slika;
            this.mijenjanjeSlike=true;
          });

        } catch (error){
        }
      };
      fileReader.readAsDataURL(f);
    }
  }
  dodajVijest(){
    this.dataService.DodajVijest(this.novaVijest).subscribe(x=>{
      this.toastr.success("Uspjesno dodat nova vijest!");
      this.dodajNova=false;
      this.getVijesti();
    }, error=>{
      this.toastr.error("Greška sa dodavanjem vijesti")
    });
  }
  odaberiVijest(id:any){
    this.dataService.GetVijestBydId(id).subscribe(x=>{
      this.odabranaVijest=x;
      this.editovanaSlika=this.odabranaVijest.slikaUrl;
      this.edit=true;
      this.odabranaVijest.id=x.id;
    });
  }
  deleteVijest(id:number){
    if (confirm("Jeste li sigurni da želite obrisati ovu vijest?")) {
      this.dataService.deleteVijest(id).subscribe(x=>{
        this.toastr.success("Uspješno izbrisana vijest", "Brisanje vijesti");
        this.getVijesti();
      })
    }
  }
  odabranaVijest:VijestGetById={
    id:0,
    naziv:"",
    datum:"",
    tekst:"",
    autor:"",
    slikaUrl:"",
    brojKlikova:0
  }
  vijestSlika:VijestSlika={
    slika:"",
    id:0
  }
  editVijest(){
    var editovanaVijest:VijestEdit={
      id:this.odabranaVijest.id,
      naziv:this.odabranaVijest.naziv,
      tekst:this.odabranaVijest.tekst
    }
    // console.log(editovanaVijest);
    this.dataService.editVijest(editovanaVijest).subscribe(x=>{
      this.toastr.success("Uspjesno dodat azurirana vijest!");
      this.edit=false;
      this.getVijesti();
    }, error => {
      this.toastr.error("Greska sa azuriranjem vijesti")
    });

    if(this.mijenjanjeSlike){
      this.dataService.editSlika(this.vijestSlika).subscribe(x=>{
        this.toastr.success("Uspješno uređena slika", "Azuriranje vijesti");
        // alert("uredjena slika");
        this.getVijesti();
        this.mijenjanjeSlike=false;
      })
    }
  }

  onTableDataChange(event: number) {

  }
}
export interface VijestDodaj {
  naziv: string
  tekst: string
  slika: string
}
export interface VijestEdit {
  id: number
  naziv: string
  tekst: string
}
export interface VijestGetById {
  id: number
  naziv: string
  datum: string
  tekst: string
  autor: string
  slikaUrl: string
  brojKlikova: number
}
