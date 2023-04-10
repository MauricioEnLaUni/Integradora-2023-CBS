using Newtonsoft.Json;

using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Model;

public class EmailContainer
    : BaseEntity, IQueryMask<EmailContainer, NewEmailDto, UpdatedEmailDto>
{
    public string owner = string.Empty;
    public string value = string.Empty;

    private EmailContainer(NewEmailDto data)
    {
        owner = data.owner;
        value = data.value;
    }
    public EmailContainer() { }

    public EmailContainer Instantiate(NewEmailDto data)
    {
        return new(data);
    }

    public EmailContainerDto ToDto()
    {
        return new() { email = value };
    }

    public string Serialize()
    {
        EmailContainerDto data = ToDto();
        return JsonConvert.SerializeObject(data);
    }

    public void Update(UpdatedEmailDto data)
    {
        if (data.value is not null
            && !data.value.IsEmailFormatted())
                return ;
        value = data.value ?? value;
        owner = data.owner ?? owner;
    }
}