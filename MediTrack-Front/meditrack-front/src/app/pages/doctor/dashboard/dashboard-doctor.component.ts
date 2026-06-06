import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { CitasService } from '../../../core/services/citas.service';
import { AuthService } from '../../../core/services/auth.service';
import { Cita } from '../../../core/models/cita.model';

@Component({
  selector: 'app-dashboard-doctor',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard-doctor.component.html',
  styleUrl: './dashboard-doctor.component.scss'
})
export class DashboardDoctorComponent implements OnInit {
  citas: Cita[] = [];
  cargando = true;

  constructor(
    public authService: AuthService,
    private citasService: CitasService
  ) {}

  ngOnInit(): void {
    const id = this.obtenerIdDoctor();
    if (!id) return;
    this.citasService.obtenerPorDoctor(id).subscribe({
      next: data => { this.citas = data; this.cargando = false; },
      error: () => this.cargando = false
    });
  }

  get citasHoy(): Cita[] {
    const hoy = new Date().toDateString();
    return this.citas.filter(c =>
      new Date(c.fechaHora).toDateString() === hoy
    );
  }

  get citasPendientesHoy(): Cita[] {
    return this.citasHoy.filter(c =>
      ['Agendada', 'Confirmada', 'Llego'].includes(c.estado)
    );
  }

  get citasEnAtencion(): Cita[] {
    return this.citas.filter(c => c.estado === 'EnAtencion');
  }

  get totalCompletadas(): number {
    return this.citas.filter(c => c.estado === 'Completada').length;
  }

  get proximaCita(): Cita | null {
    return this.citasPendientesHoy[0] ?? null;
  }

  obtenerIdDoctor(): number | null {
    const sesion = this.authService.sesionActual;
    if (!sesion) return null;
    try {
      const payload = JSON.parse(atob(sesion.token.split('.')[1]));
      return parseInt(payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']);
    } catch { return null; }
  }

  colorEstado(estado: string): string {
    const colores: { [key: string]: string } = {
      'Agendada': 'badge-agendada', 'Confirmada': 'badge-confirmada',
      'Llego': 'badge-llego', 'EnAtencion': 'badge-atencion',
      'Completada': 'badge-completada', 'NoSePresento': 'badge-nopresento',
      'Cancelada': 'badge-cancelada'
    };
    return colores[estado] ?? '';
  }
}