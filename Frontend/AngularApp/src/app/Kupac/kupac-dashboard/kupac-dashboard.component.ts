import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {Router, RouterLink, RouterOutlet} from "@angular/router";
import {AdminPanelNavbarComponent} from "../../admin-site/admin-panel-navbar/admin-panel-navbar.component";
import {AdminPanelSidebarComponent} from "../../admin-site/admin-panel-sidebar/admin-panel-sidebar.component";
import {KupacSidebarComponent} from "../kupac-sidebar/kupac-sidebar.component";
import {KupacNavbarComponent} from "../kupac-navbar/kupac-navbar.component";
import {NavbarComponent} from "../../navbar/navbar.component";
import {FooterComponent} from "../../footer/footer.component";
import {DataServiceNavbar} from "../../navbar/data-service-navbar";
import {AuthJwtHelper} from "../../Auth/AuthJwtHelper";
import {AuthJwtDataService} from "../../Auth/AuthJwtDataService";

@Component({
  selector: 'app-kupac-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterOutlet, KupacSidebarComponent, KupacNavbarComponent, NavbarComponent, FooterComponent],
  templateUrl: './kupac-dashboard.component.html',
  styleUrl: './kupac-dashboard.component.css'
})
export class KupacDashboardComponent {
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
