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

        public void Add(Producto producto)
        {
            _productoRepository.Add(producto);
        }

        public void Update(Producto producto)
        {
            _productoRepository.Update(producto);
        }

        public void Delete(int id)
        {
            var producto = _productoRepository.GetById(id);
            if (producto != null)
            {
                _productoRepository.Delete(id)
            }
        }
    }
}
