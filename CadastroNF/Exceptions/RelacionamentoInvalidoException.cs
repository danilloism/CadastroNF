namespace CadastroNF.Exceptions;

public class RelacionamentoInvalidoException : RepositoryException
{
    public RelacionamentoInvalidoException(string? message) : base(message)
    {
    }
}