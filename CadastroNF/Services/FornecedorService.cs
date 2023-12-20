using CadastroNF.Data.Repositories.Interfaces;
using CadastroNF.DTOs;
using CadastroNF.Entities;
using CadastroNF.Exceptions;
using CadastroNF.Services.Interfaces;
using CadastroNF.Utils;

namespace CadastroNF.Services;

public class FornecedorService : IFornecedorService
{
    private readonly IFornecedorRepository _repository;

    public FornecedorService(IFornecedorRepository repository)
    {
        _repository = repository;
    }

    public async Task<Fornecedor> Create(CreateFornecedorDTO dto)
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

    public async Task<Fornecedor?> GetOne(int id)
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

    public async Task<IEnumerable<Fornecedor>> GetMany()
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