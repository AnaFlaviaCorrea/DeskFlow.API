using DeskFlow.API.Data;
using DeskFlow.API.Models.Entities;
using DeskFlow.API.Repositories.Interfaces;

namespace DeskFlow.API.Repositories;

public class InteracaoRepository : IInteracaoRepository
{
    private readonly AppDbContext _context;

    public InteracaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Interacao interacao)
    {
        await _context.Interacoes.AddAsync(interacao);

        await _context.SaveChangesAsync();
    }
}