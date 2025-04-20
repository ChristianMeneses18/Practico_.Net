using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio3.Core.Interfaces;
using Ejercicio3.Core.Models;
using Ejercicio3.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio3.Data.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _context;

        public ProductoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Producto> GetAll()
        {
            return _context.Productos.ToList();
        }

        public Producto? GetById(int id)
        {
            return _context.Productos.Include(p => p.Categoria).FirstOrDefault(p => p.Id == id);
        }

        public void Add(Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
        }

        public void Update(Producto producto)
        {
            _context.Productos.Update(producto);
            _context.SaveChanges();
        }

        public void Delete(Producto producto)
        {
            _context.Productos.Remove(producto);
            _context.SaveChanges();
        }
    }
}
