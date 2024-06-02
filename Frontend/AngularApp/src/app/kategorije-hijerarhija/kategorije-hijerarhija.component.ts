import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {NavbarComponent} from "../navbar/navbar.component";
import {FooterComponent} from "../footer/footer.component";
import {
  GlavnaKategorija, GlavnaKategorijaResponse,
  KategorijeHijerarhijaDataService,
} from "./Kategorije-Hijerarhija-DataService";
import {RouterLink, RouterLinkActive} from "@angular/router";

@Component({
  selector: 'app-kategorije-hijerarhija',
  standalone: true,
  imports: [CommonModule, NavbarComponent, FooterComponent, RouterLink, RouterLinkActive],
  templateUrl: './kategorije-hijerarhija.component.html',
  styleUrl: './kategorije-hijerarhija.component.css'
})
export class KategorijeHijerarhijaComponent implements OnInit{
  constructor(private dataService:KategorijeHijerarhijaDataService) {
  }

  ngOnInit(): void {
    this.ucitajKategorije();
  }

  kategorije: GlavnaKategorijaResponse = {
    glavneKategorije: [{
      id: 0,
      naziv: '',
      kategorije: [
        {
          id: 0,
          naziv: '',
          potkategorije: [
            {
              id: 0,
              naziv: '0'
            },
          ]
        },
      ]
    }]
  }

  glavneKategorije:GlavnaKategorija[]=[{
    id:0,
    naziv:'',
    kategorije: [
      {
        id: 0,
        naziv: '',
        potkategorije: [
          {
            id: 0,
            naziv: '0'
          },
        ]
      },
    ]
  }]

  private ucitajKategorije() {
    this.dataService.getKategorije().subscribe(x=>{
      this.kategorije=x;
      this.glavneKategorije= this.kategorije.glavneKategorije;
    })
  }
}
