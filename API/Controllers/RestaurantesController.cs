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
public class RestaurantesController : ControllerBase
{
    private readonly IRepository<Restaurante> _repository;
    private readonly IMapper _mapper;

    public RestaurantesController(IRepository<Restaurante> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // Retorna todos os restaurantes
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurantes = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<RestauranteDTO>>(restaurantes));
    }

    // Retorna um restaurante pelo ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var restaurante = await _repository.GetByIdAsync(id);
        if (restaurante == null) return NotFound();
        return Ok(_mapper.Map<RestauranteDTO>(restaurante));
    }

    // Adiciona um novo restaurante
    [HttpPost]
    public async Task<IActionResult> Create(RestauranteDTO restauranteDTO)
    {
        var restaurante = _mapper.Map<Restaurante>(restauranteDTO);
        await _repository.AddAsync(restaurante);
        return CreatedAtAction(nameof(GetById), new { id = restaurante.Id }, restaurante);
    }

    // Atualiza um restaurante existente
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, RestauranteDTO restauranteDTO)
    {
        if (id != restauranteDTO.Id) return BadRequest();
        var restaurante = _mapper.Map<Restaurante>(restauranteDTO);
        await _repository.UpdateAsync(restaurante);
        return NoContent();
    }

    // Remove um restaurante pelo ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}