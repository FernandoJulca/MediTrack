export interface Venta {
  id: number;
  numeroComprobante: string;
  tipoComprobante: string;
  total: number;
  fechaVenta: string;
  pacienteId: number;
  nombrePaciente: string;
  detalles: DetalleVenta[];
}

export interface DetalleVenta {
  medicamentoId: number;
  nombreMedicamento: string;
  cantidad: number;
  precioUnitario: number;
  subtotal: number;
}

export interface CrearVenta {
  pacienteId: number;
  tipoComprobante: string;
  detalles: { medicamentoId: number; cantidad: number }[];
}