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

    public async Task<List<Chamado>> ObterTodosAsync()
    {
        return await _chamadoRepository.ObterTodosAsync();
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
}