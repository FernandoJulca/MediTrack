import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-registro',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './registro.component.html',
  styleUrl: './registro.component.scss'
})
export class RegistroComponent {
  formulario: FormGroup;
  cargando = false;
  error = '';
  mostrarContrasena = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    if (this.authService.estaAutenticado()) {
      this.router.navigate(['/dashboard']);
    }

    this.formulario = this.fb.group({
      nombres: ['', [Validators.required, Validators.minLength(2)]],
      apellidos: ['', [Validators.required, Validators.minLength(2)]],
      correo: ['', [Validators.required, Validators.email]],
      contrasena: ['', [Validators.required, Validators.minLength(6)]],
      telefono: ['', [Validators.required, Validators.minLength(9)]],
      dni: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(8)]]
    });
  }

  get f() { return this.formulario.controls; }

  registrar(): void {
    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }

    this.cargando = true;
    this.error = '';

    const datos = { ...this.formulario.value, rol: 4 };

    this.authService.registrar(datos).subscribe({
      next: () => {
        this.cargando = false;
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        this.cargando = false;
        this.error = err.error?.mensaje ?? 'Error al registrarse. Intenta nuevamente.';
      }
    });
  }
}