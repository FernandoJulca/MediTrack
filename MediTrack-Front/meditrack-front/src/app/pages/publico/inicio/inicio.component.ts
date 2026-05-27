import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { EspecialidadesService, Especialidad } from '../../../core/services/especialidades.service';
import { SedesService, Sede } from '../../../core/services/sedes.service';
import { DoctoresService, Doctor } from '../../../core/services/doctores.service';

@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './inicio.component.html',
  styleUrl: './inicio.component.scss'
})
export class InicioComponent implements OnInit {
  especialidades: Especialidad[] = [];
  sedes: Sede[] = [];
  doctores: Doctor[] = [];

  constructor(
    private especialidadesService: EspecialidadesService,
    private sedesService: SedesService,
    private doctoresService: DoctoresService
  ) {}

  ngOnInit(): void {
    this.especialidadesService.obtenerTodas().subscribe(data => this.especialidades = data);
    this.sedesService.obtenerTodas().subscribe(data => this.sedes = data);
    this.doctoresService.obtenerTodos().subscribe(data => this.doctores = data);
  }
}