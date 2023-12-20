using System.Text.Json.Serialization;

namespace CadastroNF.Entities;

public class NotaFiscal
{
    public int NumeroNota { get; set; }

    [JsonIgnore] public int ClienteId { get; set; }

    [JsonIgnore] public int FornecedorId { get; set; }

    public Cliente Cliente { get; set; } = null!;

    public Fornecedor Fornecedor { get; set; } = null!;

    public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

    public int ValorTotalEmCentavos => Produtos.Sum(produto => produto.ValorEmCentavos);
}