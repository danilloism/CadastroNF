using CadastroNF.Data.Context;
using CadastroNF.Data.Repositories.Interfaces;
using CadastroNF.DTOs;
using CadastroNF.Entities;
using Microsoft.EntityFrameworkCore;

namespace CadastroNF.Data.Repositories;

public class FornecedorRepository : IFornecedorRepository
{
    private readonly AppDbContext _context;

    public FornecedorRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<Fornecedor> Create(CreateFornecedorDTO dto)
    {
        var fornecedor = new Fornecedor { Nome = dto.Nome };

        _context.Fornecedores.Add(fornecedor);
        await _context.SaveChangesAsync();

        return fornecedor;
    }

    public async Task<Fornecedor?> GetOne(int id)
    {
        return await _context.Fornecedores.FindAsync(id);
    }

    public async Task<IEnumerable<Fornecedor>> GetMany()
    {
        return await _context.Fornecedores.AsNoTracking().ToListAsync();
    }
}