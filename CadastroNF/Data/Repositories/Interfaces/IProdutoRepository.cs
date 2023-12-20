using CadastroNF.DTOs;
using CadastroNF.Entities;

namespace CadastroNF.Data.Repositories.Interfaces;

public interface IProdutoRepository
{
    public Task<Produto> Create(CreateProdutoDTO dto);
    public Task<Produto?> GetOne(int id);
    public Task<IEnumerable<Produto>> GetMany();
}