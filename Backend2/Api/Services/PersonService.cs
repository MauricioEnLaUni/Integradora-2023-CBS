using MongoDB.Driver;

using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Repository;

public class PersonService
{
    public readonly IMongoCollection<Person> personCollection;

    public PersonService(MongoSettings container)
    {
        personCollection = container.Client.GetDatabase("cbs")
            .GetCollection<Person>("people");
    }
    
    public NewPersonDto? ValidateNewPerson(NewPersonDto data)
    {
        if (data is null) return null;

        NewPersonDto result = new();

        return result;
    }

    public string? ValidateEmail(string? data)
    {
        if (data is null) return null;

        return data;
    }

}