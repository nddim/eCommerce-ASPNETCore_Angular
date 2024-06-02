import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-kupac-sidebar',
  standalone: true,
    imports: [CommonModule, RouterLink],
  templateUrl: './kupac-sidebar.component.html',
  styleUrl: './kupac-sidebar.component.css'
})
export class KupacSidebarComponent {

}
