using MongoDB.Bson;
using MongoDB.Driver;

using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Repository;

public class PersonService
{
    public readonly IMongoCollection<Person> _personCollection;
    private readonly TimeTrackerService timeService;
    private readonly EmailService emailService;

    public PersonService(MongoSettings container, EmailService email, TimeTrackerService time)
    {
        timeService = time;
        _personCollection = container.Client.GetDatabase("cbs")
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
        if (result.Email is null && result.Phone is null) return null;
        


        return result;
    }

    public bool IsLegal(DateTime DOB)
    {
        return timeService.Over(0, DOB);
    }

    public NewEmployeeDto? ValidateEmployee(NewEmployeeDto data)
    {
        if (data is null) return null;

        if (!IsLegal(data.DOB)) return null;

        return data;
    }

    public async Task<NewJobDto?> ValidateJob(NewJobDto data)
    {
        if (data is null) return null;
        if (data.Responsibilities.Count == 0) return null;
        FilterDefinition<Person> filter = Builders<Person>.Filter
            .Eq(x => x.Id, data.Parent);
        ProjectionDefinition<Person> projection = Builders<Person>.Projection
            .Include(x => x.Employed);
        BsonDocument? parent = await _personCollection.Find(filter)
            .Project(projection)
            .SingleOrDefaultAsync();

        if (parent is null) return null;

        NewJobDto sanitized = data;
        NewSalaryDto? sanitizedSalary = ValidateSalary(data.SalaryHistory);
        if (sanitizedSalary is null) return null;

        sanitized.SalaryHistory = sanitizedSalary;

        return sanitized;
    }

    public NewSalaryDto? ValidateSalary(NewSalaryDto data)
    {
        if (data is null) return null;
        NewSalaryDto sanitized = data;

        if (sanitized.HoursWeeklyCap > 48) sanitized.HoursWeeklyCap = 48;

        return sanitized;
    }

    public async Task<Person?> GetOneByFilterAsync(
        FilterDefinition<Person> filter)
    {
        return await _personCollection
            .Find(filter)
            .SingleOrDefaultAsync();
    }
}