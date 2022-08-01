﻿using AutoMapper;
using Catalogo.Application.DTOs;
using Catalogo.Application.Interfaces;
using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;

namespace Catalogo.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _productRepository;
    private readonly IMapper _mapper;

    public ProdutoService(IProdutoRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<ProdutoDTO>> GetProdutos()
    {
        var productsEntity = await _productRepository.GetProdutosAsync();
        return _mapper.Map<IEnumerable<ProdutoDTO>>(productsEntity);
    }

    public async Task<ProdutoDTO> GetById(int? id)
    {
        var productEntity = await _productRepository.GetByIdAsync(id);
        return _mapper.Map<ProdutoDTO>(productEntity);
    }

    public async Task Add(ProdutoDTO produtoDto)
    {
        var productEntity = _mapper.Map<Produto>(produtoDto);
        await _productRepository.CreateAsync(productEntity);
    }    

    public async Task Update(ProdutoDTO produtoDto)
    {
        var productEntity = _mapper.Map<Produto>(produtoDto);
        await _productRepository.UpdateAsync(productEntity);
    }

    public async Task Remove(int? id)
    {
        var productEntity = _productRepository.GetByIdAsync(id).Result;
        await _productRepository.RemoveAsync(productEntity);
    }
}