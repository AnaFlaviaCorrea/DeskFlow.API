using DeskFlow.API.Models.Entities;
using DeskFlow.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DeskFlow.API.Models.Requests;
using DeskFlow.API.Models.Enums;

namespace DeskFlow.API.Controllers;

[ApiController]
[Route("api/chamados")]
public class ChamadosController : ControllerBase
{
    private readonly IChamadoService _chamadoService;
    private readonly IInteracaoService _interacaoService;

    public ChamadosController(IChamadoService chamadoService, IInteracaoService interacaoService)
    {
        _chamadoService = chamadoService;
        _interacaoService = interacaoService;
    }

    [HttpGet]
    [HttpGet]
public async Task<ActionResult<List<Chamado>>> ObterTodos(
    [FromQuery] StatusChamado? status,
    [FromQuery] Prioridade? prioridade,
    [FromQuery] int? categoriaId
)
{
    var chamados = await _chamadoService.ObterTodosAsync(
        status,
        prioridade,
        categoriaId
    );

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
    [HttpPost("{id:int}/iniciar")]
    public async Task<IActionResult> IniciarAtendimento(int id)
    {
        await _chamadoService.IniciarAtendimentoAsync(id);

        return NoContent();
    }
    [HttpPost("{id:int}/encerrar")]
    public async Task<IActionResult> Encerrar(
    int id,
    EncerrarChamadoRequest request
)
    {
        await _chamadoService.EncerrarChamadoAsync(
            id,
            request.Solucao
        );

        return NoContent();
    }
    [HttpPost("{id:int}/interacoes")]
    public async Task<IActionResult> AdicionarInteracao(
        int id,
        AdicionarInteracaoRequest request
    )
    {
        var interacao = await _interacaoService.AdicionarAsync(
            id,
            request.Autor,
            request.Mensagem
        );

        return Ok(interacao);
    }
}
