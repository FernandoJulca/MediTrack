import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CitasService } from '../../../core/services/citas.service';
import { SedesService, Sede } from '../../../core/services/sedes.service';
import { Cita } from '../../../core/models/cita.model';

@Component({
  selector: 'app-citas-recepcion',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './citas-recepcion.component.html',
  styleUrl: './citas-recepcion.component.scss'
})
export class CitasRecepcionComponent implements OnInit {
  citas: Cita[] = [];
  citasFiltradas: Cita[] = [];
  sedes: Sede[] = [];
  sedeSeleccionada = 1;
  fecha = new Date().toISOString().split('T')[0];
  filtroEstado = '';
  cargando = true;
  cambiando: number | null = null;
  citaDetalle: Cita | null = null;
  mensajeExito = '';

  estadosFiltro = [
    { valor: '', etiqueta: 'Todos' },
    { valor: 'Agendada', etiqueta: 'Agendada' },
    { valor: 'Confirmada', etiqueta: 'Confirmada' },
    { valor: 'Llego', etiqueta: 'Llegó' },
    { valor: 'EnAtencion', etiqueta: 'En Atención' },
    { valor: 'Completada', etiqueta: 'Completada' },
    { valor: 'NoSePresento', etiqueta: 'No se presentó' },
    { valor: 'Cancelada', etiqueta: 'Cancelada' },
  ];

  acciones = [
    { valor: 2, etiqueta: 'Confirmar', icono: 'bi-check-circle', color: 'success' },
    { valor: 3, etiqueta: 'Llegó', icono: 'bi-person-check', color: 'primary' },
    { valor: 6, etiqueta: 'No se presentó', icono: 'bi-person-x', color: 'warning' },
    { valor: 7, etiqueta: 'Cancelar', icono: 'bi-x-circle', color: 'danger' },
  ];

  constructor(
    private citasService: CitasService,
    private sedesService: SedesService
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
      next: data => {
        this.citas = data;
        this.filtrar();
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

  cambiarEstado(citaId: number, estado: number): void {
    this.cambiando = citaId;
    this.citasService.cambiarEstado(citaId, { estado }).subscribe({
      next: () => {
        this.cambiando = null;
        if (this.citaDetalle?.id === citaId) this.citaDetalle = null;
        this.mensajeExito = 'Estado actualizado.';
        this.cargarCitas();
        setTimeout(() => this.mensajeExito = '', 3000);
      },
      error: () => this.cambiando = null
    });
  }

  verDetalle(cita: Cita): void { this.citaDetalle = cita; }
  cerrarDetalle(): void { this.citaDetalle = null; }

  puedeActuar(estado: string): boolean {
    return !['Completada', 'Cancelada'].includes(estado);
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