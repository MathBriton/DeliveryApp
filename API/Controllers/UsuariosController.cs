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
public class UsuariosController : ControllerBase
{
    private readonly IRepository<Usuario> _repository;
    private readonly IMapper _mapper;

    public UsuariosController(IRepository<Usuario> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // Retorna todos os usuários
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<UsuarioDTO>>(usuarios));
    }

    // Retorna um usuário pelo ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null) return NotFound();
        return Ok(_mapper.Map<UsuarioDTO>(usuario));
    }

    // Adiciona um novo usuário
    [HttpPost]
    public async Task<IActionResult> Create(UsuarioDTO usuarioDTO)
    {
        var usuario = _mapper.Map<Usuario>(usuarioDTO);
        await _repository.AddAsync(usuario);
        return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
    }

    // Atualiza um usuário existente
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UsuarioDTO usuarioDTO)
    {
        if (id != usuarioDTO.Id) return BadRequest();
        var usuario = _mapper.Map<Usuario>(usuarioDTO);
        await _repository.UpdateAsync(usuario);
        return NoContent();
    }

    // Remove um usuário pelo ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}