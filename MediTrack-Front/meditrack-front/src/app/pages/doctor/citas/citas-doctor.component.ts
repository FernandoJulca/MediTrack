import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CitasService } from '../../../core/services/citas.service';
import { AuthService } from '../../../core/services/auth.service';
import { Cita } from '../../../core/models/cita.model';

@Component({
  selector: 'app-citas-doctor',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './citas-doctor.component.html',
  styleUrl: './citas-doctor.component.scss'
})
export class CitasDoctorComponent implements OnInit {
  citas: Cita[] = [];
  citasFiltradas: Cita[] = [];
  filtroEstado = '';
  cargando = true;
  citaDetalle: Cita | null = null;

  estados = [
    { valor: '', etiqueta: 'Todos' },
    { valor: 'Agendada', etiqueta: 'Agendada' },
    { valor: 'Confirmada', etiqueta: 'Confirmada' },
    { valor: 'Llego', etiqueta: 'Llegó' },
    { valor: 'EnAtencion', etiqueta: 'En Atención' },
    { valor: 'Completada', etiqueta: 'Completada' },
    { valor: 'Cancelada', etiqueta: 'Cancelada' },
  ];

  constructor(
    private citasService: CitasService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    const id = this.obtenerIdDoctor();
    if (!id) return;
    this.citasService.obtenerPorDoctor(id).subscribe({
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

  verDetalle(cita: Cita): void { this.citaDetalle = cita; }
  cerrarDetalle(): void { this.citaDetalle = null; }

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