using Ejercicio2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ejercicio2.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categorias = _context.Categorias.ToList(); // Obtener las categorías de la base de datos
            return View(categorias);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            try
            {
                if (categoria.FechaCreacion == default)
                {
                    categoria.FechaCreacion = DateOnly.FromDateTime(DateTime.Now); // Asigna la fecha actual si no se proporciona
                }

                _context.Categorias.Add(categoria); // Agregar la nueva categoría a la base de datos
                _context.SaveChanges(); // Guardar los cambios en la base de datos

                return RedirectToAction("Index"); // Redirigir a la lista de categorías
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(categoria);
            }
        }


        public IActionResult Edit(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                return NotFound(); // Si no se encuentra la categoría
            }

            return PartialView("_EditarCategoriaPartial", categoria);
        }

        [HttpPost]
        public IActionResult EditarPost(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var existente = _context.Categorias.FirstOrDefault(c => c.Id == categoria.Id);
                if (existente != null)
                {
                    // Actualiza los valores de la categoría
                    existente.Nombre = categoria.Nombre;
                    existente.FechaCreacion = categoria.FechaCreacion;

                    _context.SaveChanges(); // Guarda los cambios en la base de datos
                    return RedirectToAction("Index"); // Redirige a la lista de categorías
                }
            }

            return View(categoria); // Si hay errores, vuelve a mostrar la vista
        }


        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var existente = _context.Categorias.FirstOrDefault(c => c.Id == id);
            if (existente == null)
            {
                return NotFound(); // Si no se encuentra la categoría
            }

            // Verificar si hay productos asociados
            if (_context.Productos.Any(p => p.CategoriaId == id))
            {
                return BadRequest("No se puede eliminar la categoría porque tiene productos asociados.");
            }

            _context.Categorias.Remove(existente); // Eliminar la categoría de la base de datos
            _context.SaveChanges(); // Guardar los cambios en la base de datos

            return Ok(); // Devolver respuesta exitosa


        }
    }
}
