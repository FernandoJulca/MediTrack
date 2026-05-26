using MediTrack.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediTrack.API.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctoresController : ControllerBase
    {
        private readonly IServicioDoctores _servicio;

        public DoctoresController(IServicioDoctores servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var resultado = await _servicio.ObtenerTodos();
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

        [HttpGet("especialidad/{especialidadId}")]
        public async Task<IActionResult> ObtenerPorEspecialidad(int especialidadId)
        {
            var resultado = await _servicio.ObtenerPorEspecialidad(especialidadId);
            return Ok(resultado);
        }

        [HttpGet("sede/{sedeId}")]
        public async Task<IActionResult> ObtenerPorSede(int sedeId)
        {
            var resultado = await _servicio.ObtenerPorSede(sedeId);
            return Ok(resultado);
        }
    }
}
