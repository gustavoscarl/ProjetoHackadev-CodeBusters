namespace PayWiseBackend.Domain.Context.ContextResults;

public interface IContextResult<T>
{
    bool Sucesso { get; }
    string MensagemDeErro { get; }
    T Data { get; }
}
