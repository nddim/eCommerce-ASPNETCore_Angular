import {Component, Input, SimpleChanges} from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterLink, RouterLinkActive, RouterOutlet} from "@angular/router";

@Component({
  selector: 'app-admin-panel-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, RouterOutlet],
  templateUrl: './admin-panel-sidebar.component.html',
  styleUrl: './admin-panel-sidebar.component.css'
})
export class AdminPanelSidebarComponent {
  constructor() {

  }

}
