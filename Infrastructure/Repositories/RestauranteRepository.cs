using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class RestauranteRepository : IRepository<Restaurante>
{
    private readonly AppDbContext _context;

    public RestauranteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Restaurante>> GetAllAsync() =>
        await _context.Restaurantes.ToListAsync();

    public async Task<Restaurante?> GetByIdAsync(int id) =>
        await _context.Restaurantes.FindAsync(id);

    public async Task AddAsync(Restaurante restaurante)
    {
        _context.Restaurantes.Add(restaurante);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Restaurante restaurante)
    {
        _context.Restaurantes.Update(restaurante);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var restaurante = await _context.Restaurantes.FindAsync(id);
        if (restaurante != null)
        {
            _context.Restaurantes.Remove(restaurante);
            await _context.SaveChangesAsync();
        }
    }
}