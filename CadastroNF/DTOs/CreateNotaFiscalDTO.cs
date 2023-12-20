namespace CadastroNF.DTOs;

public record CreateNotaFiscalDTO(
    int IdCliente,
    int IdFornecedor,
    IEnumerable<int> IdsProdutos);