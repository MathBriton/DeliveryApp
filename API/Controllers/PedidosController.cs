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
public class PedidosController : ControllerBase
{
    private readonly IRepository<Pedido> _repository;
    private readonly IMapper _mapper;

    public PedidosController(IRepository<Pedido> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pedidos = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<PedidoDTO>>(pedidos));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var pedido = await _repository.GetByIdAsync(id);
        if (pedido == null) return NotFound();
        return Ok(_mapper.Map<PedidoDTO>(pedido));
    }

    [HttpPost]
    public async Task<IActionResult> Create(PedidoDTO pedidoDTO)
    {
        var pedido = _mapper.Map<Pedido>(pedidoDTO);
        await _repository.AddAsync(pedido);
        return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, pedido);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PedidoDTO pedidoDTO)
    {
        if (id != pedidoDTO.Id) return BadRequest();
        var pedido = _mapper.Map<Pedido>(pedidoDTO);
        await _repository.UpdateAsync(pedido);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}