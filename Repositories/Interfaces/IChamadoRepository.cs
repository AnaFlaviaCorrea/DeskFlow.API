using DeskFlow.API.Models.Entities;
using DeskFlow.API.Models.Enums;

namespace DeskFlow.API.Repositories.Interfaces;

public interface IChamadoRepository
{
    Task<Chamado?> ObterPorIdAsync(int id);

    Task<List<Chamado>> ObterTodosAsync();

    Task AdicionarAsync(Chamado chamado);

    Task AtualizarAsync(Chamado chamado);

    Task<bool> CategoriaExisteAsync(int categoriaId);
}