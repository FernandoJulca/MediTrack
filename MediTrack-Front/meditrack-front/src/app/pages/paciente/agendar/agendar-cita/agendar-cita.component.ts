import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CitasService } from '../../../../core/services/citas.service';
import { AuthService } from '../../../../core/services/auth.service';
import { SedesService, Sede } from '../../../../core/services/sedes.service';
import { EspecialidadesService, Especialidad } from '../../../../core/services/especialidades.service';
import { DoctoresService, Doctor } from '../../../../core/services/doctores.service';

@Component({
  selector: 'app-agendar-cita',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './agendar-cita.component.html',
  styleUrl: './agendar-cita.component.scss'
})
export class AgendarCitaComponent implements OnInit {
  formulario: FormGroup;
  sedes: Sede[] = [];
  especialidades: Especialidad[] = [];
  doctores: Doctor[] = [];
  cargando = false;
  error = '';
  exito = false;
  paso = 1;

  constructor(
    private fb: FormBuilder,
    private citasService: CitasService,
    private authService: AuthService,
    private sedesService: SedesService,
    private especialidadesService: EspecialidadesService,
    private doctoresService: DoctoresService,
    private router: Router
  ) {
    this.formulario = this.fb.group({
      sedeId: ['', Validators.required],
      especialidadId: ['', Validators.required],
      doctorId: ['', Validators.required],
      fechaHora: ['', Validators.required],
      motivo: ['', [Validators.required, Validators.minLength(5)]]
    });
  }

  ngOnInit(): void {
    this.sedesService.obtenerTodas().subscribe(d => this.sedes = d);
    this.especialidadesService.obtenerTodas().subscribe(d => this.especialidades = d);
  }

  onEspecialidadChange(): void {
    const espId = this.formulario.get('especialidadId')?.value;
    this.formulario.patchValue({ doctorId: '' });
    this.doctores = [];
    if (espId) {
      this.doctoresService.obtenerPorEspecialidad(+espId)
        .subscribe(d => this.doctores = d);
    }
  }

  get f() { return this.formulario.controls; }

  get sedeSeleccionada(): Sede | undefined {
    return this.sedes.find(s => s.id === +this.formulario.get('sedeId')?.value);
  }

  get especialidadSeleccionada(): Especialidad | undefined {
    return this.especialidades.find(e =>
      e.id === +this.formulario.get('especialidadId')?.value);
  }

  get doctorSeleccionado(): Doctor | undefined {
    return this.doctores.find(d =>
      d.id === +this.formulario.get('doctorId')?.value);
  }

  siguientePaso(): void { if (this.paso < 3) this.paso++; }
  anteriorPaso(): void { if (this.paso > 1) this.paso--; }

  agendar(): void {
    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }

    this.cargando = true;
    this.error = '';

    const pacienteId = this.obtenerIdPaciente();
    if (!pacienteId) { this.error = 'Error al obtener datos del paciente.'; return; }

    const datos = {
      ...this.formulario.value,
      pacienteId,
      sedeId: +this.formulario.value.sedeId,
      especialidadId: +this.formulario.value.especialidadId,
      doctorId: +this.formulario.value.doctorId,
    };

    this.citasService.crear(datos).subscribe({
      next: () => {
        this.cargando = false;
        this.exito = true;
        setTimeout(() => this.router.navigate(['/paciente/citas']), 2500);
      },
      error: (err) => {
        this.cargando = false;
        this.error = err.error?.mensaje ?? 'Error al agendar la cita.';
      }
    });
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