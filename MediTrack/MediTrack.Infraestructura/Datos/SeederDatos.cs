using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediTrack.Dominio.Entidades;
using MediTrack.Dominio.Enumeraciones;
using BCrypt.Net;

namespace MediTrack.Infraestructura.Datos
{
    public static class SeederDatos
    {
        public static async Task EjecutarAsync(ContextoAplicacion contexto)
        {
            // Solo ejecuta si no hay usuarios
            if (contexto.Usuarios.Any()) return;

            // ── Usuarios ──────────────────────────────────────────
            var usuarios = new List<Usuario>
            {
                new Usuario
                {
                    Nombres = "Carlos",
                    Apellidos = "Mendoza",
                    Correo = "admin@meditrack.com",
                    ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Telefono = "999111000",
                    Dni = "12345678",
                    Rol = RolUsuario.Administrador,
                    FechaCreacion = DateTime.UtcNow,
                    Activo = true
                },
                new Usuario
                {
                    Nombres = "María",
                    Apellidos = "Torres",
                    Correo = "recepcion@meditrack.com",
                    ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Telefono = "999222000",
                    Dni = "23456789",
                    Rol = RolUsuario.Recepcionista,
                    FechaCreacion = DateTime.UtcNow,
                    Activo = true
                },
                new Usuario
                {
                    Nombres = "Jorge",
                    Apellidos = "Ramírez",
                    Correo = "doctor1@meditrack.com",
                    ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Telefono = "999333000",
                    Dni = "34567890",
                    Rol = RolUsuario.Doctor,
                    FechaCreacion = DateTime.UtcNow,
                    Activo = true
                },
                new Usuario
                {
                    Nombres = "Ana",
                    Apellidos = "Flores",
                    Correo = "doctor2@meditrack.com",
                    ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Telefono = "999444000",
                    Dni = "45678901",
                    Rol = RolUsuario.Doctor,
                    FechaCreacion = DateTime.UtcNow,
                    Activo = true
                },
                new Usuario
                {
                    Nombres = "Luis",
                    Apellidos = "García",
                    Correo = "paciente1@meditrack.com",
                    ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Telefono = "999555000",
                    Dni = "56789012",
                    Rol = RolUsuario.Paciente,
                    FechaCreacion = DateTime.UtcNow,
                    Activo = true
                },
                new Usuario
                {
                    Nombres = "Rosa",
                    Apellidos = "Quispe",
                    Correo = "paciente2@meditrack.com",
                    ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Telefono = "999666000",
                    Dni = "67890123",
                    Rol = RolUsuario.Paciente,
                    FechaCreacion = DateTime.UtcNow,
                    Activo = true
                }
            };

            await contexto.Usuarios.AddRangeAsync(usuarios);
            await contexto.SaveChangesAsync();

            

            // ── Citas ─────────────────────────────────────────────
            if (!contexto.Citas.Any())
            {
                var doctor1Id = usuarios.First(u => u.Correo == "doctor1@meditrack.com").Id;
                var doctor2Id = usuarios.First(u => u.Correo == "doctor2@meditrack.com").Id;
                var paciente1Id = usuarios.First(u => u.Correo == "paciente1@meditrack.com").Id;
                var paciente2Id = usuarios.First(u => u.Correo == "paciente2@meditrack.com").Id;

                var citas = new List<Cita>
                {
                    new Cita
                    {
                        FechaHora = DateTime.UtcNow.AddDays(1).Date.AddHours(9),
                        Motivo = "Control general",
                        Estado = EstadoCita.Agendada,
                        PacienteId = paciente1Id,
                        DoctorId = doctor1Id,
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    },
                    new Cita
                    {
                        FechaHora = DateTime.UtcNow.AddDays(1).Date.AddHours(10),
                        Motivo = "Dolor de cabeza frecuente",
                        Estado = EstadoCita.Confirmada,
                        PacienteId = paciente2Id,
                        DoctorId = doctor1Id,
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    },
                    new Cita
                    {
                        FechaHora = DateTime.UtcNow.AddDays(2).Date.AddHours(8),
                        Motivo = "Revisión de presión arterial",
                        Estado = EstadoCita.Agendada,
                        PacienteId = paciente1Id,
                        DoctorId = doctor2Id,
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    },
                    new Cita
                    {
                        FechaHora = DateTime.UtcNow.AddDays(-1).Date.AddHours(11),
                        Motivo = "Consulta por gripe",
                        Observaciones = "Paciente con fiebre alta",
                        Estado = EstadoCita.Completada,
                        PacienteId = paciente2Id,
                        DoctorId = doctor2Id,
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    },
                    new Cita
                    {
                        FechaHora = DateTime.UtcNow.AddDays(-2).Date.AddHours(9),
                        Motivo = "Control diabetes",
                        Estado = EstadoCita.Cancelada,
                        PacienteId = paciente1Id,
                        DoctorId = doctor1Id,
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    }
                };

                await contexto.Citas.AddRangeAsync(citas);
                await contexto.SaveChangesAsync();
            }
        }
    }
}
