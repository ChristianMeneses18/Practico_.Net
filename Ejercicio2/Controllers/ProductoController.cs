using Ejercicio2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ejercicio2.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly AppDbContext _context;
        public ProductoController(AppDbContext context , ILogger<ProductoController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var productos = _context.Productos
                        .Include(p => p.Categoria)  
                        .ToList();
            return View(productos);
        }

        public IActionResult Create()
        {
            var categorias = _context.Categorias.ToList();
            ViewBag.Categorias = categorias;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Producto producto)
        {
            try
            {
                _context.Productos.Add(producto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                // Si ocurre un error al guardar, puedes agregarlo al ModelState o loguearlo
                _logger?.LogError(e, "Error al guardar el producto");
                ModelState.AddModelError("", "Error al guardar el producto.");
            }

            // Asegúrate de pasar las categorías al ViewBag nuevamente
            ViewBag.Categorias = _context.Categorias.ToList();

            // Devolver la vista, sin validar el modelo
            return View(producto);
        }



        public IActionResult Edit(int id)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
                return NotFound();

            ViewBag.Categorias = _context.Categorias
                                 .Select(c => new SelectListItem
                                 {
                                     Value = c.Id.ToString(),
                                     Text = c.Nombre
                                 }).ToList();
            return PartialView("_EditarProductoPartial", producto);
        }

        [HttpPost]
        public IActionResult EditarPost(Producto producto)
        {
            
                var existente = _context.Productos.FirstOrDefault(x => x.Id == producto.Id);
                if (existente != null)
                {
                    // Actualiza los valores del producto
                    existente.Nombre = producto.Nombre;
                    existente.Precio = producto.Precio;
                    existente.FechaAlta = producto.FechaAlta;
                    existente.CategoriaId = producto.CategoriaId;
                    existente.Categoria = _context.Categorias.FirstOrDefault(c => c.Id == producto.CategoriaId);

                    _context.SaveChanges(); // Guarda los cambios en la base de dato
                }

            return RedirectToAction("Index"); // Si hay un error, vuelve a mostrar la vista
        }


        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var existente = _context.Productos.FirstOrDefault(x => x.Id == id);
            if (existente == null)
                return NotFound();

            _context.Productos.Remove(existente); // Elimina el producto de la base de datos
            _context.SaveChanges(); // Guarda los cambios
            return Ok();
        }
    }
}
