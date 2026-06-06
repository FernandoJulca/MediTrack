import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  formulario: FormGroup;
  cargando = false;
  error = '';
  mostrarContrasena = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService
  ) {
    this.formulario = this.fb.group({
      correo: ['', [Validators.required, Validators.email]],
      contrasena: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  get correo() { return this.formulario.get('correo'); }
  get contrasena() { return this.formulario.get('contrasena'); }

  iniciarSesion(): void {
    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }

    this.cargando = true;
    this.error = '';

    this.authService.login(this.formulario.value).subscribe({
      next: () => { this.cargando = false; },
      error: (err) => {
        this.cargando = false;
        this.error = err.error?.mensaje ?? 'Credenciales incorrectas.';
      }
    });
  }
}