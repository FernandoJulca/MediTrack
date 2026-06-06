import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarPrivadoComponent } from '../navbar-privado/navbar-privado.component';
import { SidebarRecepcionComponent } from '../sidebar-recepcion/sidebar-recepcion.component';

@Component({
  selector: 'app-layout-recepcion',
  standalone: true,
  imports: [RouterOutlet, NavbarPrivadoComponent, SidebarRecepcionComponent],
  template: `
    <div class="layout-privado">
      <app-navbar-privado></app-navbar-privado>
      <div class="layout-body d-flex">
        <app-sidebar-recepcion></app-sidebar-recepcion>
        <main class="layout-main flex-grow-1">
          <router-outlet></router-outlet>
        </main>
      </div>
    </div>
  `,
  styles: [`
    .layout-privado { min-height: 100vh; background: #f8fafb; }
    .layout-body { min-height: calc(100vh - 60px); }
    .layout-main { overflow-x: hidden; }
  `]
})
export class LayoutRecepcionComponent {}