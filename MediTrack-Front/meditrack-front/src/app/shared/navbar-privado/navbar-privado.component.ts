import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-navbar-privado',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './navbar-privado.component.html',
  styleUrl: './navbar-privado.component.scss'
})
export class NavbarPrivadoComponent {
  menuAbierto = false;

  constructor(public authService: AuthService, private router: Router) {}

  cerrarSesion(): void {
    this.authService.logout();
  }
}