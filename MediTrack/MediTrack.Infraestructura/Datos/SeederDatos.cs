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
            // ── Especialidades ────────────────────────────────────
            if (!contexto.Especialidades.Any())
            {
                var especialidades = new List<Especialidad>
                {
                    new Especialidad { Nombre = "Medicina General", Descripcion = "Atención médica primaria y preventiva", Icono = "bi-heart-pulse", FechaCreacion = DateTime.UtcNow, Activo = true },
                    new Especialidad { Nombre = "Cardiología", Descripcion = "Diagnóstico y tratamiento de enfermedades del corazón", Icono = "bi-heart", FechaCreacion = DateTime.UtcNow, Activo = true },
                    new Especialidad { Nombre = "Pediatría", Descripcion = "Atención médica para niños y adolescentes", Icono = "bi-person-hearts", FechaCreacion = DateTime.UtcNow, Activo = true },
                    new Especialidad { Nombre = "Dermatología", Descripcion = "Diagnóstico y tratamiento de enfermedades de la piel", Icono = "bi-bandaid", FechaCreacion = DateTime.UtcNow, Activo = true },
                    new Especialidad { Nombre = "Neurología", Descripcion = "Diagnóstico y tratamiento de enfermedades del sistema nervioso", Icono = "bi-brain", FechaCreacion = DateTime.UtcNow, Activo = true }
                };

                await contexto.Especialidades.AddRangeAsync(especialidades);
                await contexto.SaveChangesAsync();
            }

            // ── Sedes ─────────────────────────────────────────────
            if (!contexto.Sedes.Any())
            {
                var sedes = new List<Sede>
                {
                    new Sede
                    {
                        Nombre = "MediTrack Central",
                        Direccion = "Av. Javier Prado 1234",
                        Telefono = "01-4441234",
                        Ciudad = "Lima",
                        Descripcion = "Sede principal con todas las especialidades disponibles",
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    },
                    new Sede
                    {
                        Nombre = "MediTrack Miraflores",
                        Direccion = "Av. Larco 567",
                        Telefono = "01-4445678",
                        Ciudad = "Lima",
                        Descripcion = "Sede especializada en cardiología y neurología",
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    },
                    new Sede
                    {
                        Nombre = "MediTrack San Isidro",
                        Direccion = "Calle Los Libertadores 890",
                        Telefono = "01-4449012",
                        Ciudad = "Lima",
                        Descripcion = "Sede especializada en pediatría y dermatología",
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    }
                };

                await contexto.Sedes.AddRangeAsync(sedes);
                await contexto.SaveChangesAsync();
            }

            // ── Usuarios ──────────────────────────────────────────
            if (!contexto.Usuarios.Any())
            {
                var especialidades = contexto.Especialidades.ToList();
                var espMedGeneral = especialidades.First(e => e.Nombre == "Medicina General");
                var espCardiologia = especialidades.First(e => e.Nombre == "Cardiología");
                var espPediatria = especialidades.First(e => e.Nombre == "Pediatría");
                var espDermatologia = especialidades.First(e => e.Nombre == "Dermatología");
                var espNeurologia = especialidades.First(e => e.Nombre == "Neurología");

                var usuarios = new List<Usuario>
                {
                    // Administrador
                    new Usuario
                    {
                        Nombres = "Carlos", Apellidos = "Mendoza",
                        Correo = "admin@meditrack.com",
                        ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                        Telefono = "999111000", Dni = "12345678",
                        Rol = RolUsuario.Administrador,
                        FechaCreacion = DateTime.UtcNow, Activo = true
                    },
                    // Recepcionistas
                    new Usuario
                    {
                        Nombres = "María", Apellidos = "Torres",
                        Correo = "recepcion@meditrack.com",
                        ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                        Telefono = "999222000", Dni = "23456789",
                        Rol = RolUsuario.Recepcionista,
                        FechaCreacion = DateTime.UtcNow, Activo = true
                    },
                    // Doctores
                    new Usuario
                    {
                        Nombres = "Jorge", Apellidos = "Ramírez",
                        Correo = "doctor1@meditrack.com",
                        ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                        Telefono = "999333000", Dni = "34567890",
                        Rol = RolUsuario.Doctor,
                        EspecialidadId = espMedGeneral.Id,
                        Biografia = "Médico general con 10 años de experiencia en atención primaria.",
                        FechaCreacion = DateTime.UtcNow, Activo = true
                    },
                    new Usuario
                    {
                        Nombres = "Ana", Apellidos = "Flores",
                        Correo = "doctor2@meditrack.com",
                        ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                        Telefono = "999444000", Dni = "45678901",
                        Rol = RolUsuario.Doctor,
                        EspecialidadId = espCardiologia.Id,
                        Biografia = "Cardióloga especialista en enfermedades cardiovasculares.",
                        FechaCreacion = DateTime.UtcNow, Activo = true
                    },
                    new Usuario
                    {
                        Nombres = "Pedro", Apellidos = "Sánchez",
                        Correo = "doctor3@meditrack.com",
                        ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                        Telefono = "999555001", Dni = "56789013",
                        Rol = RolUsuario.Doctor,
                        EspecialidadId = espPediatria.Id,
                        Biografia = "Pediatra con amplia experiencia en atención infantil.",
                        FechaCreacion = DateTime.UtcNow, Activo = true
                    },
                    new Usuario
                    {
                        Nombres = "Laura", Apellidos = "Vega",
                        Correo = "doctor4@meditrack.com",
                        ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                        Telefono = "999555002", Dni = "56789014",
                        Rol = RolUsuario.Doctor,
                        EspecialidadId = espDermatologia.Id,
                        Biografia = "Dermatóloga especialista en enfermedades de la piel.",
                        FechaCreacion = DateTime.UtcNow, Activo = true
                    },
                    new Usuario
                    {
                        Nombres = "Roberto", Apellidos = "Cruz",
                        Correo = "doctor5@meditrack.com",
                        ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                        Telefono = "999555003", Dni = "56789015",
                        Rol = RolUsuario.Doctor,
                        EspecialidadId = espNeurologia.Id,
                        Biografia = "Neurólogo con especialización en enfermedades del sistema nervioso.",
                        FechaCreacion = DateTime.UtcNow, Activo = true
                    },
                    // Pacientes
                    new Usuario
                    {
                        Nombres = "Luis", Apellidos = "García",
                        Correo = "paciente1@meditrack.com",
                        ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                        Telefono = "999666000", Dni = "67890123",
                        Rol = RolUsuario.Paciente,
                        FechaCreacion = DateTime.UtcNow, Activo = true
                    },
                    new Usuario
                    {
                        Nombres = "Rosa", Apellidos = "Quispe",
                        Correo = "paciente2@meditrack.com",
                        ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                        Telefono = "999777000", Dni = "78901234",
                        Rol = RolUsuario.Paciente,
                        FechaCreacion = DateTime.UtcNow, Activo = true
                    }
                };

                await contexto.Usuarios.AddRangeAsync(usuarios);
                await contexto.SaveChangesAsync();

                // ── Horarios de doctores ───────────────────────────
                var sedes = contexto.Sedes.ToList();
                var sedeCentral = sedes.First(s => s.Nombre == "MediTrack Central");
                var sedeMiraflores = sedes.First(s => s.Nombre == "MediTrack Miraflores");
                var sedeSanIsidro = sedes.First(s => s.Nombre == "MediTrack San Isidro");

                var doctor1 = usuarios.First(u => u.Correo == "doctor1@meditrack.com");
                var doctor2 = usuarios.First(u => u.Correo == "doctor2@meditrack.com");
                var doctor3 = usuarios.First(u => u.Correo == "doctor3@meditrack.com");

                var horarios = new List<HorarioDoctor>
                {
                    // Doctor1 - Lunes a Viernes en sede Central
                    new HorarioDoctor { DoctorId = doctor1.Id, SedeId = sedeCentral.Id, DiaSemana = 1, HoraInicio = new TimeOnly(8, 0), HoraFin = new TimeOnly(13, 0), DuracionCitaMinutos = 30, FechaCreacion = DateTime.UtcNow, Activo = true },
                    new HorarioDoctor { DoctorId = doctor1.Id, SedeId = sedeCentral.Id, DiaSemana = 2, HoraInicio = new TimeOnly(8, 0), HoraFin = new TimeOnly(13, 0), DuracionCitaMinutos = 30, FechaCreacion = DateTime.UtcNow, Activo = true },
                    new HorarioDoctor { DoctorId = doctor1.Id, SedeId = sedeCentral.Id, DiaSemana = 3, HoraInicio = new TimeOnly(8, 0), HoraFin = new TimeOnly(13, 0), DuracionCitaMinutos = 30, FechaCreacion = DateTime.UtcNow, Activo = true },
                    new HorarioDoctor { DoctorId = doctor1.Id, SedeId = sedeCentral.Id, DiaSemana = 4, HoraInicio = new TimeOnly(8, 0), HoraFin = new TimeOnly(13, 0), DuracionCitaMinutos = 30, FechaCreacion = DateTime.UtcNow, Activo = true },
                    new HorarioDoctor { DoctorId = doctor1.Id, SedeId = sedeCentral.Id, DiaSemana = 5, HoraInicio = new TimeOnly(8, 0), HoraFin = new TimeOnly(13, 0), DuracionCitaMinutos = 30, FechaCreacion = DateTime.UtcNow, Activo = true },

                    // Doctor2 - Lunes, Miércoles, Viernes en Miraflores
                    new HorarioDoctor { DoctorId = doctor2.Id, SedeId = sedeMiraflores.Id, DiaSemana = 1, HoraInicio = new TimeOnly(9, 0), HoraFin = new TimeOnly(14, 0), DuracionCitaMinutos = 45, FechaCreacion = DateTime.UtcNow, Activo = true },
                    new HorarioDoctor { DoctorId = doctor2.Id, SedeId = sedeMiraflores.Id, DiaSemana = 3, HoraInicio = new TimeOnly(9, 0), HoraFin = new TimeOnly(14, 0), DuracionCitaMinutos = 45, FechaCreacion = DateTime.UtcNow, Activo = true },
                    new HorarioDoctor { DoctorId = doctor2.Id, SedeId = sedeMiraflores.Id, DiaSemana = 5, HoraInicio = new TimeOnly(9, 0), HoraFin = new TimeOnly(14, 0), DuracionCitaMinutos = 45, FechaCreacion = DateTime.UtcNow, Activo = true },

                    // Doctor3 - Martes y Jueves en San Isidro
                    new HorarioDoctor { DoctorId = doctor3.Id, SedeId = sedeSanIsidro.Id, DiaSemana = 2, HoraInicio = new TimeOnly(10, 0), HoraFin = new TimeOnly(16, 0), DuracionCitaMinutos = 30, FechaCreacion = DateTime.UtcNow, Activo = true },
                    new HorarioDoctor { DoctorId = doctor3.Id, SedeId = sedeSanIsidro.Id, DiaSemana = 4, HoraInicio = new TimeOnly(10, 0), HoraFin = new TimeOnly(16, 0), DuracionCitaMinutos = 30, FechaCreacion = DateTime.UtcNow, Activo = true },
                };

                await contexto.HorariosDoctores.AddRangeAsync(horarios);
                await contexto.SaveChangesAsync();

                // ── Citas de prueba ───────────────────────────────
                var paciente1 = usuarios.First(u => u.Correo == "paciente1@meditrack.com");
                var paciente2 = usuarios.First(u => u.Correo == "paciente2@meditrack.com");

                var citas = new List<Cita>
                {
                    new Cita
                    {
                        FechaHora = DateTime.UtcNow.AddDays(1).Date.AddHours(9),
                        Motivo = "Control general",
                        Estado = EstadoCita.Confirmada,
                        PacienteId = paciente1.Id,
                        DoctorId = doctor1.Id,
                        SedeId = sedeCentral.Id,
                        EspecialidadId = espMedGeneral.Id,
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    },
                    new Cita
                    {
                        FechaHora = DateTime.UtcNow.AddDays(1).Date.AddHours(10),
                        Motivo = "Dolor de cabeza frecuente",
                        Estado = EstadoCita.Agendada,
                        PacienteId = paciente2.Id,
                        DoctorId = doctor1.Id,
                        SedeId = sedeCentral.Id,
                        EspecialidadId = espMedGeneral.Id,
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    },
                    new Cita
                    {
                        FechaHora = DateTime.UtcNow.AddDays(-1).Date.AddHours(9),
                        Motivo = "Revisión cardiovascular",
                        Estado = EstadoCita.Completada,
                        PacienteId = paciente1.Id,
                        DoctorId = doctor2.Id,
                        SedeId = sedeMiraflores.Id,
                        EspecialidadId = espCardiologia.Id,
                        FechaCreacion = DateTime.UtcNow,
                        Activo = true
                    },
                    new Cita
                    {
                        FechaHora = DateTime.UtcNow.AddDays(-2).Date.AddHours(10),
                        Motivo = "Control rutinario",
                        Estado = EstadoCita.NoSePresento,
                        PacienteId = paciente2.Id,
                        DoctorId = doctor3.Id,
                        SedeId = sedeSanIsidro.Id,
                        EspecialidadId = espPediatria.Id,
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
