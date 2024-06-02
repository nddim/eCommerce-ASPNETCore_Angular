import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  AdministratorNarudzbeDataService,
  NarudzbaPaged,
  NarudzbePaged,
  StavkeNarudzbePaged
} from "./narudzbe-dataservice";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {SignalRService} from "../../helper/signal-r/signal-r.service";
import {NgxPaginationModule} from "ngx-pagination";
import {RouterLink, RouterLinkActive} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-narudzbe',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, NgxPaginationModule, RouterLinkActive, RouterLink],
  templateUrl: './narudzbe.component.html',
  styleUrl: './narudzbe.component.css'
})
export class NarudzbeComponent {
  page: number = 1;
  count: number = 100;
  tableSize: number = 10;
  constructor(private dataService:AdministratorNarudzbeDataService,
              private signalRService:SignalRService,
              private toastr:ToastrService) {
  }
  ngOnInit(){
    this.ucitajNarudzbePaged();
    this.getStatuse();
    this.signalRService.otvori_ws_konekciju();
  }
  narudzbe:NarudzbaPaged[]=[];

  ime="";
  narudzbePaged:NarudzbePaged[]=[];

  stavkeNarudzbe:StavkeNarudzbePaged[]=[];

  ucitajNarudzbePaged(){
    this.dataService.getNarudzbePaged(this.page, this.tableSize, this.ime).subscribe(x=>{
      this.narudzbePaged=x.narudzbe;
      this.page=x.currentPage;
      this.count=x.totalCount;
      this.tableSize=x.pageSize;
      this.narudzbe=this.narudzbePaged.map(x=>x.narudzba);
    })
  }
  obrisiNarudzbu(id:number){
    this.dataService.obrisiNarudzbu(id).subscribe({
      next: () => {
        this.toastr.success("Obrisana narudzba");
      },
      error: (error) => {
        console.error("Greška prilikom brisanja narudžbe:", error);
        this.toastr.error("Došlo je do greške prilikom brisanja narudžbe.");
      }
    });
    this.ucitajNarudzbePaged();
  }
  edit=false;
  statusNarudzbe:any;
  getStatuse(){
    this.dataService.getStatus().subscribe(x=>{
      this.statusNarudzbe=x;
    });
  }
  statusKupca:any;
  async getKupacStatus(id: number): Promise<void> {
      try {
          const statusId = await this.dataService.getUserStatus(id).toPromise();
          this.statusKupca = statusId;
      } catch (error) {
          console.error('Error fetching status:', error);
      }
  }
  narudzbaId:number=0;
  narudzbaKupacId:number=0;
  narudzbaIme:string="";
  narudzbaPrezime:string="";
  narudzbaEmail:string="";
  kupacNarudzba:any;
    async getKupacNarudzba(id: number) {
        try {
            this.kupacNarudzba = await this.dataService.getKupacNarudzba(id).toPromise();
            this.narudzbaId = this.kupacNarudzba.narudzbaId;
            this.narudzbaKupacId = this.kupacNarudzba.kupacId;
            this.narudzbaIme = this.kupacNarudzba.ime;
            this.narudzbaPrezime = this.kupacNarudzba.prezime;
            this.narudzbaEmail = this.kupacNarudzba.email;
        } catch (error) {
            console.error('Error fetching kupac narudzba:', error);
        }
    }
    editStatus:number=0;
  onTableDataChange(event: number) {
    this.page=event;
    // console.log(this.page);
    this.ucitajNarudzbePaged();
  }
    editNarudzba(){
      var noviObj : Edit = {
        id:this.narudzbaId,
          statusNarudzbeId:this.editStatus
      }
        this.dataService.editNarudzba(noviObj).subscribe(
            x => {
                // alert("Uspjesno azurirana narudzba")
            },
            error => {
                // Handle error
                this.toastr.error("Greska prilikom azuriranja narudzba")
                console.error('Greska prilikom azuriranja narudzba : ', error);
            }
        );
      this.edit=false;
      this.ucitajNarudzbePaged();
    }
  protected readonly console = console;

  getStavke(narudzbaId: number) {
    this.stavkeNarudzbe=[];
    this.narudzbePaged.map(x=>x.stavkeNarudzbe.forEach(s=>{
      if(s.narudzbaId===narudzbaId)
        this.stavkeNarudzbe.push(s);
    }));

  }

}
export interface Edit {
    id: number
    statusNarudzbeId: number
}
