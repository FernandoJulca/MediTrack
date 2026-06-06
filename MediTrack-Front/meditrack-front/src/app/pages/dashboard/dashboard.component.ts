import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  template: ''
})
export class DashboardComponent implements OnInit {
  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    const rol = this.authService.rol;
    switch (rol) {
      case 'Paciente':
        this.router.navigate(['/paciente/dashboard']); break;
      case 'Doctor':
        this.router.navigate(['/doctor/dashboard']); break;
      case 'Recepcionista':
        this.router.navigate(['/recepcion/dashboard']); break;
      case 'Administrador':
        this.router.navigate(['/admin/dashboard']); break;
      default:
        this.router.navigate(['/login']);
    }
  }
}