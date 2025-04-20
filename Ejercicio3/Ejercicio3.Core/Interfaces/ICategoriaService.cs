using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio3.Core.Models;

namespace Ejercicio3.Core.Interfaces
{
    public interface ICategoriaService
    {
        IEnumerable<Categoria> GetAllCategorias();
        Categoria? GetCategoriaById(int id);
        Categoria AddCategoria(Categoria categoria);
        Categoria UpdateCategoria(Categoria categoria);
        void EliminarCategoria(int id);
    }
}
