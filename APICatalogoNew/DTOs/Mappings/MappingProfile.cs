using APICatalogoNew.Models;
using AutoMapper;

namespace APICatalogoNew.DTOs.Mappings;

public class MappingProfile :Profile
{
    public MappingProfile()
    {
        CreateMap<Produto, ProdutoDTO>().ReverseMap();
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
    }
}