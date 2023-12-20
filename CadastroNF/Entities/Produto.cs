using System.Text.Json.Serialization;

namespace CadastroNF.Entities;

public class Produto
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public int ValorEmCentavos { get; set; }

    [JsonIgnore] public ICollection<NotaFiscal> NotasFiscais { get; set; } = new List<NotaFiscal>();
}