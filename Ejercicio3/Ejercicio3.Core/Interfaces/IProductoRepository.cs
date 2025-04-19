using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio3.Core.Models;

namespace Ejercicio3.Core.Interfaces
{
    public interface IProductoRepository{
        IEnumerable<Producto?> GetAll();
        Producto? GetById(int id);
        void Add(Producto producto);
        void Update(Producto producto);
        void Delete(Producto producto);
    }
}
