import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-sidebar-paciente',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './sidebar-paciente.component.html',
  styleUrl: './sidebar-paciente.component.scss'
})
export class SidebarPacienteComponent {
  menu = [
    { label: 'Inicio', icono: 'bi-house', ruta: '/paciente/dashboard' },
    { label: 'Mis Citas', icono: 'bi-calendar-check', ruta: '/paciente/citas' },
    { label: 'Agendar Cita', icono: 'bi-calendar-plus', ruta: '/paciente/agendar' },
    { label: 'Mis Informes', icono: 'bi-file-medical', ruta: '/paciente/informes' },
    { label: 'Mi Perfil', icono: 'bi-person', ruta: '/paciente/perfil' },
  ];
}