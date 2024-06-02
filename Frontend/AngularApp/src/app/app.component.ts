import {Component, HostListener, Inject, Injectable, OnInit} from '@angular/core';
import {CommonModule, IMAGE_CONFIG} from '@angular/common';
import {Router, RouterLink, RouterOutlet} from '@angular/router';
import {BrendoviComponent} from "./admin-site/brendovi/brendovi.component";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {GlavneKategorijeComponent} from "./admin-site/glavne-kategorije/glavne-kategorije.component";
import {KategorijeComponent} from "./admin-site/kategorije/kategorije.component";
import {PotkategorijeComponent} from "./admin-site/potkategorije/potkategorije.component";
import {ProizvodiComponent} from "./admin-site/proizvodi/proizvodi.component";
import {AdminPageComponent} from "./admin-site/admin-page/admin-page.component";
import {HomepageComponent} from "./homepage/homepage.component";
import {ProizvodPageComponent} from "./proizvod-page/proizvod-page.component";
import {PopustiPageComponent} from "./popusti-page/popusti-page.component";
import {ProizvodiNoviPageComponent} from "./proizvodi-novi-page/proizvodi-novi-page.component";
import {KontaktPageComponent} from "./kontakt-page/kontakt-page.component";
import {PretragaProizvodaComponent} from "./pretraga-proizvoda/pretraga-proizvoda.component";
import {LoginFormaComponent} from "./login-forma/login-forma.component";
import {NavbarComponent} from "./navbar/navbar.component";
import {AuthLoginEndpoint} from "./Endpoints/auth endpoints/auth-login-endpoint";
import {AuthLogoutEndpoint} from "./Endpoints/auth endpoints/auth-logout-endpoint";
import {DashboardComponent} from "./admin-site/dashboard/dashboard.component";
import {PermissionsService} from "./guards/auth.guard";
import {ActivateRegistrationComponent} from "./activate-registration/activate-registration.component";
import {KupacDashboardComponent} from "./Kupac/kupac-dashboard/kupac-dashboard.component";
import {KupacPostavkeComponent} from "./Kupac/kupac-postavke/kupac-postavke.component";
import {KupacNavbarComponent} from "./Kupac/kupac-navbar/kupac-navbar.component";
import {KupacSidebarComponent} from "./Kupac/kupac-sidebar/kupac-sidebar.component";
import {KupacPanelComponent} from "./Kupac/kupac-panel/kupac-panel.component";
import {ResetLozinkeComponent} from "./Kupac/reset-lozinke/reset-lozinke.component";
import {SpinnerService} from "./service/spinner-service";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule,
  RouterOutlet,
  RouterLink,
  BrendoviComponent,
  HttpClientModule,
  GlavneKategorijeComponent,
  KategorijeComponent,
  PotkategorijeComponent,
  ProizvodiComponent,
  AdminPageComponent,
  HomepageComponent,
  PretragaProizvodaComponent,
  ProizvodPageComponent,
  PopustiPageComponent,
  ProizvodiNoviPageComponent,
  KontaktPageComponent,
    LoginFormaComponent,
    NavbarComponent,
     DashboardComponent,
    ActivateRegistrationComponent,
    KupacDashboardComponent,
    KupacPostavkeComponent,
    KupacNavbarComponent,
    KupacSidebarComponent,
    KupacPanelComponent,
    ResetLozinkeComponent,
  ],
  providers:[
    AuthLoginEndpoint,
    AuthLogoutEndpoint,
  PermissionsService,
   {
      provide: IMAGE_CONFIG,
      useValue: {
        disableImageSizeWarning: true,
        disableImageLazyLoadWarning: true
      }
    },
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  showSpinner = false;
  constructor(public spinnerService:SpinnerService) {
  }
  title = 'Webshop';

  ngOnInit(): void {
    this.spinnerService.spinnerCounter$.subscribe(counter => {
      this.showSpinner = counter > 0;
    });
  }
}

