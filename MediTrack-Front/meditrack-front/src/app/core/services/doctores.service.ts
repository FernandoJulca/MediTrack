import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environments';
//import { DtoDoctor } from '../models/medicamento.model';

export interface Doctor {
  id: number;
  nombreCompleto: string;
  correo: string;
  telefono: string;
  biografia?: string;
  urlFoto?: string;
  especialidadId: number;
  nombreEspecialidad: string;
  horarios: Horario[];
}

export interface Horario {
  diaSemana: number;
  nombreDia: string;
  horaInicio: string;
  horaFin: string;
  duracionCitaMinutos: number;
  sedeId: number;
  nombreSede: string;
}

@Injectable({ providedIn: 'root' })
export class DoctoresService {
  private readonly URL = `${environment.apiUrl}/Doctores`;
  constructor(private http: HttpClient) {}
  obtenerTodos(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(this.URL);
  }
  obtenerPorId(id: number): Observable<Doctor> {
    return this.http.get<Doctor>(`${this.URL}/${id}`);
  }
  obtenerPorEspecialidad(especialidadId: number): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(`${this.URL}/especialidad/${especialidadId}`);
  }
  obtenerPorSede(sedeId: number): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(`${this.URL}/sede/${sedeId}`);
  }
}