import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormControl, FormsModule} from "@angular/forms";
import {DataServicePotkategorija} from "./data-service-potkategorija";
import {PotkategorijaGetAllResponse, PotkategorijaPostRequest} from "./Potkategorija-get-all-response";
import {DataServiceKategorija} from "../kategorije/data-service-kategorija";
import {KategorijaGetAllResponse} from "../kategorije/Kategorija-get-all-response";
import {RouterLink, RouterOutlet} from "@angular/router";
import {NgxPaginationModule} from "ngx-pagination";
import {MatAutocompleteModule} from "@angular/material/autocomplete";
import {MatInputModule} from "@angular/material/input";
import {map, Observable, startWith} from "rxjs";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-potkategorije',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, RouterOutlet, NgxPaginationModule, MatAutocompleteModule, MatInputModule],
  providers:[DataServicePotkategorija, DataServiceKategorija], //mora ova linija da se doda
  templateUrl: './potkategorije.component.html',
  styleUrl: './potkategorije.component.css'
})
export class PotkategorijeComponent {
  page: number = 1;
  count: number = 100;
  tableSize: number = 10;




  potkategorije:PotkategorijaGetAllResponse[]=[];
  potkategorijePrikaz:any[]=[];
  kategorije:KategorijaGetAllResponse[]=[];
  potkategorija: PotkategorijaPostRequest= {
    naziv:"",
    kategorijaID:0
  }
  kategorijaFilter:PotkategorijaGetAllResponse={
    id:0,
    naziv:"",
    kategorijaID:0,
    kategorijaNaziv:""
  }
  oldPotkategorijaObj: any;
  constructor(private readonly dataService: DataServicePotkategorija,
              private readonly dataServiceKategorija: DataServiceKategorija,
              private toastr:ToastrService) {  }

  ngOnInit(): void {
    this.getAllPotkategorije();
    this.getAllKategorije();
  }

  getAllPotkategorije(){
    this.dataService.getPaged(this.kategorijaFilter.naziv,this.kategorijaFilter.kategorijaID, this.page, this.tableSize).subscribe(
        data => {
      this.potkategorije = data.potkategorije;
      this.potkategorijePrikaz=this.potkategorije;
      this.page=data.currentPage;
      this.count=data.totalCount;
      this.tableSize=data.pageSize;
    });
  }

  getAllKategorije(){
    this.dataServiceKategorija.getData('kategorija-pretraga').subscribe(data => {
      this.kategorije = data;
      this.potkategorijePrikaz=this.potkategorije;
    });
  }

  sendData() {
    const dataToSend:PotkategorijaPostRequest={
      naziv:this.potkategorija.naziv,
      kategorijaID:this.potkategorija.kategorijaID
    }
    this.dataService.postData(dataToSend, 'potkategorija-dodaj').subscribe(response=>{
        //console.log('Uspjesno poslato na API!'+response);
       this.getAllPotkategorije();
      },
      error => this.toastr.error('Greška pri slanju na API: '+JSON.parse(JSON.stringify(error.error)))
    )
    this.potkategorija.naziv="";
  }


  editBrend(){
  }


  modifikujPotkategoriju(kategorija: any) {
    this.oldPotkategorijaObj=JSON.stringify(kategorija);
    this.potkategorijePrikaz.forEach(obj=>{
      obj.isEdit=false;
    });
    kategorija.isEdit=true;
  }

  cancelModification(kategorija: any) {
    const oldObj=JSON.parse(this.oldPotkategorijaObj);
    kategorija.naziv=oldObj.naziv;
    kategorija.isEdit=false;
  }

  izbrisiPotkategoriju(kategorija: any) {
    if(confirm("Jeste li sigurni da želite obrisati potkategoriju: "+kategorija.naziv+"?")) {
      this.dataService.deleteData(kategorija.id).subscribe(response=>{
          //console.log('Objekat uspjesno obrisan!', response);
          this.getAllPotkategorije()
        },
        error=>{
          console.error('Greška prilikom brisanja objekta: ',JSON.parse(JSON.stringify(error.error)));
        })
    }

  }

  savePotkategorijaModification(potkategorija: any) {
    //console.log("editujemo potkategoriju");
    this.dataService.editData(potkategorija as PotkategorijaGetAllResponse).subscribe(response=>{
        //console.log('Uspjesno poslato na API!'+response);
        this.getAllPotkategorije();
      },
      error => this.toastr.error('Greška pri slanju na API: '+JSON.parse(JSON.stringify(error.error)))
    );
    this.getAllPotkategorije();
  }


  promijeniKategoriju(kategorijaID: number) {
    if(this.kategorije.find((obj)=>obj.id==kategorijaID)!=null)
    {
      this.kategorijaFilter.kategorijaID=kategorijaID;
    }
    else {
      this.kategorijaFilter.kategorijaID=0;
      this.kategorijaFilter.naziv="";
    }
    this.page=1;
    this.getAllPotkategorije();
  }

  onTableDataChange(event: number) {
    this.page=event;
    //console.log(this.page);
    this.getAllPotkategorije();
  }


}
