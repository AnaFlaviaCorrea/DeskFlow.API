using DeskFlow.API.Data;
using DeskFlow.API.Models.Entities;
using DeskFlow.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using DeskFlow.API.Models.Enums;

namespace DeskFlow.API.Repositories;

public class ChamadoRepository : IChamadoRepository
{
    private readonly AppDbContext _context;

    public ChamadoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Chamado>> ObterTodosAsync(
        StatusChamado? status,
        Prioridade? prioridade,
        int? categoriaId
    )
    { var query = _context.Chamados
            .Include(c => c.Categoria)
            .Include(c => c.Interacoes)
            .AsQueryable();
    if (status.HasValue)
    {
        query = query.Where(c => c.Status == status.Value);
    }
    if (prioridade.HasValue)
    {
        query = query.Where(c => c.Prioridade == prioridade.Value);
    }
    if (categoriaId.HasValue)
    {
        query = query.Where(c => c.CategoriaId == categoriaId.Value);
    }
    return await query.ToListAsync();
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