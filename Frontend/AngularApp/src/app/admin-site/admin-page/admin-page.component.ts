import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {Router, RouterLink, RouterOutlet} from "@angular/router";
import {BrendoviComponent} from "../brendovi/brendovi.component";
import {AdminPanelSidebarComponent} from "../admin-panel-sidebar/admin-panel-sidebar.component";
import {AdminPanelNavbarComponent} from "../admin-panel-navbar/admin-panel-navbar.component";
import {SignalRService} from "../../helper/signal-r/signal-r.service";
import {AuthJwtHelper} from "../../Auth/AuthJwtHelper";
import {AuthJwtDataService} from "../../Auth/AuthJwtDataService";
declare function init_plugin():any;

@Component({
  selector: 'app-admin-page',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterOutlet, BrendoviComponent, AdminPanelSidebarComponent, AdminPanelNavbarComponent],
  templateUrl: './admin-page.component.html',
  styleUrl: './admin-page.component.css'
})
export class AdminPageComponent implements OnInit{
  constructor(private signalRService:SignalRService,
              private helper:AuthJwtHelper, private router:Router, private auth:AuthJwtDataService) {
  }
  ngOnInit(): void {
    init_plugin();
  }

  logOut() {
    this.auth.revokeToken().subscribe({});
    this.router.navigate(["/login"])
  }
}
