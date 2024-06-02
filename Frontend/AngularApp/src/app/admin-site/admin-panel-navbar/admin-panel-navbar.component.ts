import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MyAuthService} from "../../helper/auth/MyAuthService";
import {Router} from "@angular/router";
import {AuthJwtHelper} from "../../Auth/AuthJwtHelper";
import {AuthJwtDataService} from "../../Auth/AuthJwtDataService";

@Component({
  selector: 'app-admin-panel-navbar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin-panel-navbar.component.html',
  styleUrl: './admin-panel-navbar.component.css'
})
export class AdminPanelNavbarComponent implements OnInit{
  // @Output() toggleSidebarEvent = new EventEmitter<void>();
  constructor(private helper:AuthJwtHelper, private router:Router, private auth:AuthJwtDataService) {
  }
  logOut() {
    //this.myAuthService.signOut();
    this.auth.revokeToken().subscribe({});
    this.router.navigate(["/login"])
  }
  public imePrezime="";
  ngOnInit(): void {
    this.ucitajIme();
  }

  private ucitajIme() {

    this.imePrezime=this.helper.getIme();
  }

}
