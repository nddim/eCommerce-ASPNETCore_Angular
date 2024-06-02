import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {DataServiceKategorija} from "./data-service-kategorija";
import {DataServiceGlavnaKategorija} from "../glavne-kategorije/data-service-glavna-kategorija";
import {KategorijaFind, KategorijaGetAllResponse, KategorijaPostRequest} from "./Kategorija-get-all-response";
import {GlavnaKategorijaGetAllResponse} from "../glavne-kategorije/glavna-kategorija-get-all-response";
import {FormsModule} from "@angular/forms";
import {RouterLink, RouterOutlet} from "@angular/router";
import {NgxPaginationModule} from "ngx-pagination";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-kategorije',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, RouterOutlet, NgxPaginationModule],
  providers:[DataServiceKategorija, DataServiceGlavnaKategorija], //mora ova linija da se doda
  templateUrl: './kategorije.component.html',
  styleUrl: './kategorije.component.css'
})
export class KategorijeComponent {
  page: number = 1;
  count: number = 100;
  tableSize: number = 10;

  kategorije:KategorijaGetAllResponse[]=[];
  kategorijePrikaz:any[]=[];
  glavnaKategorijaFilter:KategorijaGetAllResponse={
    id:0,
    naziv:"",
    glavnaKategorijaID:0,
    nazivGlavneKategorije:""
  }
  kategorija: KategorijaPostRequest= {
    naziv: "",
    glavnaKategorijaID: 1
  }
  glavneKategorije:GlavnaKategorijaGetAllResponse[]=[];
  filterKategorija:KategorijaFind={
    naziv:""
}
  oldKategorijaObj: any;
  odabranaGlavnaKategorija: string="";
  constructor(private readonly dataService: DataServiceKategorija,
              private readonly dataServiceGlavneKategorije: DataServiceGlavnaKategorija,
              private toastr:ToastrService) {  }

  ngOnInit(): void {
    this.getAllKategorije();
    this.getAllGlavneKategorije();
  }

  getAllKategorije(){
    this.dataService.getPaged(this.glavnaKategorijaFilter.naziv,this.glavnaKategorijaFilter.glavnaKategorijaID,this.page, this.tableSize).subscribe(data => {
      this.kategorije = data.kategorije;
      this.page=data.currentPage;
      this.count=data.totalCount;
      this.tableSize=data.pageSize;
      this.kategorijePrikaz=this.kategorije;
    });
  }

  getAllGlavneKategorije(){
    this.dataServiceGlavneKategorije.getData('glavna-kategorija-pretraga').subscribe(data => {
      this.glavneKategorije = data;
      this.kategorijePrikaz=this.kategorije;
    });
  }

  sendData() {
    const dataToSend:KategorijaPostRequest={
      naziv:this.kategorija.naziv,
      glavnaKategorijaID:this.kategorija.glavnaKategorijaID
    }
    if(dataToSend.naziv!="")
    {
      this.dataService.postData(dataToSend, 'kategorija-dodaj').subscribe(response=>{

          this.getAllKategorije();
        },
        error => this.toastr.error('Greška pri slanju na API: '+JSON.parse(JSON.stringify(error.error)))
      )
      this.kategorija.naziv="";
    }
    else {
      this.toastr.error("Kategorija ne može biti prazan string!");
    }

  }

  modifikujKategoriju(kategorija: any) {
    this.oldKategorijaObj=JSON.stringify(kategorija);
    this.kategorijePrikaz.forEach(obj=>{
      obj.isEdit=false;
    });
    kategorija.isEdit=true;
  }

  cancelModification(kategorija: any) {
    const oldObj=JSON.parse(this.oldKategorijaObj);
    kategorija.naziv=oldObj.naziv;
    kategorija.isEdit=false;
  }

  izbrisiKategoriju(kategorija: any) {
    if(confirm("Jeste li sigurni da želite obrisati kategoriju: "+kategorija.naziv+"?")) {
      this.dataService.deleteData(kategorija.id).subscribe(response=>{

          this.getAllKategorije()
        },
        error=>{
          console.error('Greška prilikom brisanja objekta: ',JSON.parse(JSON.stringify(error.error)));
        })
    }

  }

  saveCategoryModification(kategorija: any) {

    if(kategorija.naziv!="")
    {
      this.dataService.editData(kategorija as KategorijaGetAllResponse).subscribe(response=>{

          this.getAllKategorije();
        },
        error => this.toastr.error('Greška pri slanju na API: '+JSON.parse(JSON.stringify(error.error)))
      );
    }
    else {
      this.toastr.error("Kategorija ne može biti prazan string!");
    }
  }

  promijeniGlavnuKategoriju(id: number) {
    if(this.glavneKategorije.find((obj)=>obj.id==id)!=null)
    {
      this.glavnaKategorijaFilter.glavnaKategorijaID=id;
    }
    else {
      this.glavnaKategorijaFilter.glavnaKategorijaID=0;
      this.glavnaKategorijaFilter.naziv="";
    }
    this.getAllKategorije();
  }

  onTableDataChange(event: number) {
    this.page=event;
    //console.log(this.page);
    this.getAllKategorije();
  }
}
