using CadastroNF.DTOs;
using CadastroNF.Entities;

namespace CadastroNF.Services.Interfaces;

public interface IFornecedorService
{
    public Task<Fornecedor> Create(CreateFornecedorDTO dto);
    public Task<Fornecedor?> GetOne(int id);
    public Task<IEnumerable<Fornecedor>> GetMany();
}