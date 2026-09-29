using DeskFlow.API.Models.Entities;
using DeskFlow.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlow.API.Controllers;

[ApiController]
[Route("api/categorias")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Categoria>>> ObterTodos()
    {
        var categorias = await _categoriaService.ObterTodosAsync();

        return Ok(categorias);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Categoria>> ObterPorId(int id)
    {
        var categoria = await _categoriaService.ObterPorIdAsync(id);

        if (categoria is null)
        {
            return NotFound();
        }

        return Ok(categoria);
    }

    [HttpPost]
    public async Task<ActionResult<Categoria>> Adicionar(Categoria categoria)
    {
        var categoriaCriada =
            await _categoriaService.AdicionarAsync(categoria);

        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = categoriaCriada.Id },
            categoriaCriada
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(
        int id,
        Categoria categoria
    )
    {
        await _categoriaService.AtualizarAsync(id, categoria);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id)
    {
        await _categoriaService.RemoverAsync(id);

        return NoContent();
    }
}