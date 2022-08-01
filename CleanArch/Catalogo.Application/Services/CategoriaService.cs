using AutoMapper;
using Catalogo.Application.DTOs;
using Catalogo.Application.Interfaces;
using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;

namespace Catalogo.Application.Services;

public class CategoriaService : ICategoriaService
{
    private ICategoriaRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoriaService(ICategoriaRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<CategoriaDTO>> GetCategorias()
    {
        var categoriesEntities = await _categoryRepository.GetCategoriasAsync();
        return _mapper.Map<IEnumerable<CategoriaDTO>>(categoriesEntities);
    }

    public async Task<CategoriaDTO> GetById(int? id)
    {
        var categoryEntity = await _categoryRepository.GetByIdAsync(id);
        return _mapper.Map<CategoriaDTO>(categoryEntity);
    }

    public async Task Add(CategoriaDTO categoriaDto)
    {
        var categoryEntity = _mapper.Map<Categoria>(categoriaDto);
        await _categoryRepository.CreateAsync(categoryEntity);
    }

    public async Task Update(CategoriaDTO categoriaDto)
    {
        var categoryEntity = _mapper.Map<Categoria>(categoriaDto);
        await _categoryRepository.UpdateAsync(categoryEntity);
    }

    public async Task Remove(int? id)
    {
        var categoryEntity = _categoryRepository.GetByIdAsync(id).Result;
        await _categoryRepository.RemoveAsync(categoryEntity);
    }
}
