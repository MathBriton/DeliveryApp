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
public class ItensPedidoController : ControllerBase
{
    private readonly IRepository<ItemPedido> _repository;
    private readonly IMapper _mapper;

    public ItensPedidoController(IRepository<ItemPedido> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // Retorna todos os itens de pedido
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var itensPedido = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<ItemPedidoDTO>>(itensPedido));
    }

    // Retorna um item de pedido pelo ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var itemPedido = await _repository.GetByIdAsync(id);
        if (itemPedido == null) return NotFound();
        return Ok(_mapper.Map<ItemPedidoDTO>(itemPedido));
    }

    // Adiciona um novo item de pedido
    [HttpPost]
    public async Task<IActionResult> Create(ItemPedidoDTO itemPedidoDTO)
    {
        var itemPedido = _mapper.Map<ItemPedido>(itemPedidoDTO);
        await _repository.AddAsync(itemPedido);
        return CreatedAtAction(nameof(GetById), new { id = itemPedido.Id }, itemPedido);
    }

    // Atualiza um item de pedido existente
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ItemPedidoDTO itemPedidoDTO)
    {
        if (id != itemPedidoDTO.Id) return BadRequest();
        var itemPedido = _mapper.Map<ItemPedido>(itemPedidoDTO);
        await _repository.UpdateAsync(itemPedido);
        return NoContent();
    }

    // Remove um item de pedido pelo ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}