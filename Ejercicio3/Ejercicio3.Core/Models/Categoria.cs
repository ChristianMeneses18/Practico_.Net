using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ejercicio3.Core.Models
{
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
