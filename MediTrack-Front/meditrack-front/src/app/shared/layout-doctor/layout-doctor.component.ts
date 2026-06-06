import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarPrivadoComponent } from '../navbar-privado/navbar-privado.component';
import { SidebarDoctorComponent } from '../sidebar-doctor/sidebar-doctor.component';

@Component({
  selector: 'app-layout-doctor',
  standalone: true,
  imports: [RouterOutlet, NavbarPrivadoComponent, SidebarDoctorComponent],
  template: `
    <div class="layout-privado">
      <app-navbar-privado></app-navbar-privado>
      <div class="layout-body d-flex">
        <app-sidebar-doctor></app-sidebar-doctor>
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
export class LayoutDoctorComponent {}