using AutoMapper;
using Ejercicio3.Core.DTOs;
using Ejercicio3.Core.Interfaces;
using Ejercicio3.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<ProductoDto>> GetAll()
        {
            var productos = _productoService.GetAllProductos();
            var productosDto = _mapper.Map<IEnumerable<ProductoDto>>(productos);
            return Ok(productosDto);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductoDto> GetById(int id)
        {
            var producto = _productoService.GetProductoById(id);
            if (producto == null)
                return NotFound();

            var productoDto = _mapper.Map<ProductoDto>(producto);
            return Ok(productoDto);
        }

        [HttpPost]
        public ActionResult<ProductoDto> Create([FromBody] ProductoCreateDto createDto)
        {
            var producto = _mapper.Map<Producto>(createDto);
            var creado = _productoService.AddProducto(producto);
            var productoDto = _mapper.Map<ProductoDto>(creado);

            return CreatedAtAction(nameof(GetById), new { id = productoDto.Id }, productoDto);
        }

        [HttpPut("{id}")]
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
