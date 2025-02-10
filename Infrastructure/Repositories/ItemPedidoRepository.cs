using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class ItemPedidoRepository : IRepository<ItemPedido>
{
    private readonly AppDbContext _context;

    public ItemPedidoRepository(AppDbContext context)
    {
        _context = context;
    }

    // Retorna todos os itens de pedido
    public async Task<IEnumerable<ItemPedido>> GetAllAsync() =>
        await _context.ItensPedido
            .Include(i => i.Pedido)    // Inclui o pedido relacionado
            .Include(i => i.Produto)   // Inclui o produto relacionado
            .ToListAsync();

    // Retorna um item de pedido pelo ID (permite retorno nulo)
    public async Task<ItemPedido?> GetByIdAsync(int id) =>
        await _context.ItensPedido
            .Include(i => i.Pedido)    // Inclui o pedido relacionado
            .Include(i => i.Produto)   // Inclui o produto relacionado
            .FirstOrDefaultAsync(i => i.Id == id);

    // Adiciona um novo item de pedido
    public async Task AddAsync(ItemPedido itemPedido)
    {
        _context.ItensPedido.Add(itemPedido);
        await _context.SaveChangesAsync();
    }

    // Atualiza um item de pedido existente
    public async Task UpdateAsync(ItemPedido itemPedido)
    {
        _context.ItensPedido.Update(itemPedido);
        await _context.SaveChangesAsync();
    }

    // Remove um item de pedido pelo ID
    public async Task DeleteAsync(int id)
    {
        var itemPedido = await _context.ItensPedido.FindAsync(id);
        if (itemPedido != null)
        {
            _context.ItensPedido.Remove(itemPedido);
            await _context.SaveChangesAsync();
        }
    }
}