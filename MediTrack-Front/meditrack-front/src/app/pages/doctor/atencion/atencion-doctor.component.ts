import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CitasService } from '../../../core/services/citas.service';
import { AuthService } from '../../../core/services/auth.service';
import { Cita } from '../../../core/models/cita.model';

@Component({
  selector: 'app-atencion-doctor',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './atencion-doctor.component.html',
  styleUrl: './atencion-doctor.component.scss'
})
export class AtencionDoctorComponent implements OnInit {
  citas: Cita[] = [];
  citaActual: Cita | null = null;
  cargando = true;
  procesando = false;
  mensajeExito = '';
  error = '';
  mostrarFormulario = false;

  formularioInforme: FormGroup;

  constructor(
    private citasService: CitasService,
    public authService: AuthService,
    private fb: FormBuilder
  ) {
    this.formularioInforme = this.fb.group({
      sintomas: ['', [Validators.required, Validators.minLength(5)]],
      diagnostico: ['', [Validators.required, Validators.minLength(5)]],
      tratamiento: ['', [Validators.required, Validators.minLength(5)]],
      observaciones: [''],
      receta: ['']
    });
  }

  ngOnInit(): void {
    this.cargarCitas();
  }

  cargarCitas(): void {
    const id = this.obtenerIdDoctor();
    if (!id) return;
    this.cargando = true;
    this.citasService.obtenerPorDoctor(id).subscribe({
      next: data => {
        const hoy = new Date().toDateString();
        this.citas = data.filter(c =>
          new Date(c.fechaHora).toDateString() === hoy &&
          ['Llego', 'EnAtencion'].includes(c.estado)
        );
        this.citaActual = this.citas.find(c => c.estado === 'EnAtencion') ?? null;
        this.cargando = false;
      },
      error: () => this.cargando = false
    });
  }

  get cola(): Cita[] {
    return this.citas.filter(c => c.estado === 'Llego');
  }

  llamarPaciente(cita: Cita): void {
    this.procesando = true;
    this.error = '';
    this.citasService.cambiarEstado(cita.id, { estado: 4 }).subscribe({
      next: () => {
        this.procesando = false;
        this.citaActual = { ...cita, estado: 'EnAtencion' };
        this.mostrarFormulario = false;
        this.formularioInforme.reset();
        this.cargarCitas();
      },
      error: (err) => {
        this.procesando = false;
        this.error = err.error?.mensaje ?? 'Error al iniciar atención.';
      }
    });
  }

  iniciarFormulario(): void {
    this.mostrarFormulario = true;
  }

  finalizarAtencion(): void {
    if (this.formularioInforme.invalid) {
      this.formularioInforme.markAllAsTouched();
      return;
    }

    if (!this.citaActual) return;

    this.procesando = true;
    this.error = '';

    const informe = {
      citaId: this.citaActual.id,
      ...this.formularioInforme.value
    };

    this.citasService.crearInforme(informe).subscribe({
      next: () => {
        this.procesando = false;
        this.citaActual = null;
        this.mostrarFormulario = false;
        this.formularioInforme.reset();
        this.mensajeExito = 'Atención finalizada correctamente.';
        this.cargarCitas();
        setTimeout(() => this.mensajeExito = '', 4000);
      },
      error: (err) => {
        this.procesando = false;
        this.error = err.error?.mensaje ?? 'Error al finalizar atención.';
      }
    });
  }

  obtenerIdDoctor(): number | null {
    const sesion = this.authService.sesionActual;
    if (!sesion) return null;
    try {
      const payload = JSON.parse(atob(sesion.token.split('.')[1]));
      return parseInt(payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']);
    } catch { return null; }
  }
}