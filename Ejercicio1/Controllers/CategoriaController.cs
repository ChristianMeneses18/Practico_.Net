using Ejercicio1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ejercicio1.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View(CategoriaProducto.Categorias);
        }

        public IActionResult Create()
        {
            ViewBag.Categorias = CategoriaProducto.Categorias;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            try
            {
                // Asigna la fecha actual si no se proporciona
                if (categoria.FechaCreacion == default)
                {
                    categoria.FechaCreacion = DateOnly.FromDateTime(DateTime.Now); // Si no se especifica, se asigna la fecha de hoy
                }

                // Agregar la nueva categoría a la lista estática
                CategoriaProducto.AgregarCategoria(categoria);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ViewBag.Categorias = CategoriaProducto.Categorias;
                return View(categoria);
            }
        }

        public IActionResult Edit(int id)
        {
            var categoria = CategoriaProducto.Categorias.FirstOrDefault(p => p.Id == id);
            if (categoria == null)
                return NotFound();

            return PartialView("_EditarCategoriaPartial", categoria);
        }

        [HttpPost]
        public IActionResult EditarPost(Categoria p)
        {
            var existente = CategoriaProducto.Categorias.FirstOrDefault(x => x.Id == p.Id);
            if (existente != null)
            {
                // Actualiza los valores del producto con los datos enviados
                existente.Nombre = p.Nombre;
                existente.FechaCreacion = p.FechaCreacion;
            }

            return RedirectToAction("Index"); // Redirige al índice para ver los cambios
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var existente = CategoriaProducto.Categorias.FirstOrDefault(x => x.Id == id);
            if (existente == null)
                return NotFound();

            if (CategoriaProducto.Productos.Any(p => p.CategoriaId == id))
            {
                return BadRequest("No se puede eliminar la categoría porque tiene productos asociados.");
            }

            CategoriaProducto.Categorias.Remove(existente);
            return Ok();
        }

    }
}
