namespace Fictichos.Constructora.Model;

public class HTTPResult<T>
{
    public int Code { get; set; }
    public T? Value { get; set; }
}