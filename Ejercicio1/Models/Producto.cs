namespace Ejercicio1.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaAlta { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public Producto() { }

        public Producto(int id, string nombre, decimal precio, DateTime fechaAlta, int categoriaId, Categoria categoria)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
            FechaAlta = fechaAlta;
            CategoriaId = categoriaId;
            Categoria = categoria;
        }
    }
}
