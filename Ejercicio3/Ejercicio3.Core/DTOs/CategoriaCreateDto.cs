using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio3.Core.DTOs
{
    public class CategoriaCreateDto
    {
        [Required]
        public string Nombre { get; set; }
        public DateOnly FechaCreacion { get; set; }

    }
}
