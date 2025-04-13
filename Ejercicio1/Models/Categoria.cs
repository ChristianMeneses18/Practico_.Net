namespace Ejercicio1.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public DateOnly FechaCreacion { get; set; }

        public Categoria() { }

        public Categoria(int categoriaId, string nombreCategoria, DateOnly fechaCreacion)
        {
            Id = categoriaId;
            Nombre = nombreCategoria;
            FechaCreacion = fechaCreacion;
        }
    }
}
