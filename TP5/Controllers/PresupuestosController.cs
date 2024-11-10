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
        private readonly PresupuestosRepository _presupuestosRepository;

        // Constructor que crea el repositorio de presupuestos
        public PresupuestoController()
        {
            _presupuestosRepository = new PresupuestosRepository();  // Si es necesario, este repositorio puede ser inyectado
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
                // Llamada al repositorio para crear el presupuesto
                _presupuestosRepository.CrearPresupuesto(presupuesto);
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
                // Aquí se asume que el producto existe en el sistema y ya tiene los datos necesarios
                var presupuesto = _presupuestosRepository.ObtenerPresupuesto(id);

                if (presupuesto == null)
                {
                    return NotFound($"Presupuesto con id {id} no encontrado.");
                }

                // Se agrega el producto al presupuesto
                presupuesto.Detalle.Add(detalle);
                // Nota: La base de datos también debería ser actualizada para reflejar esta adición si es necesario.
                _presupuestosRepository.AgregarProductoAPresupuesto(id, detalle.Producto, detalle.Cantidad);
                
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
                var presupuestos = _presupuestosRepository.ListarPresupuestos();
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
                var presupuesto = _presupuestosRepository.ObtenerPresupuesto(id);
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
