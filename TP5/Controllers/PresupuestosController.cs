using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using rapositoriosTP5;
using EspacioTp5;

namespace TP5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PresupuestoController : ControllerBase
    {
        private readonly IPresupuestoRepository _presupuestoRepository;

        // Inyección de dependencias en el constructor
        public PresupuestoController(IPresupuestoRepository presupuestoRepository)
        {
            _presupuestoRepository = presupuestoRepository;
        }

        // POST /api/Presupuesto: Crear un nuevo presupuesto
        [HttpPost]
        public IActionResult CrearPresupuesto([FromBody] Presupuestos presupuesto)
        {
            if (presupuesto == null)
            {
                return BadRequest("El presupuesto no puede ser nulo.");
            }
            try
            {
                _presupuestoRepository.CrearPresupuesto(presupuesto);
                return CreatedAtAction(nameof(ObtenerPresupuesto), new { id = presupuesto.IdPresupuesto }, presupuesto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear presupuesto: {ex.Message}");
            }
        }
        // POST /api/Presupuesto/{id}/ProductoDetalle: Agregar un producto y cantidad al presupuesto
        [HttpPost("{id}/ProductoDetalle")]
        public IActionResult AgregarProductoAPresupuesto(int id, [FromBody] PresupuestosDetalle detalle)
        {
            if (detalle == null || detalle.Cantidad <= 0)
            {
                return BadRequest("El detalle del producto es inválido.");
            }

            try
            {
                var presupuesto = _presupuestoRepository.ObtenerPresupuesto(id);
                if (presupuesto == null)
                {
                    return NotFound($"Presupuesto con id {id} no encontrado.");
                }

                // Agregar el producto al presupuesto a través del repositorio
                _presupuestoRepository.AgregarProductoAPresupuesto(id, detalle.Producto, detalle.Cantidad);

                // No es necesario modificar la lista del objeto presupuesto, ya que lo estamos haciendo en el repositorio.
                return NoContent(); // 204 No Content, operación exitosa sin retorno de contenido.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar producto al presupuesto: {ex.Message}");
            }
        }

        // GET /api/presupuesto: Listar todos los presupuestos
        [HttpGet]
        public ActionResult<List<Presupuestos>> ListarPresupuestos()
        {
            try
            {
                var presupuestos = _presupuestoRepository.ListarPresupuestos();
                return Ok(presupuestos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener presupuestos: {ex.Message}");
            }
        }

        // GET /api/Presupuesto/{id}: Obtener un presupuesto por su ID
        [HttpGet("{id}")]
        public ActionResult<Presupuestos> ObtenerPresupuesto(int id)
        {
            try
            {
                var presupuesto = _presupuestoRepository.ObtenerPresupuesto(id);
                if (presupuesto == null)
                {
                    return NotFound($"Presupuesto con id {id} no encontrado.");
                }

                return Ok(presupuesto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener presupuesto: {ex.Message}");
            }
        }
    }
}
