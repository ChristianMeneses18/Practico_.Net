using AutoMapper;
using Ejercicio3.Core.DTOs;
using Ejercicio3.Core.Interfaces;
using Ejercicio3.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ejercicio3.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;
        private readonly IMapper _mapper;


        public ProductoController(IProductoService productoService , IMapper mapper)
        {
            _productoService = productoService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todos los productos", Description = "Devuelve una lista de todos los productos disponibles.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de productos obtenida correctamente", typeof(IEnumerable<ProductoDto>))]
        public ActionResult<IEnumerable<ProductoDto>> GetAll()
        {
            var productos = _productoService.GetAllProductos();
            var productosDto = _mapper.Map<IEnumerable<ProductoDto>>(productos);
            return Ok(productosDto);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtener producto por ID", Description = "Devuelve un producto específico usando su ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Producto encontrado", typeof(ProductoDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Producto no encontrado")]
        public ActionResult<ProductoDto> GetById(int id)
        {
            var producto = _productoService.GetProductoById(id);
            if (producto == null)
                return NotFound();

            var productoDto = _mapper.Map<ProductoDto>(producto);
            return Ok(productoDto);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crear un nuevo producto", Description = "Crea un nuevo producto en la base de datos")]
        [SwaggerResponse(200, "Producto creado correctamente", typeof(Producto))]
        [SwaggerResponse(400, "Datos de entrada incorrectos")]
        public ActionResult<ProductoDto> Create([FromBody] ProductoCreateDto createDto)
        {
            var producto = _mapper.Map<Producto>(createDto);
            var creado = _productoService.AddProducto(producto);
            var productoDto = _mapper.Map<ProductoDto>(creado);

            return CreatedAtAction(nameof(GetById), new { id = productoDto.Id }, productoDto);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualizar un producto", Description = "Actualiza los datos de un producto existente.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Producto actualizado correctamente", typeof(ProductoDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "ID no coincide o datos inválidos")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Producto no encontrado")]
        public ActionResult<ProductoDto> Update(int id, [FromBody] ProductoUpdateDto updateDto)
        {
            if (id != updateDto.Id)
                return BadRequest("El ID no coincide.");

            var producto = _mapper.Map<Producto>(updateDto);
            var actualizado = _productoService.UpdateProducto(producto);

            if (actualizado == null)
                return NotFound();

            var productoDto = _mapper.Map<ProductoDto>(actualizado);
            return Ok(productoDto);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Eliminar un producto", Description = "Elimina un producto existente por su ID.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Producto eliminado correctamente")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Producto no encontrado")]
        public ActionResult Delete(int id)
        {
            var producto = _productoService.GetProductoById(id);
            if (producto == null)
                return NotFound();

            _productoService.EliminarProducto(id);
            return NoContent();
        }
    }
}
