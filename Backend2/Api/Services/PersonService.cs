using MongoDB.Driver;

using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Repository;

public class PersonService
{
    public readonly IMongoCollection<Person> personCollection;
    private readonly EmailService emailService;

    public PersonService(MongoSettings container, EmailService email)
    {
        personCollection = container.Client.GetDatabase("cbs")
            .GetCollection<Person>("people");
        emailService = email;
    }
    
    public async Task<NewPersonDto?> ValidateNewPerson(NewPersonDto data)
    {
        if (data is null) return null;

        NewPersonDto result = data;
        
        if (result.Email is not null)
            result.Email = await emailService
                .GetEmailByValue(result.Email) is null ? result.Email : null;
        if (data.Email is null && data.Phone is null) return null;

        return result;
    }

    public bool IsLegal(DateOnly DOB)
    {
    }
}