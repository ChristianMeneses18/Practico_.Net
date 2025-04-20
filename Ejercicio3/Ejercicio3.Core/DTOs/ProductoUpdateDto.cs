using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio3.Core.DTOs
{
    public class ProductoUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Precio { get; set; }

        [Required]
        public DateTime FechaAlta { get; set; }

        [Required]
        public int CategoriaId { get; set; }
    }
}
