using MongoDB.Driver;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Repository;

public class EmailService
    : BaseService<EmailContainer, NewEmailDto, UpdatedEmailDto>
{
    private const string MAINCOLLECTION = "emails";

    public EmailService(MongoSettings container)
        : base(container, MAINCOLLECTION) { }

    public async void ValidateEmailUpdate(
        List<UpdateList<string>> data)
    {
        foreach (var email in data)
        {
            if (email.Operation == 1) continue;
            if (email.NewItem is null ||
            email.NewItem == string.Empty ||
            !email.NewItem.IsEmailFormatted())
            {
                data.Remove(email);
                continue;
            }
            FilterDefinition<EmailContainer> filter =
                Builders<EmailContainer>
                    .Filter
                    .Eq(x => x.Id, email.NewItem);
            if (await GetByAsync(filter) != null)
                data.Remove(email);
        }
    }
}