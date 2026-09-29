using DeskFlow.API.Models.Entities;
using DeskFlow.API.Repositories.Interfaces;
using DeskFlow.API.Services.Interfaces;


namespace DeskFlow.API.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<List<Categoria>> ObterTodosAsync()
    {
        return await _categoriaRepository.ObterTodosAsync();
    }

    public async Task<Categoria?> ObterPorIdAsync(int id)
    {
        return await _categoriaRepository.ObterPorIdAsync(id);
    }

    public async Task<Categoria> AdicionarAsync(Categoria categoria)
    {
        if (string.IsNullOrWhiteSpace(categoria.Nome))
        {
            throw new ArgumentException("O nome da categoria é obrigatório.");
        }

        await _categoriaRepository.AdicionarAsync(categoria);

        return categoria;
    }

    public async Task AtualizarAsync(int id, Categoria categoria)
    {
        if (string.IsNullOrWhiteSpace(categoria.Nome))
        {
            throw new ArgumentException("O nome da categoria é obrigatório.");
        }

        var categoriaExistente =
            await _categoriaRepository.ObterPorIdAsync(id);

        if (categoriaExistente is null)
        {
            throw new KeyNotFoundException("Categoria não encontrada.");
        }

        categoriaExistente.Nome = categoria.Nome;

        await _categoriaRepository.AtualizarAsync(categoriaExistente);
    }

    public async Task RemoverAsync(int id)
    {
        var categoria =
            await _categoriaRepository.ObterPorIdAsync(id);

        if (categoria is null)
        {
            throw new KeyNotFoundException("Categoria não encontrada.");
        }

        var possuiChamados =
            await _categoriaRepository.PossuiChamadosAsync(id);

        if (possuiChamados)
        {
            throw new InvalidOperationException(
                "Não é possível remover uma categoria que possui chamados associados."
            );
        }

        await _categoriaRepository.RemoverAsync(categoria);
    }
}
