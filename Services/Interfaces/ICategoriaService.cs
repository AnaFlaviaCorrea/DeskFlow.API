using DeskFlow.API.Models.Entities;

namespace DeskFlow.API.Services.Interfaces;

public interface ICategoriaService
{
    Task<List<Categoria>> ObterTodosAsync();

    Task<Categoria?> ObterPorIdAsync(int id);

    Task<Categoria> AdicionarAsync(Categoria categoria);

    Task AtualizarAsync(int id, Categoria categoria);

    Task RemoverAsync(int id);

  
}