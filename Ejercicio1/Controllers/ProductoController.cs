using Ejercicio1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
