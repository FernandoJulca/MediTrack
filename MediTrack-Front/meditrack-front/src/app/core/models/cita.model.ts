export interface InformeMedico {
  id: number;
  sintomas: string;
  diagnostico: string;
  tratamiento: string;
  observaciones?: string;
  receta?: string;
  fechaInforme: string;
}

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
  sedeId: number;
  nombreSede: string;
  especialidadId: number;
  nombreEspecialidad: string;
  informeMedico?: InformeMedico;
}

export interface CrearCita {
  fechaHora: string;
  motivo: string;
  pacienteId: number;
  doctorId: number;
  sedeId: number;
  especialidadId: number;
}