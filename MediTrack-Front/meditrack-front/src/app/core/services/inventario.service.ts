import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environments';
import { CrearMedicamento, Medicamento } from '../models/medicamento.model';

@Injectable({ providedIn: 'root' })
export class InventarioService {
  private readonly URL = `${environment.apiUrl}/Inventario`;

  constructor(private http: HttpClient) {}

  obtenerTodos(): Observable<Medicamento[]> {
    return this.http.get<Medicamento[]>(this.URL);
  }

  obtenerPorId(id: number): Observable<Medicamento> {
    return this.http.get<Medicamento>(`${this.URL}/${id}`);
  }

  crear(datos: CrearMedicamento): Observable<Medicamento> {
    return this.http.post<Medicamento>(this.URL, datos);
  }

  actualizar(id: number, datos: CrearMedicamento): Observable<Medicamento> {
    return this.http.put<Medicamento>(`${this.URL}/${id}`, datos);
  }

  eliminar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.URL}/${id}`);
  }

  obtenerStockBajo(): Observable<Medicamento[]> {
    return this.http.get<Medicamento[]>(`${this.URL}/stock-bajo`);
  }

  ajustarStock(medicamentoId: number, cantidad: number, motivo: string): Observable<Medicamento> {
    return this.http.patch<Medicamento>(`${this.URL}/ajustar-stock`, {
      medicamentoId, cantidad, motivo
    });
  }
}