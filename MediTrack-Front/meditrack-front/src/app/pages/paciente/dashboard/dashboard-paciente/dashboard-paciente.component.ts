import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';
import { CitasService } from '../../../../core/services/citas.service';
import { Cita } from '../../../../core/models/cita.model';

@Component({
  selector: 'app-dashboard-paciente',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard-paciente.component.html',
  styleUrl: './dashboard-paciente.component.scss'
})
export class DashboardPacienteComponent implements OnInit {
  citas: Cita[] = [];
  cargando = true;

  constructor(
    public authService: AuthService,
    private citasService: CitasService
  ) {}

  ngOnInit(): void {
    const pacienteId = this.obtenerIdPaciente();
    if (pacienteId) {
      this.citasService.obtenerPorPaciente(pacienteId).subscribe({
        next: data => {
          this.citas = data;
          this.cargando = false;
        },
        error: () => this.cargando = false
      });
    }
  }

  get citasProximas(): Cita[] {
    return this.citas.filter(c =>
      ['Agendada', 'Confirmada'].includes(c.estado) &&
      new Date(c.fechaHora) >= new Date()
    ).slice(0, 3);
  }

  get citasPasadas(): Cita[] {
    return this.citas.filter(c => c.estado === 'Completada').length
      ? this.citas.filter(c => c.estado === 'Completada')
      : [];
  }

  get totalCitas(): number { return this.citas.length; }
  get citasCompletadas(): number {
    return this.citas.filter(c => c.estado === 'Completada').length;
  }
  get citasPendientes(): number {
    return this.citas.filter(c => ['Agendada','Confirmada'].includes(c.estado)).length;
  }

  obtenerIdPaciente(): number | null {
    const sesion = this.authService.sesionActual;
    if (!sesion) return null;
    try {
      const payload = JSON.parse(atob(sesion.token.split('.')[1]));
      return parseInt(payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']);
    } catch { return null; }
  }

  colorEstado(estado: string): string {
    const colores: { [key: string]: string } = {
      'Agendada': 'badge-agendada',
      'Confirmada': 'badge-confirmada',
      'Llego': 'badge-llego',
      'EnAtencion': 'badge-atencion',
      'Completada': 'badge-completada',
      'NoSePresento': 'badge-nopresento',
      'Cancelada': 'badge-cancelada'
    };
    return colores[estado] ?? 'badge-secondary';
  }
}