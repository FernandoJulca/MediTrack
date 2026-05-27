import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { EspecialidadesService, Especialidad } from '../../../core/services/especialidades.service';
import { DoctoresService, Doctor } from '../../../core/services/doctores.service';

@Component({
  selector: 'app-especialidades',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './especialidades.component.html',
  styleUrl: './especialidades.component.scss'
})
export class EspecialidadesComponent implements OnInit {
  especialidades: Especialidad[] = [];
  doctoresPorEspecialidad: { [key: number]: Doctor[] } = {};
  especialidadSeleccionada: number | null = null;

  constructor(
    private especialidadesService: EspecialidadesService,
    private doctoresService: DoctoresService
  ) {}

  ngOnInit(): void {
    this.especialidadesService.obtenerTodas().subscribe(data => {
      this.especialidades = data;
    });
  }

  seleccionar(id: number): void {
    if (this.especialidadSeleccionada === id) {
      this.especialidadSeleccionada = null;
      return;
    }
    this.especialidadSeleccionada = id;
    if (!this.doctoresPorEspecialidad[id]) {
      this.doctoresService.obtenerPorEspecialidad(id).subscribe(data => {
        this.doctoresPorEspecialidad[id] = data;
      });
    }
  }
}