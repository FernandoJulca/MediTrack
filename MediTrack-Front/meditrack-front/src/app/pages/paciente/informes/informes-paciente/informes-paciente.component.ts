import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CitasService } from '../../../../core/services/citas.service';
import { AuthService } from '../../../../core/services/auth.service';
import { Cita } from '../../../../core/models/cita.model';

@Component({
  selector: 'app-informes-paciente',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './informes-paciente.component.html',
  styleUrl: './informes-paciente.component.scss'
})
export class InformesPacienteComponent implements OnInit {
  citas: Cita[] = [];
  cargando = true;
  informeSeleccionado: Cita | null = null;

  constructor(
    private citasService: CitasService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    const id = this.obtenerIdPaciente();
    if (!id) return;

    this.citasService.obtenerPorPaciente(id).subscribe({
      next: data => {
        this.citas = data.filter(c => c.informeMedico !== null);
        this.cargando = false;
      },
      error: () => this.cargando = false
    });
  }

  verInforme(cita: Cita): void {
    this.informeSeleccionado = cita;
  }

  cerrarInforme(): void {
    this.informeSeleccionado = null;
  }

  obtenerIdPaciente(): number | null {
    const sesion = this.authService.sesionActual;
    if (!sesion) return null;
    try {
      const payload = JSON.parse(atob(sesion.token.split('.')[1]));
      return parseInt(payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']);
    } catch { return null; }
  }
}