export interface Cita {
  id: number;
  fechaHora: string;
  motivo: string;
  observaciones?: string;
  estado: string;
  pacienteId: number;
  nombrePaciente: string;
  doctorId: number;
  nombreDoctor: string;
}

export interface CrearCita {
  fechaHora: string;
  motivo: string;
  pacienteId: number;
  doctorId: number;
}