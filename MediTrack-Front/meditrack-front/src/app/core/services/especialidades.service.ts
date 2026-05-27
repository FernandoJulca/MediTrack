import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environments';

export interface Especialidad {
  id: number;
  nombre: string;
  descripcion: string;
  icono?: string;
}

@Injectable({ providedIn: 'root' })
export class EspecialidadesService {
  private readonly URL = `${environment.apiUrl}/Especialidades`;
  constructor(private http: HttpClient) {}
  obtenerTodas(): Observable<Especialidad[]> {
    return this.http.get<Especialidad[]>(this.URL);
  }
}