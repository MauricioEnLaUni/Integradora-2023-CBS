using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using MongoDB.Driver;

namespace Fictichos.Constructora.Repository;

public class EmailService
{
    private readonly IMongoCollection<EmailContainer> _emailCollection;

    public EmailService(MongoSettings container, UserService users)
    {
        _emailCollection = container.Client.GetDatabase("cbs")
            .GetCollection<EmailContainer>("email");
    }

    public async Task<List<EmailContainer>> GetEmailsByUser(string data)
    {
        List<EmailContainer> result = new();
        if (data is null) return result;

        FilterDefinition<EmailContainer> filter = Builders<EmailContainer>.Filter
            .Eq(x => x.owner, data);
        result = await _emailCollection.GeyByFilterAsync(filter);
        return result;
    }

    public async Task<EmailContainer?> GetEmailByValue(string data)
    {
        if (data is null) return null;
        
        FilterDefinition<EmailContainer> filter = Builders<EmailContainer>.Filter
            .Eq(x => x.value, data);
        EmailContainer? result = await _emailCollection.GetOneByFilterAsync(filter);

        return result;
    }

    public async void ValidateEmailUpdate(
        List<UpdateList<string>> data,
        User usr)
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
            if (await GetEmailByValue(email.NewItem) != null)
                data.Remove(email);
        }
    }

    public async Task InsertOneAsync(string owner, string value)
    {
        EmailContainer data = new(owner, value);
        await _emailCollection.InsertOneAsync(data);
    }
}