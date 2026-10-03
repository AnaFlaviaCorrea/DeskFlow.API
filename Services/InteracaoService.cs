using DeskFlow.API.Models.Entities;
using DeskFlow.API.Models.Enums;
using DeskFlow.API.Repositories.Interfaces;
using DeskFlow.API.Services.Interfaces;

namespace DeskFlow.API.Services;

public class InteracaoService : IInteracaoService
{
    private readonly IInteracaoRepository _interacaoRepository;
    private readonly IChamadoRepository _chamadoRepository;

    public InteracaoService(
        IInteracaoRepository interacaoRepository,
        IChamadoRepository chamadoRepository
    )
    {
        _interacaoRepository = interacaoRepository;
        _chamadoRepository = chamadoRepository;
    }

    public async Task<Interacao> AdicionarAsync(
        int chamadoId,
        string autor,
        string mensagem
    )
    {
        var chamado =
            await _chamadoRepository.ObterPorIdAsync(chamadoId);

        if (chamado is null)
        {
            throw new KeyNotFoundException(
                "Chamado não encontrado."
            );
        }

        if (chamado.Status == StatusChamado.Fechado)
        {
            throw new InvalidOperationException(
                "Não é possível adicionar interação a um chamado fechado."
            );
        }

        if (string.IsNullOrWhiteSpace(autor))
        {
            throw new ArgumentException(
                "O autor da interação é obrigatório."
            );
        }

        if (string.IsNullOrWhiteSpace(mensagem))
        {
            throw new ArgumentException(
                "A mensagem da interação é obrigatória."
            );
        }

        var interacao = new Interacao
        {
            ChamadoId = chamadoId,
            Autor = autor,
            Mensagem = mensagem,
            DataRegistro = DateTime.Now
        };

        await _interacaoRepository.AdicionarAsync(interacao);

        return interacao;
    }
}