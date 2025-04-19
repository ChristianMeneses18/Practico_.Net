using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio3.Core.Models;

namespace Ejercicio3.Core.Interfaces
{
    public interface IProductoService
    {
        IEnumerable<Producto> GetAllProductos();
        Producto GetProductoById(int id);
        void AddProducto(Producto producto);
        void UpdateProducto(Producto producto);
        void EliminarProducto(int id);
    }
}
