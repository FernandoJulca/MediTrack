import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environments';
import { AuthResponse, LoginRequest, UsuarioSesion } from '../models/usuario.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly URL = `${environment.apiUrl}/Autenticacion`;
  private readonly KEY = 'meditrack_sesion';

  private sesionSubject = new BehaviorSubject<UsuarioSesion | null>(this.cargarSesion());
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

  private cargarSesion(): UsuarioSesion | null {
    const datos = localStorage.getItem(this.KEY);
    return datos ? JSON.parse(datos) : null;
  }
}