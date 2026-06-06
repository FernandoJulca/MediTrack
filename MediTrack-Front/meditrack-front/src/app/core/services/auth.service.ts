import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environments';

export interface LoginRequest {
  correo: string;
  contrasena: string;
}

export interface AuthResponse {
  token: string;
  correo: string;
  nombreCompleto: string;
  rol: string;
  expiracion: string;
}

export interface UsuarioSesion {
  token: string;
  correo: string;
  nombreCompleto: string;
  rol: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly URL = `${environment.apiUrl}/Autenticacion`;
  private readonly KEY = 'meditrack_sesion';

  private sesionSubject = new BehaviorSubject<UsuarioSesion | null>(
    this.cargarSesion()
  );
  sesion$ = this.sesionSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  login(datos: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.URL}/login`, datos).pipe(
      tap(res => {
        const sesion: UsuarioSesion = {
          token: res.token,
          correo: res.correo,
          nombreCompleto: res.nombreCompleto,
          rol: res.rol
        };
        localStorage.setItem(this.KEY, JSON.stringify(sesion));
        this.sesionSubject.next(sesion);
        this.redirigirPorRol(res.rol);
      })
    );
  }

  registrar(datos: any): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.URL}/registrar`, datos).pipe(
      tap(res => {
        const sesion: UsuarioSesion = {
          token: res.token,
          correo: res.correo,
          nombreCompleto: res.nombreCompleto,
          rol: res.rol
        };
        localStorage.setItem(this.KEY, JSON.stringify(sesion));
        this.sesionSubject.next(sesion);
      })
    );
  }

  logout(): void {
    localStorage.removeItem(this.KEY);
    this.sesionSubject.next(null);
    this.router.navigate(['/login']);
  }

  get sesionActual(): UsuarioSesion | null {
    return this.sesionSubject.value;
  }

  get token(): string | null {
    return this.sesionActual?.token ?? null;
  }

  get rol(): string | null {
    return this.sesionActual?.rol ?? null;
  }

  estaAutenticado(): boolean {
    return !!this.token;
  }

  tieneRol(...roles: string[]): boolean {
    return roles.includes(this.rol ?? '');
  }

  private redirigirPorRol(rol: string): void {
    switch (rol) {
      case 'Paciente':
        this.router.navigate(['/paciente/dashboard']); break;
      case 'Doctor':
        this.router.navigate(['/doctor/dashboard']); break;
      case 'Recepcionista':
        this.router.navigate(['/recepcion/dashboard']); break;
      case 'Administrador':
        this.router.navigate(['/recepcion/dashboard']); break;
      default:
        this.router.navigate(['/login']);
    }
  }

  private cargarSesion(): UsuarioSesion | null {
    const datos = localStorage.getItem(this.KEY);
    return datos ? JSON.parse(datos) : null;
  }
}