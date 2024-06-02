import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ProizvodGetAllResponse} from "../admin-site/proizvodi/proizvod-get-all-response";
import {DataServiceProizvod} from "../admin-site/proizvodi/data-service-proizvod";
import {RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";
import {PopustiDataservice} from "../popusti-page/popusti-dataservice";
import {NavbarComponent} from "../navbar/navbar.component";
import {FooterComponent} from "../footer/footer.component";

@Component({
  selector: 'app-proizvodi-novi-page',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive, NavbarComponent, FooterComponent],
  providers:[DataServiceProizvod],
  templateUrl: './proizvodi-novi-page.component.html',
  styleUrl: './proizvodi-novi-page.component.css'
})
export class ProizvodiNoviPageComponent {
  proizvodi: ProizvodGetAllResponse[] = [];
  proizvodiPrikaz: any[] = [];

  constructor(private readonly dataServiceProizvod: PopustiDataservice) {
  }

  ngOnInit() {
    this.getAllNoviProizvod();
  }

  getAllNoviProizvod() {
    this.dataServiceProizvod.getData('proizvod-novi-pretraga').subscribe(
      data => {
        this.proizvodi = data;
        this.proizvodiPrikaz = this.proizvodi;
      });
  }
}

