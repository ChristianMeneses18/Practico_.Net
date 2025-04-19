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
        void AddCategoria(Categoria categoria);
        void UpdateCategoria(Categoria categoria);
        void EliminarCategoria(int id);
    }
}
