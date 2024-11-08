using Microsoft.AspNetCore.Mvc;
using EspacioTp5;
using rapositoriosTP5;
using System.Collections.Generic;

namespace TiendaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _repositorio;

        public ProductoController(IProductoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        // POST /api/Producto: Permite crear un nuevo Producto.
        [HttpPost]
        public ActionResult CrearProducto([FromBody] Productos producto)
        {
            _repositorio.CrearProducto(producto);
            return CreatedAtAction(nameof(ListarProductos), new { id = producto.IdProducto }, producto);
        }

        // GET /api/Producto: Permite listar los Productos existentes.
        [HttpGet]
        public ActionResult<List<Productos>> ListarProductos()
        {
            var productos = _repositorio.ListarProductos();
            return Ok(productos);
        }

        // PUT /api/Producto/{id}: Permite modificar un nombre de un Producto.
        [HttpPut("{id}")]
        public ActionResult ModificarProducto(int id, [FromBody] string nuevoNombre)
        {
            var producto = _repositorio.ObtenerProducto(id);
            if (producto == null)
            {
                return NotFound();
            }

            // Como los set son privados, se debe usar un método adecuado para modificar el producto.
            var productoModificado = new Productos(producto.IdProducto, nuevoNombre, producto.Precio);
            _repositorio.ModificarProducto(id, productoModificado);

            return Ok(productoModificado);
        }
    }
}

