using MongoDB.Bson;
using MongoDB.Driver;

using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Repository;

public class PersonService
    : BaseService<Person, NewPersonDto, UpdatedPersonDto>
{
    private const string MAINCOLLECTION = "people";

    public PersonService(MongoSettings container, EmailService email, TimeTrackerService time)
        : base(container, MAINCOLLECTION) { }
    
    public NewPersonDto? ValidateNewPerson(NewPersonDto data)
    {
        if (data is null) return null;

        NewPersonDto result = data;
        


        return result;
    }

    public NewEmployeeDto? ValidateEmployee(NewEmployeeDto data)
    {
        if (data is null) return null;

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
        BsonDocument? parent = await _mainCollection.Find(filter)
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

    internal async Task UpdateAsync(UpdateDto<Person> data)
    {
        await _mainCollection.UpdateOneAsync(data.filter, data.update);
    }
}