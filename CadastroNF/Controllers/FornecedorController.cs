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
public class FornecedorController : ControllerBase
{
    private readonly IFornecedorService _service;

    public FornecedorController(IFornecedorService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Fornecedor>))]
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Fornecedor))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErroControllerDTO))]
    public async Task<IActionResult> GetOne(int id)
    {
        Fornecedor? fornecedor;

        try
        {
            fornecedor = await _service.GetOne(id);
        }
        catch (ServiceException e)
        {
            return StatusCode(e.StatusCode, new ErroControllerDTO(e.Message));
        }

        if (fornecedor == null) return NotFound(new ErroControllerDTO(Constantes.MensagemRecursoNaoEncontrado));

        return Ok(fornecedor);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Fornecedor))]
    public async Task<IActionResult> Post([FromBody] CreateFornecedorDTO dto)
    {
        try
        {
            var fornecedor = await _service.Create(dto);

            return CreatedAtAction("GetOne", new { id = fornecedor.Id }, fornecedor);
        }
        catch (ServiceException e)
        {
            return StatusCode(e.StatusCode, new ErroControllerDTO(e.Message));
        }
    }

    [HttpGet("{fornecedorId:int}/notas-fiscais")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NotaFiscal>))]
    public IActionResult GetNotasFiscais(int fornecedorId)
    {
        return RedirectToAction("GetMany", "NotaFiscal", new { FornecedorId = fornecedorId });
    }
}