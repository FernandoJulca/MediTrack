using MediTrack.Aplicacion.DTOs.Ventas;
using MediTrack.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediTrack.API.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentasController : ControllerBase
    {
        private readonly IServicioVentas _servicio;

        public VentasController(IServicioVentas servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Recepcionista")]
        public async Task<IActionResult> ObtenerTodas()
        {
            var resultado = await _servicio.ObtenerTodas();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador,Recepcionista")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var resultado = await _servicio.ObtenerPorId(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpGet("paciente/{pacienteId}")]
        public async Task<IActionResult> ObtenerPorPaciente(int pacienteId)
        {
            try
            {
                var resultado = await _servicio.ObtenerPorPaciente(pacienteId);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Recepcionista")]
        public async Task<IActionResult> Crear([FromBody] DtoCrearVenta dto)
        {
            try
            {
                var resultado = await _servicio.Crear(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = resultado.Id }, resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
