import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MyAuthService} from "../../helper/auth/MyAuthService";
import {Router} from "@angular/router";
import {AuthJwtHelper} from "../../Auth/AuthJwtHelper";
import {AuthJwtDataService} from "../../Auth/AuthJwtDataService";

@Component({
  selector: 'app-kupac-navbar',
  standalone: true,
  imports: [CommonModule],
  providers:[MyAuthService],
  templateUrl: './kupac-navbar.component.html',
  styleUrl: './kupac-navbar.component.css'
})
export class KupacNavbarComponent {
  constructor(private helper:AuthJwtHelper,
              private router:Router,
              private jwt:AuthJwtDataService) {
  }
  logOut() {
    //this.myAuthService.signOut();
    this.router.navigate([""])

    this.jwt.revokeToken().subscribe(x=>{
    });
    this.helper.signOut();

    // setTimeout(()=>{
    //
    // }, 1000);

  }

  getIme(){
    return this.helper.getIme();
  }

}
