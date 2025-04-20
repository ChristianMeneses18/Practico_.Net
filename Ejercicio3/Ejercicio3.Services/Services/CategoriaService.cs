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

        public Categoria AddCategoria(Categoria categoria)
        {
            _categoriaRepository.Add(categoria);
            return categoria;
        }

        public Categoria UpdateCategoria(Categoria categoria)
        {
            var existente = _categoriaRepository.GetById(categoria.Id);
            if (existente == null)
                return null;

            existente.Nombre = categoria.Nombre;
            existente.FechaCreacion = categoria.FechaCreacion;

            _categoriaRepository.Update(existente);
            return existente;

        }

        public void EliminarCategoria(int id)
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
