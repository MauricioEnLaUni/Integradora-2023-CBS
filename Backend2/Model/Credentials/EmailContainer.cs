using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model;

public class EmailContainer : BaseEntity
{
    public string Value { get; set; } = string.Empty;

    public EmailContainer(string data)
    {
        Value = data;
    }
}