using DeskFlow.API.Models.Entities;
using DeskFlow.API.Models.Enums;
using DeskFlow.API.Repositories.Interfaces;
using DeskFlow.API.Services.Interfaces;

namespace DeskFlow.API.Services;

public class ChamadoService : IChamadoService
{
    private readonly IChamadoRepository _chamadoRepository;

    public ChamadoService(IChamadoRepository chamadoRepository)
    {
        _chamadoRepository = chamadoRepository;
    }

   public async Task<List<Chamado>> ObterTodosAsync(
    StatusChamado? status,
    Prioridade? prioridade,
    int? categoriaId
)
{
    return await _chamadoRepository.ObterTodosAsync(
        status,
        prioridade,
        categoriaId
    );
}

    public async Task<Chamado> ObterPorIdAsync(int id)
    {
        var chamado = await _chamadoRepository.ObterPorIdAsync(id);

        if (chamado is null)
        {
            throw new KeyNotFoundException("Chamado não encontrado.");
        }

        return chamado;
    }

    public async Task<Chamado> AdicionarAsync(Chamado chamado)
    {
        if (string.IsNullOrWhiteSpace(chamado.Titulo))
        {
            throw new ArgumentException(
                "O título do chamado é obrigatório."
            );
        }

        if (string.IsNullOrWhiteSpace(chamado.Descricao))
        {
            throw new ArgumentException(
                "A descrição do chamado é obrigatória."
            );
        }

        if (string.IsNullOrWhiteSpace(chamado.SolicitanteNome))
        {
            throw new ArgumentException(
                "O nome do solicitante é obrigatório."
            );
        }

        var categoriaExiste =
            await _chamadoRepository.CategoriaExisteAsync(
                chamado.CategoriaId
            );

        if (!categoriaExiste)
        {
            throw new ArgumentException(
                "A categoria informada não existe."
            );
        }

        chamado.Status = StatusChamado.Aberto;
        chamado.DataAbertura = DateTime.Now;
        chamado.DataFechamento = null;
        chamado.Solucao = null;

        await _chamadoRepository.AdicionarAsync(chamado);

        return chamado;
    }
    public async Task IniciarAtendimentoAsync(int id)
{
    var chamado = await _chamadoRepository.ObterPorIdAsync(id);

    if (chamado is null)
    {
        throw new KeyNotFoundException("Chamado não encontrado.");
    }

    if (chamado.Status != StatusChamado.Aberto)
    {
        throw new InvalidOperationException(
            "Somente chamados com status Aberto podem ser iniciados."
        );
    }

    chamado.Status = StatusChamado.EmAndamento;

    await _chamadoRepository.AtualizarAsync(chamado);
}
public async Task EncerrarChamadoAsync(int id, string solucao)
{
    var chamado = await _chamadoRepository.ObterPorIdAsync(id);

    if (chamado is null)
    {
        throw new KeyNotFoundException("Chamado não encontrado.");
    }

    if (chamado.Status != StatusChamado.EmAndamento)
    {
        throw new InvalidOperationException(
            "Somente chamados em andamento podem ser encerrados."
        );
    }

    if (string.IsNullOrWhiteSpace(solucao))
    {
        throw new ArgumentException(
            "A solução do chamado é obrigatória."
        );
    }

    chamado.Status = StatusChamado.Fechado;
    chamado.Solucao = solucao;
    chamado.DataFechamento = DateTime.Now;

    await _chamadoRepository.AtualizarAsync(chamado);
}
}
