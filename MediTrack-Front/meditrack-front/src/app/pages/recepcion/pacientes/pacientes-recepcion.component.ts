import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CitasService } from '../../../core/services/citas.service';
import { Cita } from '../../../core/models/cita.model';

@Component({
  selector: 'app-pacientes-recepcion',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pacientes-recepcion.component.html',
  styleUrl: './pacientes-recepcion.component.scss'
})
export class PacientesRecepcionComponent implements OnInit {
  citas: Cita[] = [];
  busqueda = '';
  cargando = true;

  constructor(private citasService: CitasService) {}

  ngOnInit(): void {
    this.citasService.obtenerTodas().subscribe({
      next: data => { this.citas = data; this.cargando = false; },
      error: () => this.cargando = false
    });
  }

  get pacientes(): { id: number; nombre: string; totalCitas: number; ultimaCita: Cita }[] {
    const mapa = new Map<number, { id: number; nombre: string; citas: Cita[] }>();
    this.citas.forEach(c => {
      if (!mapa.has(c.pacienteId)) {
        mapa.set(c.pacienteId, {
          id: c.pacienteId, nombre: c.nombrePaciente, citas: []
        });
      }
      mapa.get(c.pacienteId)!.citas.push(c);
    });

    return Array.from(mapa.values())
      .filter(p => p.nombre.toLowerCase().includes(this.busqueda.toLowerCase()))
      .map(p => ({
        id: p.id,
        nombre: p.nombre,
        totalCitas: p.citas.length,
        ultimaCita: p.citas.sort((a, b) =>
          new Date(b.fechaHora).getTime() - new Date(a.fechaHora).getTime())[0]
      }));
  }
}