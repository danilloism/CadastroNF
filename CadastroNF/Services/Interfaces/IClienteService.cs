using CadastroNF.DTOs;
using CadastroNF.Entities;

namespace CadastroNF.Services.Interfaces;

public interface IClienteService
{
    public Task<Cliente> Create(CreateClienteDTO dto);
    public Task<Cliente?> GetOne(int id);
    public Task<IEnumerable<Cliente>> GetMany();
}