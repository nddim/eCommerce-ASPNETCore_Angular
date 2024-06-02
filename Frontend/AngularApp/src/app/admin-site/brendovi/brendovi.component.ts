import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {DataServiceBrend} from "./data-service";
import {BrendoviGetAllResponse, BrendoviGetPaged, BrendoviPostRequest, BrendSearch} from "./brendovi-get-all-response";
import {FormsModule} from "@angular/forms";
import {RouterLink, RouterOutlet} from "@angular/router";
import {NgxPaginationModule} from "ngx-pagination";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-brendovi',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, RouterOutlet, NgxPaginationModule],
  providers:[DataServiceBrend], //mora ova linija da se doda
  templateUrl: './brendovi.component.html',
  styleUrl: './brendovi.component.css'
})
export class BrendoviComponent implements OnInit{
  page: number = 1;
  count: number = 100;
  tableSize: number = 10;

  brendovi:BrendoviGetAllResponse[]=[];
  brendoviPrikaz:any[]=[];
  brend: BrendoviPostRequest= {
    naziv:""
  }
  filterBrend:BrendSearch={
    naziv:""
};
  oldBrendObj: any;
  constructor(private readonly dataService: DataServiceBrend,
              private toastr:ToastrService) {  }

  ngOnInit(): void {
    this.getAllBrends();
  }
  brendoviPaged:BrendoviGetPaged={
    brendovi:[],
    currentPage:0,
    pageSize:0,
    totalPages:0,
    totalCount:0
  }
  getAllBrends(){
    this.dataService.getDataPaged(this.filterBrend.naziv, this.page, this.tableSize).subscribe(data => {
      this.brendoviPaged=data;
      this.brendovi = this.brendoviPaged.brendovi;
      this.brendoviPrikaz=this.brendovi;
      this.page=data.currentPage;
      this.count=data.totalCount;
      this.tableSize=data.pageSize;
    });
  }

  sendData() {
    const dataToSend:BrendoviPostRequest={
      naziv:this.brend.naziv
    }
    this.dataService.postData(dataToSend, 'brend-dodaj')
      .subscribe(response=>{

          this.getAllBrends();
        },
        error => this.toastr.error('Greška pri slanju na API: '+
          JSON.parse(JSON.stringify(error.error)))
      )
    this.brend.naziv="";
  }

  modifikujBrend(brend: any) {
    this.oldBrendObj=JSON.stringify(brend);
    this.brendoviPrikaz.forEach(obj=>{
      obj.isEdit=false;
    });
    brend.isEdit=true;
  }

  cancelModification(brend: any) {
    const oldObj=JSON.parse(this.oldBrendObj);
    brend.naziv=oldObj.naziv;
    brend.isEdit=false;
  }

  izbrisiBrend(brend: any) {
    if(confirm("Jeste li sigurni da želite obrisati brend: "+brend.naziv+"?")) {
      this.dataService.deleteData(brend.id).subscribe(response=>{
          this.page=1;
          this.getAllBrends()
        },
        error=>{
          console.error('Greška prilikom brisanja objekta: ',JSON.parse(JSON.stringify(error.error)));
        })
    }

  }

  saveBrendModification(brend: any) {

    if(brend.naziv!="")
    {
      this.dataService.editData(brend as BrendoviGetAllResponse).subscribe(response=>{

          this.getAllBrends();

        },
        error => this.toastr.error('Greška pri slanju na API: '+JSON.parse(JSON.stringify(error.error)))
      );
    }
    else{
      this.toastr.error("Brend ne može biti prazan string!")
    }


  }

  onTableDataChange(event: number) {
    this.page=event;
    //console.log(this.page);
    this.getAllBrends();
    window.scrollTo({ top: 0, behavior: 'instant' });

  }
}
