using CadastroNF.DTOs;
using CadastroNF.Entities;

namespace CadastroNF.Services.Interfaces;

public interface IProdutoService
{
    public Task<Produto> Create(CreateProdutoDTO dto);
    public Task<Produto?> GetOne(int id);
    public Task<IEnumerable<Produto>> GetMany();
}