using CadastroNF.Data.Context;
using CadastroNF.Data.Repositories.Interfaces;
using CadastroNF.DTOs;
using CadastroNF.Entities;
using Microsoft.EntityFrameworkCore;

namespace CadastroNF.Data.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<Cliente> Create(CreateClienteDTO dto)
    {
        var cliente = new Cliente { Nome = dto.Nome };

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        return cliente;
    }


    public async Task<Cliente?> GetOne(int id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<IEnumerable<Cliente>> GetMany()
    {
        return await _context.Clientes.AsNoTracking().ToListAsync();
    }
}