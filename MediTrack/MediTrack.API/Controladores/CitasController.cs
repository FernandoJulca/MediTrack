using MediTrack.Aplicacion.DTOs.Citas;
using MediTrack.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediTrack.API.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CitasController : ControllerBase
    {
        private readonly IServicioCitas _servicio;

        public CitasController(IServicioCitas servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Recepcionista,Doctor")]
        public async Task<IActionResult> ObtenerTodas()
        {
            var resultado = await _servicio.ObtenerTodas();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> ObtenerPorPaciente(int pacienteId)
        {
            var resultado = await _servicio.ObtenerPorPaciente(pacienteId);
            return Ok(resultado);
        }

        [HttpGet("doctor/{doctorId}")]
        [Authorize(Roles = "Administrador,Recepcionista,Doctor")]
        public async Task<IActionResult> ObtenerPorDoctor(int doctorId)
        {
            var resultado = await _servicio.ObtenerPorDoctor(doctorId);
            return Ok(resultado);
        }

        [HttpGet("sede/{sedeId}/fecha/{fecha}")]
        [Authorize(Roles = "Administrador,Recepcionista")]
        public async Task<IActionResult> ObtenerPorFechaYSede(int sedeId, DateTime fecha)
        {
            var resultado = await _servicio.ObtenerPorFechaYSede(fecha, sedeId);
            return Ok(resultado);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Crear([FromBody] DtoCrearCita dto)
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

        [HttpPatch("{id}/estado")]
        [Authorize(Roles = "Administrador,Recepcionista,Doctor")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] DtoCambiarEstadoCita dto)
        {
            try
            {
                var resultado = await _servicio.CambiarEstado(id, dto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("informe")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> CrearInforme([FromBody] DtoCrearInformeMedico dto)
        {
            try
            {
                var resultado = await _servicio.CrearInformeMedico(dto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPatch("{id}/cancelar")]
        [Authorize]
        public async Task<IActionResult> Cancelar(int id)
        {
            try
            {
                await _servicio.Cancelar(id);
                return Ok(new { mensaje = "Cita cancelada correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
