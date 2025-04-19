using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio3.Core.Interfaces;
using Ejercicio3.Core.Models;
using Ejercicio3.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio3.Services.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IEnumerable<Categoria> GetAllCategorias()
        {
            return _categoriaRepository.GetAll();
        }

        public Categoria GetCategoriaById(int id)
        {
            return _categoriaRepository.GetById(id);
        }

        public void AddCategoria(Categoria categoria)
        {
             _categoriaRepository.Add(categoria);
        }

        public void Update(Categoria categoria)
        {
            _categoriaRepository.Update(categoria);
        }

        public void Delete(int id)
        {
            var categoria = _categoriaRepository.GetById(id);
            if (categoria == null)
            {
                throw new Exception("Categoría no encontrada.");
            }

            if (_categoriaRepository.TieneProductosAsociados(id))
            {
                throw new Exception("No se puede eliminar la categoría porque tiene productos asociados.");
            }
            _categoriaRepository.Delete(categoria);
        }
    }
}
