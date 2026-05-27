import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environments';

export interface Sede {
  id: number;
  nombre: string;
  direccion: string;
  telefono: string;
  ciudad: string;
  descripcion?: string;
  urlFoto?: string;
}

@Injectable({ providedIn: 'root' })
export class SedesService {
  private readonly URL = `${environment.apiUrl}/Sedes`;
  constructor(private http: HttpClient) {}
  obtenerTodas(): Observable<Sede[]> {
    return this.http.get<Sede[]>(this.URL);
  }
  obtenerPorId(id: number): Observable<Sede> {
    return this.http.get<Sede>(`${this.URL}/${id}`);
  }
}