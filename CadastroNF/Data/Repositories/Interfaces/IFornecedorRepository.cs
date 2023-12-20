using CadastroNF.DTOs;
using CadastroNF.Entities;

namespace CadastroNF.Data.Repositories.Interfaces;

public interface IFornecedorRepository
{
    public Task<Fornecedor> Create(CreateFornecedorDTO dto);
    public Task<Fornecedor?> GetOne(int id);
    public Task<IEnumerable<Fornecedor>> GetMany();
}