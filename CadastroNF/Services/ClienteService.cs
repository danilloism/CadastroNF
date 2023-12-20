using CadastroNF.Data.Repositories.Interfaces;
using CadastroNF.DTOs;
using CadastroNF.Entities;
using CadastroNF.Exceptions;
using CadastroNF.Services.Interfaces;
using CadastroNF.Utils;

namespace CadastroNF.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Cliente> Create(CreateClienteDTO dto)
    {
        try
        {
            return await _repository.Create(dto);
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

    public async Task<Cliente?> GetOne(int id)
    {
        try
        {
            return await _repository.GetOne(id);
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

    public async Task<IEnumerable<Cliente>> GetMany()
    {
        try
        {
            return await _repository.GetMany();
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