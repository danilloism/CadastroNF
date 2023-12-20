using CadastroNF.DTOs;
using CadastroNF.Entities;

namespace CadastroNF.Data.Repositories.Interfaces;

public interface INotaFiscalRepository
{
    public Task<NotaFiscal> Create(CreateNotaFiscalDTO dto);
    public Task<NotaFiscal?> GetOne(int numeroNota);
    public Task<IEnumerable<NotaFiscal>> GetMany(GetNotasFiscaisQueryParamsDTO? queryParams);
}