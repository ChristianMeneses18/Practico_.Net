using Ejercicio1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ejercicio1.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View(CategoriaProducto.Productos);
        }

        public IActionResult Create()
        {
            ViewBag.Categorias = CategoriaProducto.Categorias;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Producto producto)
        {
            try
            {
                CategoriaProducto.AgregarProducto(producto);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ViewBag.Categorias = CategoriaProducto.Categorias;
                return View(producto);
            }
        }

        public IActionResult Edit(int id)
        {
            var producto = CategoriaProducto.Productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
                return NotFound();

            ViewBag.Categorias = new SelectList(CategoriaProducto.Categorias, "Id", "Nombre");

            return PartialView("_EditarProductoPartial", producto);
        }

        [HttpPost]
        public IActionResult EditarPost(Producto p)
        {
            var existente = CategoriaProducto.Productos.FirstOrDefault(x => x.Id == p.Id);
            if (existente != null)
            {
                // Actualiza los valores del producto con los datos enviados
                existente.Nombre = p.Nombre;
                existente.Precio = p.Precio;
                existente.FechaAlta = p.FechaAlta;
                existente.CategoriaId = p.CategoriaId;
                existente.Categoria = CategoriaProducto.Categorias.FirstOrDefault(c => c.Id == p.CategoriaId);
            }

            return RedirectToAction("Index"); // Redirige al índice para ver los cambios
        }


        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var existente = CategoriaProducto.Productos.FirstOrDefault(x => x.Id == id);
            if (existente == null)
                return NotFound();

            CategoriaProducto.Productos.Remove(existente);
            return Ok();
        }
    }
}
