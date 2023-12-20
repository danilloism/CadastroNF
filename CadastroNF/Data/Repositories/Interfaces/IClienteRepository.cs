using CadastroNF.DTOs;
using CadastroNF.Entities;

namespace CadastroNF.Data.Repositories.Interfaces;

public interface IClienteRepository
{
    public Task<Cliente> Create(CreateClienteDTO dto);
    public Task<Cliente?> GetOne(int id);
    public Task<IEnumerable<Cliente>> GetMany();
}