using CadastroNF.Data.Context;
using CadastroNF.Data.Repositories.Interfaces;
using CadastroNF.DTOs;
using CadastroNF.Entities;
using Microsoft.EntityFrameworkCore;

namespace CadastroNF.Data.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Produto> Create(CreateProdutoDTO dto)
    {
        var produto = new Produto { Nome = dto.Nome, ValorEmCentavos = dto.ValorEmCentavos };

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return produto;
    }

    public async Task<Produto?> GetOne(int id)
    {
        return await _context.Produtos.FindAsync(id);
    }

    public async Task<IEnumerable<Produto>> GetMany()
    {
        return await _context.Produtos.AsNoTracking().ToListAsync();
    }
}