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
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        // Inyección de dependencias
        public CategoriaController(ICategoriaService categoriaService, IMapper mapper)
        {
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        // GET api/categoria
        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDto>> GetAll()
        {
            var categorias = _categoriaService.GetAllCategorias();
            var categoriasDto = _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
            return Ok(categoriasDto);
        }

        // GET api/categoria/{id}
        [HttpGet("{id}")]
        public ActionResult<CategoriaDto> GetById(int id)
        {
            var categoria = _categoriaService.GetCategoriaById(id);
            if (categoria == null)
                return NotFound();

            var categoriaDto = _mapper.Map<CategoriaDto>(categoria);
            return Ok(categoriaDto);
        }

        // POST api/categoria
        [HttpPost]
        public ActionResult<CategoriaDto> Create([FromBody] CategoriaCreateDto createDto)
        {
            var categoria = _mapper.Map<Categoria>(createDto);
            var creada = _categoriaService.AddCategoria(categoria);
            var categoriaDto = _mapper.Map<CategoriaDto>(creada);

            return CreatedAtAction(nameof(GetById), new { id = categoriaDto.Id }, categoriaDto);
        }

        // PUT api/categoria/{id}
        [HttpPut("{id}")]
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

        // DELETE api/categoria/{id}
        [HttpDelete("{id}")]
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
