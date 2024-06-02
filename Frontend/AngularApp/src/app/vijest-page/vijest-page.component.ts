import { Component } from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import {NavbarComponent} from "../navbar/navbar.component";
import {FooterComponent} from "../footer/footer.component";
import {ActivatedRoute, RouterLink} from "@angular/router";
import {VijestpageDataservice} from "./vijestpage-dataservice";

@Component({
  selector: 'app-vijest-page',
  standalone: true,
  imports: [CommonModule, NavbarComponent, FooterComponent, RouterLink, NgOptimizedImage],
  templateUrl: './vijest-page.component.html',
  styleUrl: './vijest-page.component.css'
})
export class VijestPageComponent {
  constructor(private route:ActivatedRoute, private dataService:VijestpageDataservice) {
  }
  vijestId:string="";
  ngOnInit(){
    window.scrollTo({ top: 0, behavior: 'smooth' });

    this.route.params.subscribe((params)=> {
      this.vijestId = params['id'];
      this.getVijesti();
    })
    this.getVijestiByDate();
  }
  vijesti:any;
  getVijesti(){
    this.dataService.getVijest(this.vijestId).subscribe(x=>{
      this.vijesti=x;
    })
  }
  vijestiLista:any;
  getVijestiByDate(){
    this.dataService.getVijestLista().subscribe(x=>{
      this.vijestiLista=x;
    });
  }

  getTimeElapsed(dateString: string): string {
    const date = new Date(dateString);
    const now = new Date();
    const diff = now.getTime() - date.getTime();
    const minutes = Math.floor(diff / 60000);
    if (minutes < 60) {
      return minutes + ' minutes ago';
    } else {
      const hours = Math.floor(minutes / 60);
      if (hours < 24) {
        return hours + ' hours ago';
      } else {
        const days = Math.floor(hours / 24);
        return days + ' days ago';
      }
    }
  }
}
