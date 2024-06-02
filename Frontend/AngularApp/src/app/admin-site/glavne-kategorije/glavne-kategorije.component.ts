import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {DataServiceGlavnaKategorija} from "./data-service-glavna-kategorija";
import {
  GlavnaKategorijaFind,
  GlavnaKategorijaGetAllResponse,
  GlavnaKategorijaPostRequest
} from "./glavna-kategorija-get-all-response";
import {RouterLink, RouterOutlet} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-glavne-kategorije',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, RouterLink, RouterOutlet],
  providers:[DataServiceGlavnaKategorija], //mora ova linija da se doda
  templateUrl: './glavne-kategorije.component.html',
  styleUrl: './glavne-kategorije.component.css'
})
export class GlavneKategorijeComponent {
  kategorije:GlavnaKategorijaGetAllResponse[]=[];
  kategorijePrikaz:any[]=[];
  kategorija: GlavnaKategorijaPostRequest= {
    naziv:""
  }
  filterGlavnaKategorija:GlavnaKategorijaFind={
    naziv:""
}
  oldKategorijaObj: any;
  constructor(private readonly dataService: DataServiceGlavnaKategorija,
              private toastr:ToastrService) {  }

  ngOnInit(): void {
    this.getAllGlavneKategorije();
  }

  getAllGlavneKategorije(){
    this.dataService.getData('glavna-kategorija-pretraga?Naziv='+this.filterGlavnaKategorija.naziv).subscribe(data => {
      this.kategorije = data;
      this.kategorijePrikaz=this.kategorije;
    });
  }

  sendData() {
    const dataToSend:GlavnaKategorijaPostRequest={
      naziv:this.kategorija.naziv
    }
    if(dataToSend.naziv!="")
    {
      this.dataService.postData(dataToSend, 'glavna-kategorija-dodaj').subscribe(response=>{

          this.getAllGlavneKategorije();
        },
        error => this.toastr.error('Greška pri slanju na API: '+JSON.parse(JSON.stringify(error.error)))
      )
      this.kategorija.naziv="";
    }
    else {
      this.toastr.error("Glavna kategorija ne može biti prazan string!");
    }

  }
  editBrend(){
  }
  modifikujGlavnuKategoriju(kategorija: any) {
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

  izbrisiGlavnuKategoriju(kategorija: any) {
    if(confirm("Jeste li sigurni da želite obrisati glavnu kategoriju: "+kategorija.naziv+"?")) {
      this.dataService.deleteData(kategorija.id).subscribe(response=>{

          this.getAllGlavneKategorije()
        },
        error=>{
          console.error('Greška prilikom brisanja objekta: ',JSON.parse(JSON.stringify(error.error)));
        })
    }

  }

  saveKategorijaModification(kategorija: any) {
    if(kategorija.naziv!="")
    {
      this.dataService.editData(kategorija as GlavnaKategorijaGetAllResponse).subscribe(response=>{
          this.getAllGlavneKategorije();
        },
        error => this.toastr.error('Greška pri slanju na API: '+JSON.parse(JSON.stringify(error.error)))
      );
    }
    else {
      this.toastr.error("Glavna kategorija ne može biti prazan string!");
    }
  }
}
