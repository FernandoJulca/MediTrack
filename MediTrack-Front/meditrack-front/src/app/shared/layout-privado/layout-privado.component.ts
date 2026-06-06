import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { SidebarPacienteComponent } from '../sidebar-paciente/sidebar-paciente.component';
import { NavbarPrivadoComponent } from '../navbar-privado/navbar-privado.component';

@Component({
  selector: 'app-layout-privado',
  standalone: true,
  imports: [CommonModule, RouterOutlet, SidebarPacienteComponent, NavbarPrivadoComponent],
  template: `
    <div class="layout-privado">
      <app-navbar-privado></app-navbar-privado>
      <div class="layout-body d-flex">
        <app-sidebar-paciente></app-sidebar-paciente>
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
export class LayoutPrivadoComponent {}