using System.Text.Json.Serialization;

namespace CadastroNF.Entities;

public class Fornecedor
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    [JsonIgnore] public ICollection<NotaFiscal> NotasFiscais { get; set; } = new List<NotaFiscal>();
}