import { Component } from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import {NavbarComponent} from "../navbar/navbar.component";
import {FooterComponent} from "../footer/footer.component";
import {VijestDataservice} from "./vijest-dataservice";
import {RouterLink} from "@angular/router";
import {NgxPaginationModule} from "ngx-pagination";

@Component({
  selector: 'app-vijest',
  standalone: true,
  imports: [CommonModule, NavbarComponent, FooterComponent, NgOptimizedImage, RouterLink, NgxPaginationModule],
  templateUrl: './vijest.component.html',
  styleUrl: './vijest.component.css'
})
export class VijestComponent {
  page: number = 1;
  count: number = 100;
  tableSize: number = 9;
  constructor(private dataService: VijestDataservice) {
  }
  ngOnInit(){
    window.scrollTo({top:0, behavior:'smooth'});
    this.getVijesti();
  }
  vijesti:any;
  getVijesti(){
    this.dataService.getPaged(this.page, this.tableSize).subscribe(x=>{
      this.vijesti=x.vijesti;
      this.page=x.currentPage;
      this.count=x.totalCount;
      this.tableSize=x.pageSize;
    });
  }
  onTableDataChange(event: number) {
    this.page=event;
    //console.log(this.page);
    this.getVijesti();
    window.scrollTo({ top: 0, behavior: 'instant' });
  }
}
