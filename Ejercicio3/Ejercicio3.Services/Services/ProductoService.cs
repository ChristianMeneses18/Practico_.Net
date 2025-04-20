using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio3.Core.Interfaces;
using Ejercicio3.Core.Models;

namespace Ejercicio3.Services.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public IEnumerable<Producto> GetAllProductos()
        {
            return _productoRepository.GetAll();
        }

        public Producto GetProductoById(int id)
        {
            return _productoRepository.GetById(id);
        }

        public Producto AddProducto(Producto producto)
        {
            _productoRepository.Add(producto);
            return producto;
        }

        public Producto UpdateProducto(Producto producto)
        {
            var existente = _productoRepository.GetById(producto.Id);
            if (existente == null)
                return null;

            existente.Nombre = producto.Nombre;
            existente.Precio = producto.Precio;
            existente.FechaAlta = producto.FechaAlta;
            existente.CategoriaId = producto.CategoriaId;

            _productoRepository.Update(existente);
            return existente;
        }


        public void EliminarProducto(int id)
        {
            var producto = _productoRepository.GetById(id);
            if (producto != null)
            {
                _productoRepository.Delete(producto);
            }
        }
    }
}
