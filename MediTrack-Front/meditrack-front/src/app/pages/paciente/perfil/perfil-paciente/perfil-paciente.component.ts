import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-perfil-paciente',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './perfil-paciente.component.html',
  styleUrl: './perfil-paciente.component.scss'
})
export class PerfilPacienteComponent {
  constructor(public authService: AuthService) {}

  cerrarSesion(): void {
    this.authService.logout();
  }
}