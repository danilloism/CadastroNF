using CadastroNF.Data.Repositories.Interfaces;
using CadastroNF.DTOs;
using CadastroNF.Entities;
using CadastroNF.Exceptions;
using CadastroNF.Services.Interfaces;
using CadastroNF.Utils;

namespace CadastroNF.Services;

public class NotaFiscalService : INotaFiscalService
{
    private readonly INotaFiscalRepository _repository;

    public NotaFiscalService(INotaFiscalRepository repository)
    {
        _repository = repository;
    }

    public async Task<NotaFiscal> Create(CreateNotaFiscalDTO dto)
    {
        try
        {
            return await _repository.Create(dto);
        }
        catch (RelacionamentoInvalidoException e)
        {
            throw new ServiceException(e.Message, StatusCodes.Status422UnprocessableEntity);
        }
        catch (RepositoryException e)
        {
            throw new ServiceException(e.Message);
        }
        catch (Exception)
        {
            throw new ServiceException(Constantes.MensagemErroDesconhecidoServidor);
        }
    }

    public async Task<NotaFiscal?> GetOne(int numeroNota)
    {
        try
        {
            return await _repository.GetOne(numeroNota);
        }
        catch (RepositoryException e)
        {
            throw new ServiceException(e.Message);
        }
        catch (Exception)
        {
            throw new ServiceException(Constantes.MensagemErroDesconhecidoServidor);
        }
    }

    public async Task<IEnumerable<NotaFiscal>> GetMany(GetNotasFiscaisQueryParamsDTO? queryParams)
    {
        try
        {
            return await _repository.GetMany(queryParams);
        }
        catch (RepositoryException e)
        {
            throw new ServiceException(e.Message);
        }
        catch (Exception)
        {
            throw new ServiceException(Constantes.MensagemErroDesconhecidoServidor);
        }
    }
}