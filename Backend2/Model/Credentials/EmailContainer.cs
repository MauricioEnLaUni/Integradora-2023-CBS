using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Model;

public class EmailContainer : BaseEntity
{
    public string owner = string.Empty;
    public string value = string.Empty;

    public EmailContainer(string parent, string data)
    {
        owner = parent;
        value = data;
    }
}