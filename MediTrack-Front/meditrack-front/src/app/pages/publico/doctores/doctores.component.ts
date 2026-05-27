import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { DoctoresService, Doctor } from '../../../core/services/doctores.service';
import { EspecialidadesService, Especialidad } from '../../../core/services/especialidades.service';

@Component({
  selector: 'app-doctores',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './doctores.component.html',
  styleUrl: './doctores.component.scss'
})
export class DoctoresComponent implements OnInit {
  doctores: Doctor[] = [];
  doctoresFiltrados: Doctor[] = [];
  especialidades: Especialidad[] = [];
  especialidadSeleccionada: number = 0;
  busqueda: string = '';

  constructor(
    private doctoresService: DoctoresService,
    private especialidadesService: EspecialidadesService
  ) {}

  ngOnInit(): void {
    this.doctoresService.obtenerTodos().subscribe(data => {
      this.doctores = data;
      this.doctoresFiltrados = data;
    });
    this.especialidadesService.obtenerTodas().subscribe(data => {
      this.especialidades = data;
    });
  }

  filtrar(): void {
    this.doctoresFiltrados = this.doctores.filter(d => {
      const coincideEsp = this.especialidadSeleccionada === 0 ||
        d.especialidadId === this.especialidadSeleccionada;
      const coincideBusqueda = d.nombreCompleto.toLowerCase()
        .includes(this.busqueda.toLowerCase());
      return coincideEsp && coincideBusqueda;
    });
  }

  limpiarFiltros(): void {
    this.especialidadSeleccionada = 0;
    this.busqueda = '';
    this.doctoresFiltrados = this.doctores;
  }
}