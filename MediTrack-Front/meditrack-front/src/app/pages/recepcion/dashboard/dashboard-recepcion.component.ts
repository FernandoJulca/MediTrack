import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CitasService } from '../../../core/services/citas.service';
import { SedesService, Sede } from '../../../core/services/sedes.service';
import { AuthService } from '../../../core/services/auth.service';
import { Cita } from '../../../core/models/cita.model';

@Component({
  selector: 'app-dashboard-recepcion',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './dashboard-recepcion.component.html',
  styleUrl: './dashboard-recepcion.component.scss'
})
export class DashboardRecepcionComponent implements OnInit {
  citas: Cita[] = [];
  sedes: Sede[] = [];
  sedeSeleccionada = 1;
  fecha = new Date().toISOString().split('T')[0];
  cargando = true;
  cambiando: number | null = null;
  mensajeExito = '';

  estados = [
    { valor: 2, etiqueta: 'Confirmada', icono: 'bi-check-circle', color: 'success' },
    { valor: 3, etiqueta: 'Llegó', icono: 'bi-person-check', color: 'primary' },
    { valor: 6, etiqueta: 'No se presentó', icono: 'bi-person-x', color: 'danger' },
    { valor: 7, etiqueta: 'Cancelada', icono: 'bi-x-circle', color: 'secondary' },
  ];

  constructor(
    private citasService: CitasService,
    private sedesService: SedesService,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    this.sedesService.obtenerTodas().subscribe(data => {
      this.sedes = data;
      if (data.length > 0) {
        this.sedeSeleccionada = data[0].id;
        this.cargarCitas();
      }
    });
  }

  cargarCitas(): void {
    this.cargando = true;
    this.citasService.obtenerPorFechaYSede(
      new Date(this.fecha), this.sedeSeleccionada
    ).subscribe({
      next: data => { this.citas = data; this.cargando = false; },
      error: () => this.cargando = false
    });
  }

  cambiarEstado(citaId: number, estado: number): void {
    this.cambiando = citaId;
    this.citasService.cambiarEstado(citaId, { estado }).subscribe({
      next: () => {
        this.cambiando = null;
        this.mensajeExito = 'Estado actualizado correctamente.';
        this.cargarCitas();
        setTimeout(() => this.mensajeExito = '', 3000);
      },
      error: () => this.cambiando = null
    });
  }

  get citasAgendadas(): number {
    return this.citas.filter(c => c.estado === 'Agendada').length;
  }
  get citasConfirmadas(): number {
    return this.citas.filter(c => c.estado === 'Confirmada').length;
  }
  get citasLlegaron(): number {
    return this.citas.filter(c => c.estado === 'Llego').length;
  }
  get citasCompletadas(): number {
    return this.citas.filter(c => c.estado === 'Completada').length;
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
    return colores[estado] ?? '';
  }
}