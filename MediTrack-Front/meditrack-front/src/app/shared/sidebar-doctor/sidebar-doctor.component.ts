import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-sidebar-doctor',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './sidebar-doctor.component.html',
  styleUrl: './sidebar-doctor.component.scss'
})
export class SidebarDoctorComponent {
  menu = [
    { label: 'Dashboard', icono: 'bi-house', ruta: '/doctor/dashboard' },
    { label: 'Mis Citas', icono: 'bi-calendar-check', ruta: '/doctor/citas' },
    { label: 'Atención', icono: 'bi-clipboard-pulse', ruta: '/doctor/atencion' },
  ];
}