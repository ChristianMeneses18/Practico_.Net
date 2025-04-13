using Ejercicio1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            if (!ModelState.IsValid)
                return View(categoria);

            CategoriaProducto.AgregarCategoria(categoria);
            return RedirectToAction("Index");
        }
    }
}
