export interface Medicamento {
  id: number;
  nombre: string;
  descripcion: string;
  laboratorio: string;
  unidadMedida: string;
  stockActual: number;
  stockMinimo: number;
  precioCompra: number;
  precioVenta: number;
  fechaVencimiento: string;
  stockBajo: boolean;
  porVencer: boolean;
}

export interface CrearMedicamento {
  nombre: string;
  descripcion: string;
  laboratorio: string;
  unidadMedida: string;
  stockActual: number;
  stockMinimo: number;
  precioCompra: number;
  precioVenta: number;
  fechaVencimiento: string;
}