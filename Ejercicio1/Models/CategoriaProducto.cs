namespace Ejercicio1.Models
{
    public class CategoriaProducto
    {
        public static List<Categoria> Categorias = new();
        public static List<Producto> Productos = new();

        public static void AgregarCategoria(Categoria categoria)
        {
            categoria.Id = Categorias.Count + 1;
            Categorias.Add(categoria);
        }

        public static void AgregarProducto(Producto producto)
        {
            var categoria = Categorias.FirstOrDefault(c => c.Id == producto.CategoriaId);
            if (categoria == null)
                throw new Exception("La categoría no existe.");

            producto.Id = Productos.Count + 1;
            producto.Categoria = categoria;
            Productos.Add(producto);
        }
    }
}
