import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CitasService } from '../../../../core/services/citas.service';
import { AuthService } from '../../../../core/services/auth.service';
import { Cita } from '../../../../core/models/cita.model';

@Component({
  selector: 'app-citas-paciente',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './citas-paciente.component.html',
  styleUrl: './citas-paciente.component.scss'
})
export class CitasPacienteComponent implements OnInit {
  citas: Cita[] = [];
  citasFiltradas: Cita[] = [];
  cargando = true;
  filtroEstado = '';
  citaSeleccionada: Cita | null = null;
  cancelando = false;
  mensajeExito = '';

  estados = [
    { valor: '', etiqueta: 'Todos los estados' },
    { valor: 'Agendada', etiqueta: 'Agendada' },
    { valor: 'Confirmada', etiqueta: 'Confirmada' },
    { valor: 'Completada', etiqueta: 'Completada' },
    { valor: 'Cancelada', etiqueta: 'Cancelada' },
    { valor: 'NoSePresento', etiqueta: 'No se presentó' },
  ];

  constructor(
    private citasService: CitasService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.cargarCitas();
  }

  cargarCitas(): void {
    const id = this.obtenerIdPaciente();
    if (!id) return;

    this.cargando = true;
    this.citasService.obtenerPorPaciente(id).subscribe({
      next: data => {
        this.citas = data;
        this.citasFiltradas = data;
        this.cargando = false;
      },
      error: () => this.cargando = false
    });
  }

  filtrar(): void {
    this.citasFiltradas = this.filtroEstado
      ? this.citas.filter(c => c.estado === this.filtroEstado)
      : [...this.citas];
  }

  verDetalle(cita: Cita): void {
    this.citaSeleccionada = cita;
  }

  cerrarDetalle(): void {
    this.citaSeleccionada = null;
  }

  cancelarCita(id: number): void {
    if (!confirm('¿Estás seguro de cancelar esta cita?')) return;
    this.cancelando = true;
    this.citasService.cancelar(id).subscribe({
      next: () => {
        this.cancelando = false;
        this.citaSeleccionada = null;
        this.mensajeExito = 'Cita cancelada correctamente.';
        this.cargarCitas();
        setTimeout(() => this.mensajeExito = '', 3000);
      },
      error: () => this.cancelando = false
    });
  }

  puedeCancel(estado: string): boolean {
    return ['Agendada', 'Confirmada'].includes(estado);
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