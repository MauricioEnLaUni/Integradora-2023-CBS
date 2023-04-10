using System.Text.RegularExpressions;

using MongoDB.Driver;

using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities.MongoDB;

namespace Fictichos.Constructora.Repository;

public class PersonService
    : BaseService<Person, NewPersonDto, UpdatedPersonDto>
{
    private const string MAINCOLLECTION = "people";
    private const string RFCPATTERN = @"^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$";
    private const string CURPPATTERN = @"([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)";
    public PersonService(MongoSettings container)
        : base(container, MAINCOLLECTION) { }

    public NewJobDto? ValidateJob(NewJobDto data)
    {
        if (data is null) return null;
        if (data.Responsibilities.Count == 0) return null;
        FilterDefinition<Person> filter = Builders<Person>.Filter
            .Eq(x => x.Id, data.Parent);

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
        if (sanitized.PayPeriod < 0) return null;
        if (!sanitized.Due && sanitized.PayPeriod > 52) return null;
        if (sanitized.Due && sanitized.PayPeriod > 26) return null;

        return sanitized;
    }

    internal async Task UpdateAsync(UpdateDto<Person> data)
    {
        await _mainCollection.UpdateOneAsync(data.filter, data.update);
    }
    
    public NewPersonDto? ValidateNewPerson(NewPersonDto data)
    {
        if (data is null) return null;

        NewPersonDto result = data;
        if (!ValidateRegex(result.RFC, RFCPATTERN)) return null;
        if (!ValidateRegex(result.CURP, CURPPATTERN)) return null;

        NewJobDto? job = ValidateJob(result.Job);
        if (job is null) return null;

        result.Job = job;
        
        return result;
    }

    internal bool ValidateRegex(string raw, string pattern)
    {
        int timeOut = 5000;
        Regex regex = new(pattern, RegexOptions.None, TimeSpan.FromMilliseconds(timeOut));
        return regex.IsMatch(raw);
    }

    internal async Task<bool> ValidateAreaHead(string? data)
    {
        if (data is null) return false;
        Person? found = await GetOneByAsync(Filter.ById<Person>(data));
        if (found is null) return false;

        Job? job = found?.GetCurrentJob();
        if (job is null) return false;
        if (!job.Responsibilities.Contains("Manager")) return false;

        return true;
    }
}