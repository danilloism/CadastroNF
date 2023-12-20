using CadastroNF.DTOs;
using CadastroNF.Entities;

namespace CadastroNF.Services.Interfaces;

public interface INotaFiscalService
{
    public Task<NotaFiscal> Create(CreateNotaFiscalDTO dto);
    public Task<NotaFiscal?> GetOne(int numeroNota);
    public Task<IEnumerable<NotaFiscal>> GetMany(GetNotasFiscaisQueryParamsDTO? queryParams);
}