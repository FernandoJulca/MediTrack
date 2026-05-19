using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using MediTrack.Aplicacion.DTOs.Autenticacion;
using MediTrack.Aplicacion.Interfaces;
using MediTrack.Infraestructura.Datos;
using Microsoft.EntityFrameworkCore;
using MediTrack.Dominio.Entidades;
using MediTrack.Dominio.Enumeraciones;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace MediTrack.Aplicacion.Servicios
{
    public class ServicioAutenticacion : IServicioAutenticacion
    {
        private readonly ContextoAplicacion _contexto;
        private readonly IConfiguration _configuracion;

        public ServicioAutenticacion(ContextoAplicacion contexto, IConfiguration configuracion)
        {
            _contexto = contexto;
            _configuracion = configuracion;
        }

        public async Task<DtoRespuestaAuth> Registrar(DtoRegistro dto)
        {
            //verificar si el correo existe
            var existeCorreo = await _contexto.Usuarios
                .AnyAsync(u => u.Correo == dto.Correo);

            if (existeCorreo)
            {
                throw new Exception("El correo ya está registrado.");
            }

            //verificar si el dni ya existe
            var existeDni = await _contexto.Usuarios
                .AnyAsync(u => u.Dni == dto.Dni);
            if (existeDni)
            {
                throw new Exception("El DNI ya está registrado");
            }

            //crear el usuario
            var usuario = new Usuario
            {
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                Correo = dto.Correo,
                ContrasenaHash = BCrypt.Net.BCrypt.HashPassword(dto.Contrasena),
                Telefono = dto.Telefono,
                Dni = dto.Dni,
                Rol = (RolUsuario)dto.Rol,
            };

            _contexto.Usuarios.Add(usuario);
            await _contexto.SaveChangesAsync();

            return GenerarRespuesta(usuario);
        }

        public async Task<DtoRespuestaAuth> Login(DtoLogin dto)
        {
            var usuario = await _contexto.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == dto.Correo && u.Activo);

            if (usuario == null)
            {
                throw new Exception("Credenciales incorrectas.");
            }

            var contrasenaValida = BCrypt.Net.BCrypt.Verify(dto.Contrasena, usuario.ContrasenaHash);

            if (!contrasenaValida)
                throw new Exception("Credenciales incorrectas.");

            return GenerarRespuesta(usuario);
        }

        private DtoRespuestaAuth GenerarRespuesta(Usuario usuario)
        {
            var token = GenerarToken(usuario);
            var expiracion = DateTime.UtcNow.AddMinutes(
                double.Parse(_configuracion["Jwt:ExpiracionMinutos"]!));

            return new DtoRespuestaAuth
            {
                Token = token,
                Correo = usuario.Correo,
                NombreCompleto = $"{usuario.Nombres} {usuario.Apellidos}",
                Rol = usuario.Rol.ToString(),
                Expiracion = expiracion
            };

        }

        private string GenerarToken(Usuario usuario)
        {
            var clave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuracion["Jwt:Clave"]!));
            
            var credenciales = new SigningCredentials( clave, SecurityAlgorithms.HmacSha256 );

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString()),
                new Claim(ClaimTypes.Name, $"{usuario.Nombres} {usuario.Apellidos}")
            };

            var token = new JwtSecurityToken(
                issuer: _configuracion["Jwt:Emisor"],
                audience: _configuracion["Jwt:Audiencia"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    double.Parse(_configuracion["Jwt:ExpiracionMinutos"]!)),
                signingCredentials: credenciales
            );

            return new JwtSecurityTokenHandler().WriteToken( token );
        }
    }
}
