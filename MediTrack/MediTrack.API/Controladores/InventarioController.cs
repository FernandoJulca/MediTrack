using MediTrack.Aplicacion.DTOs.Inventario;
using MediTrack.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediTrack.API.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventarioController : ControllerBase
    {
        private readonly IServicioInventario _servicio;

        public InventarioController(IServicioInventario servicio)
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

        [HttpPost]
        [Authorize(Roles = "Administrado")]
        public async Task<IActionResult> Crear([FromBody] DtoCrearMedicamento dto)
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

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] DtoActualizarMedicamento dto)
        {
            try
            {
                var resultado = await _servicio.Actualizar(id, dto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _servicio.Eliminar(id);
                return Ok(new { mensaje = "Medicamento eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("stock-bajo")]
        [Authorize(Roles = "Administrador,Recepcionista")]
        public async Task<IActionResult> ObtenerConStockBajo()
        {
            var resultado = await _servicio.ObtenerConStockBajo();
            return Ok(resultado);
        }

        [HttpGet("por-vencer/{dias}")]
        [Authorize(Roles = "Administrador,Recepcionista")]
        public async Task<IActionResult> ObtenerPorVencer(int dias)
        {
            var resultado = await _servicio.ObtenerPorVencer(dias);
            return Ok(resultado);
        }

        [HttpPatch("ajustar-stock")]
        [Authorize(Roles = "Administrador,Recepcionista")]
        public async Task<IActionResult> AjustarStock([FromBody] DtoAjustarStock dto)
        {
            try
            {
                var resultado = await _servicio.AjustarStock(dto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
