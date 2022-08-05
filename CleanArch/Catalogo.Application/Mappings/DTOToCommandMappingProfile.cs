using AutoMapper;
using Catalogo.Application.DTOs;
using Catalogo.Application.Produtos.Commands;
using Catalogo.Domain.Entities;

namespace Catalogo.Application.Mappings;

public class DTOToCommandMappingProfile : Profile
{
    public DTOToCommandMappingProfile()
    {
        CreateMap<CategoriaDTO, ProdutoCreateCommand>().ReverseMap();
        CreateMap<CategoriaDTO, ProdutoUpdateCommand>().ReverseMap();
    }
}

