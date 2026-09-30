using DeskFlow.API.Models.Entities;

namespace DeskFlow.API.Services.Interfaces;

public interface IChamadoService
{
    Task<List<Chamado>> ObterTodosAsync();

    Task<Chamado> ObterPorIdAsync(int id);

    Task<Chamado> AdicionarAsync(Chamado chamado);
}