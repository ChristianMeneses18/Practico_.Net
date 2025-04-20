using AutoMapper;
using Ejercicio3.Core.DTOs;
using Ejercicio3.Core.Models;

namespace Ejercicio3.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
        }

    }
}
