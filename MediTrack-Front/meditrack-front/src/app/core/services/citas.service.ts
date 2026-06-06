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

  obtenerPorDoctor(doctorId: number): Observable<Cita[]> {
    return this.http.get<Cita[]>(`${this.URL}/doctor/${doctorId}`);
  }

  obtenerPorFechaYSede(fecha: Date, sedeId: number): Observable<Cita[]> {
    const fechaStr = fecha.toISOString().split('T')[0];
    return this.http.get<Cita[]>(`${this.URL}/sede/${sedeId}/fecha/${fechaStr}`);
  }

  crear(datos: CrearCita): Observable<Cita> {
    return this.http.post<Cita>(this.URL, datos);
  }

  cambiarEstado(id: number, datos: { estado: number; observaciones?: string }): Observable<Cita> {
    return this.http.patch<Cita>(`${this.URL}/${id}/estado`, datos);
  }

  crearInforme(datos: any): Observable<Cita> {
    return this.http.post<Cita>(`${this.URL}/informe`, datos);
  }

  cancelar(id: number): Observable<void> {
    return this.http.patch<void>(`${this.URL}/${id}/cancelar`, {});
  }
}