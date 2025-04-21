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
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaService categoriaService, IMapper mapper)
        {
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todas las categorias", Description = "Devuelve una lista de todas las categorias disponibles.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de categorias obtenida correctamente", typeof(IEnumerable<CategoriaDto>))]
        public ActionResult<IEnumerable<CategoriaDto>> GetAll()
        {
            var categorias = _categoriaService.GetAllCategorias();
            var categoriasDto = _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
            return Ok(categoriasDto);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtener categoria por ID", Description = "Devuelve una categoria específica usando su ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Categoria encontrada", typeof(CategoriaDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Categoria no encontrada")]
        public ActionResult<CategoriaDto> GetById(int id)
        {
            var categoria = _categoriaService.GetCategoriaById(id);
            if (categoria == null)
                return NotFound();

            var categoriaDto = _mapper.Map<CategoriaDto>(categoria);
            return Ok(categoriaDto);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crear una nueva categoria", Description = "Crea una nueva categoria en la base de datos")]
        [SwaggerResponse(200, "Categoria creada correctamente", typeof(Categoria))]
        [SwaggerResponse(400, "Datos de entrada incorrectos")]
        public ActionResult<CategoriaDto> Create([FromBody] CategoriaCreateDto createDto)
        {
            var categoria = _mapper.Map<Categoria>(createDto);
            var creada = _categoriaService.AddCategoria(categoria);
            var categoriaDto = _mapper.Map<CategoriaDto>(creada);

            return CreatedAtAction(nameof(GetById), new { id = categoriaDto.Id }, categoriaDto);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualizar una categoria", Description = "Actualiza los datos de una categoria existente.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Categoria actualizado correctamente", typeof(CategoriaDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "ID no coincide o datos inválidos")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Categoria no encontrada")]
        public ActionResult<CategoriaDto> Update(int id, [FromBody] CategoriaUpdateDto updateDto)
        {
            if (id != updateDto.Id)
                return BadRequest("El ID no coincide.");

            var categoria = _mapper.Map<Categoria>(updateDto);
            var actualizada = _categoriaService.UpdateCategoria(categoria);

            if (actualizada == null)
                return NotFound();

            var categoriaDto = _mapper.Map<CategoriaDto>(actualizada);
            return Ok(categoriaDto);
        }

        
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Eliminar una categoria", Description = "Elimina una categoria existente por su ID.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Categoria eliminada correctamente")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Categoria no encontrada")]
        public ActionResult Delete(int id)
        {
            try
            {
                _categoriaService.EliminarCategoria(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
