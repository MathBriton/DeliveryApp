using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class PedidoRepository : IRepository<Pedido>
{
    private readonly AppDbContext _context;

    public PedidoRepository(AppDbContext context)
    {
        _context = context;
    }

    // Retorna todos os pedidos
    public async Task<IEnumerable<Pedido>> GetAllAsync() =>
        await _context.Pedidos
            .Include(p => p.Itens) // Inclui os itens do pedido
            .ToListAsync();

    // Retorna um pedido pelo ID (permite retorno nulo)
    public async Task<Pedido?> GetByIdAsync(int id) =>
        await _context.Pedidos
            .Include(p => p.Itens) // Inclui os itens do pedido
            .FirstOrDefaultAsync(p => p.Id == id);

    // Adiciona um novo pedido
    public async Task AddAsync(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
    }

    // Atualiza um pedido existente
    public async Task UpdateAsync(Pedido pedido)
    {
        _context.Pedidos.Update(pedido);
        await _context.SaveChangesAsync();
    }

    // Remove um pedido pelo ID
    public async Task DeleteAsync(int id)
    {
        var pedido = await _context.Pedidos.FindAsync(id);
        if (pedido != null)
        {
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
        }
    }
}