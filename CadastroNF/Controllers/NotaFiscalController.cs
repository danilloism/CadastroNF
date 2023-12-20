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
public class NotaFiscalController : ControllerBase
{
    private readonly INotaFiscalService _service;

    public NotaFiscalController(INotaFiscalService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NotaFiscal>))]
    public async Task<IActionResult> GetMany(
        [FromQuery] GetNotasFiscaisQueryParamsDTO? queryParams)
    {
        try
        {
            return Ok(await _service.GetMany(queryParams));
        }
        catch (ServiceException e)
        {
            return StatusCode(e.StatusCode, new ErroControllerDTO(e.Message));
        }
    }

    [HttpGet("{numeroNota:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotaFiscal))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErroControllerDTO))]
    public async Task<IActionResult> GetOne(int numeroNota)
    {
        NotaFiscal? notaFiscal;

        try
        {
            notaFiscal = await _service.GetOne(numeroNota);
        }
        catch (ServiceException e)
        {
            return StatusCode(e.StatusCode, new ErroControllerDTO(e.Message));
        }

        if (notaFiscal == null) return NotFound(new ErroControllerDTO(Constantes.MensagemRecursoNaoEncontrado));

        return Ok(notaFiscal);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NotaFiscal))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErroControllerDTO))]
    public async Task<IActionResult> Post([FromBody] CreateNotaFiscalDTO dto)
    {
        try
        {
            var notaFiscal = await _service.Create(dto);

            return CreatedAtAction("GetOne", new { numeroNota = notaFiscal.NumeroNota }, notaFiscal);
        }
        catch (ServiceException e)
        {
            return StatusCode(e.StatusCode, new ErroControllerDTO(e.Message));
        }
    }
}