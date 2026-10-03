using DeskFlow.API.Models.Entities;

namespace DeskFlow.API.Repositories.Interfaces;

public interface IInteracaoRepository
{
    Task AdicionarAsync(Interacao interacao);
}