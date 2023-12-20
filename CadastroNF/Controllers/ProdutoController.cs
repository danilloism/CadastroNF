using CadastroNF.DTOs;
using CadastroNF.Entities;
using CadastroNF.Exceptions;
using CadastroNF.Services.Interfaces;
using CadastroNF.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CadastroNF.Controllers;

[Route("[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErroControllerDTO))]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _service;

    public ProdutoController(IProdutoService service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Produto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErroControllerDTO))]
    public async Task<IActionResult> GetOne(int id)
    {
        Produto? produto;

        try
        {
            produto = await _service.GetOne(id);
        }
        catch (ServiceException e)
        {
            return StatusCode(e.StatusCode, new ErroControllerDTO(e.Message));
        }

        if (produto == null) return NotFound(new ErroControllerDTO(Constantes.MensagemRecursoNaoEncontrado));

        return Ok(produto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Produto>))]
    public async Task<IActionResult> GetMany()
    {
        try
        {
            return Ok(await _service.GetMany());
        }
        catch (ServiceException e)
        {
            return StatusCode(e.StatusCode, new ErroControllerDTO(e.Message));
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Produto))]
    public async Task<IActionResult> Post([FromBody] CreateProdutoDTO dto)
    {
        try
        {
            var produto = await _service.Create(dto);

            return CreatedAtAction("GetOne", new { id = produto.Id }, produto);
        }
        catch (ServiceException e)
        {
            return StatusCode(e.StatusCode, new ErroControllerDTO(e.Message));
        }
    }
}