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

            // ── Medicamentos ──────────────────────────────────────
            if (!contexto.Medicamentos.Any())
            {
                var medicamentos = new List<Medicamento>
                {
                    new Medicamento
                    {
                        Nombre = "Paracetamol 500mg",
                        Descripcion = "Analgésico y antipirético",
                        Laboratorio = "Laboratorio Bayer",
                        UnidadMedida = "Tableta",
                        StockActual = 100,
                        StockMinimo = 20,
                        PrecioCompra = 0.15m,
                        PrecioVenta = 0.50m,
                        FechaVencimiento = new DateTime(2026, 12, 31),
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    },
                    new Medicamento
                    {
                        Nombre = "Ibuprofeno 400mg",
                        Descripcion = "Antiinflamatorio no esteroideo",
                        Laboratorio = "Laboratorio Genfar",
                        UnidadMedida = "Tableta",
                        StockActual = 80,
                        StockMinimo = 15,
                        PrecioCompra = 0.20m,
                        PrecioVenta = 0.70m,
                        FechaVencimiento = new DateTime(2026, 10, 31),
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    },
                    new Medicamento
                    {
                        Nombre = "Amoxicilina 500mg",
                        Descripcion = "Antibiótico de amplio espectro",
                        Laboratorio = "Laboratorio MK",
                        UnidadMedida = "Cápsula",
                        StockActual = 50,
                        StockMinimo = 10,
                        PrecioCompra = 0.80m,
                        PrecioVenta = 2.50m,
                        FechaVencimiento = new DateTime(2025, 8, 31),
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    },
                    new Medicamento
                    {
                        Nombre = "Omeprazol 20mg",
                        Descripcion = "Inhibidor de bomba de protones",
                        Laboratorio = "Laboratorio Bayer",
                        UnidadMedida = "Cápsula",
                        StockActual = 60,
                        StockMinimo = 10,
                        PrecioCompra = 0.30m,
                        PrecioVenta = 1.00m,
                        FechaVencimiento = new DateTime(2026, 6, 30),
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    },
                    new Medicamento
                    {
                        Nombre = "Loratadina 10mg",
                        Descripcion = "Antihistamínico",
                        Laboratorio = "Laboratorio Genfar",
                        UnidadMedida = "Tableta",
                        StockActual = 40,
                        StockMinimo = 10,
                        PrecioCompra = 0.25m,
                        PrecioVenta = 0.80m,
                        FechaVencimiento = new DateTime(2026, 9, 30),
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    },
                    new Medicamento
                    {
                        Nombre = "Metformina 850mg",
                        Descripcion = "Antidiabético oral",
                        Laboratorio = "Laboratorio MK",
                        UnidadMedida = "Tableta",
                        StockActual = 15,
                        StockMinimo = 20,
                        PrecioCompra = 0.40m,
                        PrecioVenta = 1.20m,
                        FechaVencimiento = new DateTime(2026, 3, 31),
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    },
                    new Medicamento
                    {
                        Nombre = "Enalapril 10mg",
                        Descripcion = "Antihipertensivo",
                        Laboratorio = "Laboratorio Bayer",
                        UnidadMedida = "Tableta",
                        StockActual = 70,
                        StockMinimo = 15,
                        PrecioCompra = 0.35m,
                        PrecioVenta = 1.10m,
                        FechaVencimiento = new DateTime(2026, 11, 30),
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    },
                    new Medicamento
                    {
                        Nombre = "Azitromicina 500mg",
                        Descripcion = "Antibiótico macrólido",
                        Laboratorio = "Laboratorio Genfar",
                        UnidadMedida = "Tableta",
                        StockActual = 8,
                        StockMinimo = 15,
                        PrecioCompra = 1.50m,
                        PrecioVenta = 4.00m,
                        FechaVencimiento = new DateTime(2025, 7, 31),
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    }
                };

                await contexto.Medicamentos.AddRangeAsync(medicamentos);
                await contexto.SaveChangesAsync();
            }

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
                        Estado = EstadoCita.Pendiente,
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
                        Estado = EstadoCita.Pendiente,
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
