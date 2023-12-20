using CadastroNF.Data.Repositories.Interfaces;
using CadastroNF.DTOs;
using CadastroNF.Entities;
using CadastroNF.Exceptions;
using CadastroNF.Services.Interfaces;
using CadastroNF.Utils;

namespace CadastroNF.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repository;

    public ProdutoService(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Produto> Create(CreateProdutoDTO dto)
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

    public async Task<Produto?> GetOne(int id)
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

    public async Task<IEnumerable<Produto>> GetMany()
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