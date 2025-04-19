using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio3.Core.Models;

namespace Ejercicio3.Core.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> GetAll();
        Categoria? GetById(int id);
        void Add(Categoria categoria);
        void Update(Categoria categoria);
        void Delete(Categoria categoria);
        bool TieneProductosAsociados(int categoriaId);
    }
}
