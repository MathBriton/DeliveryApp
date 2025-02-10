using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class ProdutoRepository : IRepository<Produto>
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    // Retorna todos os produtos
    public async Task<IEnumerable<Produto>> GetAllAsync() =>
        await _context.Produtos.ToListAsync();

    // Retorna um produto pelo ID (permite retorno nulo)
    public async Task<Produto?> GetByIdAsync(int id) =>
        await _context.Produtos.FindAsync(id);

    // Adiciona um novo produto
    public async Task AddAsync(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
    }

    // Atualiza um produto existente
    public async Task UpdateAsync(Produto produto)
    {
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();
    }

    // Remove um produto pelo ID
    public async Task DeleteAsync(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto != null)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }
}