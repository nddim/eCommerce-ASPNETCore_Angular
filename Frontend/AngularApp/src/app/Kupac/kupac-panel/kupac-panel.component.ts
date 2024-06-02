import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {DataServiceNavbar} from "../../navbar/data-service-navbar";
import {AuthJwtHelper} from "../../Auth/AuthJwtHelper";
import {AuthJwtDataService} from "../../Auth/AuthJwtDataService";
import {Router, RouterLink} from "@angular/router";
import {NavbarComponent} from "../../navbar/navbar.component";
import {FooterComponent} from "../../footer/footer.component";

@Component({
  selector: 'app-kupac-panel',
  standalone: true,
  imports: [CommonModule, RouterLink, NavbarComponent, FooterComponent],
  templateUrl: './kupac-panel.component.html',
  styleUrl: './kupac-panel.component.css'
})
export class KupacPanelComponent {
  constructor(private dataServiceNavbar:DataServiceNavbar,
              private jwt:AuthJwtHelper,
              private auth:AuthJwtDataService,
              private router:Router) {
  }
  public logOut () {
    this.izbrisiKonekciju();
    //console.log("logout-2");
    //console.log("Odjavili smo se prvi put");

    this.auth.revokeToken().subscribe({});

    this.router.navigate(["/login"]);

  }
  public izbrisiKonekciju(){
    let userId:string=this.jwt.getId();
    this.dataServiceNavbar.removeConnection(userId).subscribe(x=>{

    });
  }
}
