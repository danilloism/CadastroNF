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
public class ClienteController : ControllerBase
{
    private readonly IClienteService _service;

    public ClienteController(IClienteService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Cliente>))]
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

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cliente))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErroControllerDTO))]
    public async Task<IActionResult> GetOne(int id)
    {
        Cliente? cliente;

        try
        {
            cliente = await _service.GetOne(id);
        }
        catch (ServiceException e)
        {
            return StatusCode(e.StatusCode, new ErroControllerDTO(e.Message));
        }

        if (cliente == null) return NotFound(new ErroControllerDTO(Constantes.MensagemRecursoNaoEncontrado));

        return Ok(cliente);
    }

    [HttpGet("{clienteId:int}/notas-fiscais")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NotaFiscal>))]
    public IActionResult GetNotasFiscais(int clienteId)
    {
        return RedirectToAction("GetMany", "NotaFiscal", new { ClienteId = clienteId });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Cliente))]
    public async Task<ActionResult> Post([FromBody] CreateClienteDTO dto)
    {
        try
        {
            var cliente = await _service.Create(dto);

            return CreatedAtAction("GetOne", new { id = cliente.Id }, cliente);
        }
        catch (ServiceException e)
        {
            return StatusCode(e.StatusCode, new ErroControllerDTO(e.Message));
        }
    }
}