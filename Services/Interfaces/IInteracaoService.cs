using DeskFlow.API.Models.Entities;

namespace DeskFlow.API.Services.Interfaces;

public interface IInteracaoService
{
    Task<Interacao> AdicionarAsync(
        int chamadoId,
        string autor,
        string mensagem
    );
}