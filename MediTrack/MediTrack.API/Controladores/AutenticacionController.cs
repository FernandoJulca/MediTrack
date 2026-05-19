using MediTrack.Aplicacion.DTOs.Autenticacion;
using MediTrack.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediTrack.API.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IServicioAutenticacion _servicio;

        public AutenticacionController(IServicioAutenticacion servicio)
        {
            _servicio = servicio;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] DtoRegistro dto)
        {
            try
            {
                var resultado = await _servicio.Registrar(dto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new {mensaje = ex.Message});
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DtoLogin dto)
        {
            try
            {
                var resultado = await _servicio.Login(dto);
                return Ok(resultado);
            }
            catch (Exception ex) 
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
