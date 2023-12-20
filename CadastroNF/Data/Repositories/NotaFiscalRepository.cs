using CadastroNF.Data.Context;
using CadastroNF.Data.Repositories.Interfaces;
using CadastroNF.DTOs;
using CadastroNF.Entities;
using CadastroNF.Exceptions;
using CadastroNF.Utils;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CadastroNF.Data.Repositories;

public class NotaFiscalRepository : INotaFiscalRepository
{
    private readonly AppDbContext _context;

    public NotaFiscalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<NotaFiscal> Create(CreateNotaFiscalDTO dto)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var notaFiscal = new NotaFiscal { ClienteId = dto.IdCliente, FornecedorId = dto.IdFornecedor };

            _context.NotasFiscais.Add(notaFiscal);
            await _context.SaveChangesAsync();

            var produtos = dto.IdsProdutos.Select(id =>
            {
                var produto = new Produto { Id = id };
                _context.Produtos.Attach(produto);
                return produto;
            }).ToList();

            notaFiscal.Produtos = produtos;
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return notaFiscal;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();

            var exception = e.InnerException switch
            {
                PostgresException { SqlState: PostgresErrorCodes.ForeignKeyViolation, ConstraintName: "fk_produto" } =>
                    new RelacionamentoInvalidoException("Produto informado não encontrado."),
                PostgresException { SqlState: PostgresErrorCodes.ForeignKeyViolation, ConstraintName: "fk_cliente" } =>
                    new RelacionamentoInvalidoException("Cliente informado não encontrado."),
                PostgresException { SqlState: PostgresErrorCodes.ForeignKeyViolation, ConstraintName: "fk_fornecedor" }
                    => new RelacionamentoInvalidoException("Fornecedor informado não encontrado."),
                _ => new RepositoryException(Constantes.MensagemErroDesconhecidoServidor)
            };

            throw exception;
        }
    }

    public async Task<NotaFiscal?> GetOne(int numeroNota)
    {
        return await _context.NotasFiscais.FindAsync(numeroNota);
    }

    public async Task<IEnumerable<NotaFiscal>> GetMany(GetNotasFiscaisQueryParamsDTO? queryParams)
    {
        var query = _context
            .NotasFiscais
            .AsNoTracking();

        if (queryParams?.ClienteId != null)
            query = query.Where(
                nf => nf.ClienteId == queryParams.ClienteId);

        if (queryParams?.FornecedorId != null)
            query = query.Where(
                nf => nf.FornecedorId == queryParams.FornecedorId);

        return await query
            .Include(nf => nf.Produtos)
            .Include(nf => nf.Cliente)
            .Include(nf => nf.Fornecedor)
            .ToListAsync();
    }
}