import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-sidebar-recepcion',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './sidebar-recepcion.component.html',
  styleUrl: './sidebar-recepcion.component.scss'
})
export class SidebarRecepcionComponent {
  menu = [
    { label: 'Dashboard', icono: 'bi-house', ruta: '/recepcion/dashboard' },
    { label: 'Citas del día', icono: 'bi-calendar-day', ruta: '/recepcion/citas' },
    { label: 'Pacientes', icono: 'bi-people', ruta: '/recepcion/pacientes' },
  ];
}