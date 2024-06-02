import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {Router, RouterLink, RouterOutlet} from "@angular/router";
import {MyAuthService} from "../../helper/auth/MyAuthService";
// import {AutorizacijaGuard} from "../../helper/auth/autorizacija-guard.service";

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterOutlet],
  //providers:[MyAuthService],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  // constructor(
  //   private myAuthService:MyAuthService,
  //   private router: Router,
  // ) {
  // }
}
