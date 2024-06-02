import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";
import {NavbarComponent} from "../navbar/navbar.component";
import {FooterComponent} from "../footer/footer.component";

@Component({
  selector: 'app-kontakt-page',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, RouterOutlet, NavbarComponent, FooterComponent],
  templateUrl: './kontakt-page.component.html',
  styleUrl: './kontakt-page.component.css'
})
export class KontaktPageComponent {

}
