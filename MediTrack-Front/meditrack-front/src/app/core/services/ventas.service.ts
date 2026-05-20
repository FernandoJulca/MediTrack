import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environments';
import { CrearVenta, Venta } from '../models/venta.model';

@Injectable({ providedIn: 'root' })
export class VentasService {
  private readonly URL = `${environment.apiUrl}/Ventas`;

  constructor(private http: HttpClient) {}

  obtenerTodas(): Observable<Venta[]> {
    return this.http.get<Venta[]>(this.URL);
  }

  obtenerPorId(id: number): Observable<Venta> {
    return this.http.get<Venta>(`${this.URL}/${id}`);
  }

  crear(datos: CrearVenta): Observable<Venta> {
    return this.http.post<Venta>(this.URL, datos);
  }
}