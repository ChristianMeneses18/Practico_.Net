using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio3.Core.Interfaces;
using Ejercicio3.Core.Models;
using Ejercicio3.Data.Context;

namespace Ejercicio3.Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> GetAll()
        {
            return _context.Categorias.ToList();
        }

        public Categoria? GetById(int id)
        {
            return _context.Categorias.Find(id);
        }

        public void Add(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
        }

        public void Update(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            _context.SaveChanges();
        }

        public void Delete(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
        }

        public bool TieneProductosAsociados(int categoriaId)
        {
            return _context.Productos.Any(p => p.CategoriaId == categoriaId);
        }
    }
}
