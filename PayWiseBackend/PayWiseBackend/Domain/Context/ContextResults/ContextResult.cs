namespace PayWiseBackend.Domain.Context.ContextResults;

public class ContextResult<T> : IContextResult<T>
{
    public T Data { get; private set; }
    public bool Sucesso { get; private set; }
    public string MensagemDeErro { get; private set; }

    public static ContextResult<T> ResultadoSucesso(T data) => new ContextResult<T> { Sucesso = true, Data = data };
    public static ContextResult<T> ResultadoFalha(string mensagemDeErro) => new ContextResult<T> { Sucesso = false, MensagemDeErro = mensagemDeErro };

}
