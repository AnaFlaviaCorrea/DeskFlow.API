using DeskFlow.API.Data;
using DeskFlow.API.Models.Entities;
using DeskFlow.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeskFlow.API.Repositories;

public class ChamadoRepository : IChamadoRepository
{
    private readonly AppDbContext _context;

    public ChamadoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Chamado>> ObterTodosAsync()
    {
        return await _context.Chamados
            .Include(c => c.Categoria)
            .Include(c => c.Interacoes)
            .ToListAsync();
    }

    public async Task<Chamado?> ObterPorIdAsync(int id)
    {
        return await _context.Chamados
            .Include(c => c.Categoria)
            .Include(c => c.Interacoes)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AdicionarAsync(Chamado chamado)
    {
        await _context.Chamados.AddAsync(chamado);

        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Chamado chamado)
    {
        _context.Chamados.Update(chamado);

        await _context.SaveChangesAsync();
    }

    public async Task<bool> CategoriaExisteAsync(int categoriaId)
    {
        return await _context.Categorias
            .AnyAsync(c => c.Id == categoriaId);
    }
}