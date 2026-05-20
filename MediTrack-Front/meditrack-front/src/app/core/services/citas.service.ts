import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environments';
import { Cita, CrearCita } from '../models/cita.model';

@Injectable({ providedIn: 'root' })
export class CitasService {
  private readonly URL = `${environment.apiUrl}/Citas`;

  constructor(private http: HttpClient) {}

  obtenerTodas(): Observable<Cita[]> {
    return this.http.get<Cita[]>(this.URL);
  }

  obtenerPorId(id: number): Observable<Cita> {
    return this.http.get<Cita>(`${this.URL}/${id}`);
  }

  obtenerPorPaciente(pacienteId: number): Observable<Cita[]> {
    return this.http.get<Cita[]>(`${this.URL}/paciente/${pacienteId}`);
  }

  crear(datos: CrearCita): Observable<Cita> {
    return this.http.post<Cita>(this.URL, datos);
  }

  cambiarEstado(id: number, estado: number, observaciones?: string): Observable<Cita> {
    return this.http.patch<Cita>(`${this.URL}/${id}/estado`, { estado, observaciones });
  }

  cancelar(id: number): Observable<void> {
    return this.http.patch<void>(`${this.URL}/${id}/cancelar`, {});
  }
}