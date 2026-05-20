export interface LoginRequest {
  correo: string;
  contrasena: string;
}

export interface AuthResponse {
  token: string;
  correo: string;
  nombreCompleto: string;
  rol: string;
  expiracion: string;
}

export interface UsuarioSesion {
  token: string;
  correo: string;
  nombreCompleto: string;
  rol: string;
}