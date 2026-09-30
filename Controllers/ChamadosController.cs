using DeskFlow.API.Models.Entities;
using DeskFlow.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlow.API.Controllers;

[ApiController]
[Route("api/chamados")]
public class ChamadosController : ControllerBase
{
    private readonly IChamadoService _chamadoService;

    public ChamadosController(IChamadoService chamadoService)
    {
        _chamadoService = chamadoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Chamado>>> ObterTodos()
    {
        var chamados = await _chamadoService.ObterTodosAsync();

        return Ok(chamados);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Chamado>> ObterPorId(int id)
    {
        var chamado = await _chamadoService.ObterPorIdAsync(id);

        return Ok(chamado);
    }

    [HttpPost]
    public async Task<ActionResult<Chamado>> Adicionar(Chamado chamado)
    {
        var chamadoCriado =
            await _chamadoService.AdicionarAsync(chamado);

        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = chamadoCriado.Id },
            chamadoCriado
        );
    }
}