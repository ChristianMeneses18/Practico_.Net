using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio3.Core.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }
        public DateTime FechaAlta { get; set; }

        
        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }

        public Producto() { }

        public Producto(int id, string nombre, decimal precio, DateTime fechaAlta, int categoriaId)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
            FechaAlta = fechaAlta;
            CategoriaId = categoriaId;
        }
    }
}
