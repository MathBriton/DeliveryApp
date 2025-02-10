using AutoMapper;
using Domain.Entities;
using Application.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IRepository<Produto> _repository;
    private readonly IMapper _mapper;

    public ProdutosController(IRepository<Produto> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // Retorna todos os produtos
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var produtos = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<ProdutoDTO>>(produtos));
    }

    // Retorna um produto pelo ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var produto = await _repository.GetByIdAsync(id);
        if (produto == null) return NotFound();
        return Ok(_mapper.Map<ProdutoDTO>(produto));
    }

    // Adiciona um novo produto
    [HttpPost]
    public async Task<IActionResult> Create(ProdutoDTO produtoDTO)
    {
        var produto = _mapper.Map<Produto>(produtoDTO);
        await _repository.AddAsync(produto);
        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    // Atualiza um produto existente
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProdutoDTO produtoDTO)
    {
        if (id != produtoDTO.Id) return BadRequest();
        var produto = _mapper.Map<Produto>(produtoDTO);
        await _repository.UpdateAsync(produto);
        return NoContent();
    }

    // Remove um produto pelo ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}