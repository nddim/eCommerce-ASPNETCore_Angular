import {Component, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA} from '@angular/core';
import { CommonModule } from '@angular/common';
import { NarudzbaDetailsDataService} from "./narudzba-details-dataservice";
import {ActivatedRoute} from "@angular/router";
import {dateTimestampProvider} from "rxjs/internal/scheduler/dateTimestampProvider";
import {FormControl, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {defineComponents, IgcRatingComponent} from "igniteui-webcomponents";
import {MojConfig} from "../../../assets/moj-config";
import {NavbarComponent} from "../../navbar/navbar.component";
import {FooterComponent} from "../../footer/footer.component";
import {ToastrService} from "ngx-toastr";
defineComponents(IgcRatingComponent);
@Component({
  selector: 'app-kupac-narudzba-details',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, NavbarComponent, FooterComponent],
  templateUrl: './kupac-narudzba-details.component.html',
  styleUrl: './kupac-narudzba-details.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class KupacNarudzbaDetailsComponent {
  constructor(private dataService:NarudzbaDetailsDataService,
              private route:ActivatedRoute,
              private toastr:ToastrService
              ) {
  }
  narudzbaId:string="";
  ngOnInit(){
    window.scrollTo({ top: 0, behavior: 'smooth' });

    this.route.params.subscribe((params)=> {
      this.narudzbaId = params['id'];
    })
    this.getNarudzbaDetails();
    this.getStavkeNarudzbe();
  }
  narudzbaDetails:any;
  getNarudzbaDetails(){
    this.dataService.GetNarudzaDetails(this.narudzbaId).subscribe(x=>{
      this.narudzbaDetails=x;
    });
  }
  stavkeNarudzbe:any;
  getStavkeNarudzbe(){
    this.dataService.GetStavkeNarudzbe(this.narudzbaId).subscribe(x=>{
      this.stavkeNarudzbe=x;
      //console.log(x);
    });
  }
  inputProizvodId:number=0;
  ratingInputVrijednost:any;
  doodajRating(){
    var obj = {
      proizvodId:this.inputProizvodId,
      vrijednost:this.ratingInputVrijednost
    }
    //console.log( this.inputProizvodId);
    this.dataService.doodajRating(obj).subscribe(x=>{
      this.toastr.success("Uspjesno dodana recenzija");
    }, error => {this.toastr.error("Greska prilikom dodavanja recenzije")});
  }
  protected readonly dateTimestampProvider = dateTimestampProvider;
}
