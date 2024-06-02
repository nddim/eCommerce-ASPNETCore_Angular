import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {NavbarComponent} from "../../navbar/navbar.component";
import {FooterComponent} from "../../footer/footer.component";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-notfound',
  standalone: true,
  imports: [CommonModule, NavbarComponent, FooterComponent, RouterLink],
  templateUrl: './notfound.component.html',
  styleUrl: './notfound.component.css'
})
export class NotfoundComponent {

}
