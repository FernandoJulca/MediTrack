using MediTrack.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediTrack.API.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private readonly IServicioEspecialidades _servicio;

        public EspecialidadesController(IServicioEspecialidades servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var resultado = await _servicio.ObtenerTodas();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
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
    }
}
