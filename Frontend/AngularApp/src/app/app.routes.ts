import {ActivatedRoute, Routes} from '@angular/router';
import {BrendoviComponent} from "./admin-site/brendovi/brendovi.component";
import {KategorijeComponent} from "./admin-site/kategorije/kategorije.component";
import {PotkategorijeComponent} from "./admin-site/potkategorije/potkategorije.component";
import {GlavneKategorijeComponent} from "./admin-site/glavne-kategorije/glavne-kategorije.component";
import {ProizvodiComponent} from "./admin-site/proizvodi/proizvodi.component";
import {AdminPageComponent} from "./admin-site/admin-page/admin-page.component";
import {DashboardComponent} from "./admin-site/dashboard/dashboard.component";
import {HomepageComponent} from "./homepage/homepage.component";
import {ProizvodPageComponent} from "./proizvod-page/proizvod-page.component";
import {PopustiPageComponent} from "./popusti-page/popusti-page.component";
import {ProizvodiNoviPageComponent} from "./proizvodi-novi-page/proizvodi-novi-page.component";
import {KontaktPageComponent} from "./kontakt-page/kontakt-page.component";
import {PretragaProizvodaComponent} from "./pretraga-proizvoda/pretraga-proizvoda.component";
import {LoginFormaComponent} from "./login-forma/login-forma.component";
import {authGuardAdmin, authGuardKupac, authKorpa} from "./guards/auth.guard";
import {RegisterFormaComponent} from "./register-forma/register-forma.component";
import {ActivateRegistrationComponent} from "./activate-registration/activate-registration.component";
import {KupacDashboardComponent} from "./Kupac/kupac-dashboard/kupac-dashboard.component";
import {KupacPostavkeComponent} from "./Kupac/kupac-postavke/kupac-postavke.component";
import {ResetLozinkeComponent} from "./Kupac/reset-lozinke/reset-lozinke.component";
import {KorpaComponent} from "./Kupac/korpa/korpa.component";
import {ProizvodPretragaNazivComponent} from "./proizvod-pretraga-naziv/proizvod-pretraga-naziv.component";
import {KupacCheckoutComponent} from "./Kupac/kupac-checkout/kupac-checkout.component";
import {KorisniciComponent} from "./admin-site/korisnici/korisnici.component";
import {KupacNarudzbeComponent} from "./Kupac/kupac-narudzbe/kupac-narudzbe.component";
import {AdministratorNarudzbeDataService} from "./admin-site/narudzbe/narudzbe-dataservice";
import {NarudzbeComponent} from "./admin-site/narudzbe/narudzbe.component";
import {AuthJwtGuardAdmin, AuthJwtGuardKupac} from "./Auth/Guards/AuthGuard";
import {KategorijeHijerarhijaComponent} from "./kategorije-hijerarhija/kategorije-hijerarhija.component";
import {VijestComponent} from "./vijest/vijest.component";
import {VijestPageComponent} from "./vijest-page/vijest-page.component";
import {AdminVijestComponent} from "./admin-site/admin-vijest/admin-vijest.component";
import {KupacNarudzbaDetailsComponent} from "./Kupac/kupac-narudzba-details/kupac-narudzba-details.component";
import {KupacPanelComponent} from "./Kupac/kupac-panel/kupac-panel.component";
import {NotfoundComponent} from "./shared/notfound/notfound.component";


export const routes: Routes = [
 {path:'admin-panel', component:AdminPageComponent, canActivate:[AuthJwtGuardAdmin],  children:[
      {path:'brendovi', component:BrendoviComponent},
      {path:'kategorije', component:KategorijeComponent},
      {path:'potkategorije', component:PotkategorijeComponent },
      {path:'glavne-kategorije', component:GlavneKategorijeComponent  },
      {path:'proizvodi', component:ProizvodiComponent,  },
      {path:'dashboard', component:DashboardComponent, },
      {path:'korisnici', component:KorisniciComponent, },
      {path: 'narudzbe', component: NarudzbeComponent},
      {path: 'admin-vijesti', component: AdminVijestComponent,}
    ]},
  {path:'admin-panel', redirectTo:'admin-panel/narudzbe'},
  {path:'kupac-panel', component:KupacDashboardComponent, canActivate:[AuthJwtGuardKupac]},
  {path:'kupac-panel/pocetna', component: KupacPanelComponent, canActivate: [AuthJwtGuardKupac]},
  {path:'kupac-panel/postavke', component:KupacPostavkeComponent, canActivate:[AuthJwtGuardKupac] },
  {path:'kupac-panel/promijeni-lozinku', component:ResetLozinkeComponent, canActivate:[AuthJwtGuardKupac] },
  {path:'kupac-panel/pregled-narudzbi', component:KupacNarudzbeComponent, canActivate:[AuthJwtGuardKupac]},
  {path:'kupac-panel/narudzba/:id', component:KupacNarudzbaDetailsComponent, canActivate:[AuthJwtGuardKupac]},
  {path:'', component:HomepageComponent},
  {path:'proizvod/:id', component:ProizvodPageComponent}, //
  {path:'pretraga-proizvoda/:potk/:sort/:page/:tableSize/:min/:max/:grid/:brendovi', component:PretragaProizvodaComponent},
  { path: 'pretraga-proizvoda/:potk', redirectTo: 'pretraga-proizvoda/:potk/1/1/6/0/0/1/b' },
  {path:'popusti/:sort/:page/:tableSize', component:PopustiPageComponent},
  {path:'popusti', redirectTo:'popusti/1/1/6'},
  {path:'proizvodi-novi', component:ProizvodiNoviPageComponent},
  {path:'kontakt', component:KontaktPageComponent},
  {path:'login', component:LoginFormaComponent},
  {path:'register', component:RegisterFormaComponent},
  {path:'activate/:activationCode', component:ActivateRegistrationComponent},
  {path:'korpa', component:KorpaComponent, canActivate:[AuthJwtGuardKupac]},
  {path:'kategorije', component:KategorijeHijerarhijaComponent},
  {path:'vijesti', component:VijestComponent},
  {path:'vijest/:id', component:VijestPageComponent},
  {path:'checkout', component:KupacCheckoutComponent, canActivate:[authKorpa] },
  {path:'pretraga-naziv/:naziv/:sort/:page/:tableSize', component:ProizvodPretragaNazivComponent},
  {path:'pretraga-naziv/:naziv', redirectTo:'pretraga-naziv/:naziv/1/1/6'},
  {path:'**', component:NotfoundComponent}
];


