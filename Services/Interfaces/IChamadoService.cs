using DeskFlow.API.Models.Entities;
using DeskFlow.API.Models.Enums;

namespace DeskFlow.API.Services.Interfaces;

public interface IChamadoService
{
   Task<List<Chamado>> ObterTodosAsync(
    StatusChamado? status,
    Prioridade? prioridade,
    int? categoriaId
);

    Task<Chamado> ObterPorIdAsync(int id);

    Task<Chamado> AdicionarAsync(Chamado chamado);

    Task IniciarAtendimentoAsync(int id);

    Task EncerrarChamadoAsync(int id, string solucao);
}