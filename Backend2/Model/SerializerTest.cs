using Fictichos.Constructora.Dto;
using MongoDB.Bson;

namespace Fictichos.Constructora.Utilities;

public class SerializerTest
{
    public ProjectDto projectDto { get; set; } = new()
    {
        Id = ObjectId.GenerateNewId().ToString(),
        Name = "whatever",
        Starts = DateTime.Now,
        Ends = DateTime.Now,
        LastTask = new()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            StartDate = DateTime.Now,
            Ends = DateTime.Now,
            Parent = "whatever",
            Subtasks = new()
            {
                new() { Id = ObjectId.GenerateNewId().ToString(), StartDate = DateTime.Now, Ends = DateTime.Now, Parent = "whatever", Subtasks = new(), EmployeesAssigned = new() { "alsdansd", "asnaspf" }, Material = new() { "dfboubgs", "gsdbgp0sidg" } },
                new() { Id = ObjectId.GenerateNewId().ToString(), StartDate = DateTime.Now, Ends = DateTime.Now, Parent = "whatever", Subtasks = new(), EmployeesAssigned = new() { "alsdansd", "asnaspf" }, Material = new() { "dfboubgs", "gsdbgp0sidg" } }
            },
            EmployeesAssigned = new() { "alsdansd", "asnaspf" },
            Material = new() { "dfboubgs", "gsdbgp0sidg" },
            Address = new() { Street = "Calle C", Number = "1687", Colony = "Zona Centro", PostalCode = "aspanh", }
        }
    };

    public MaterialDto Material { get; set; } = new()
    {
        Id = ObjectId.GenerateNewId().ToString(),
        Name = "dfasopd",
        Owner = "asobaf",
        Brand = "adpaibnsdf",
        Quantity = 98
    };

    public MaterialCategoryDto Category { get; set; } = new()
    {
        Id = ObjectId.GenerateNewId().ToString(),
        Name = "dfasopd",
        Parent = "adighsd",
        SubCategory = new() { "asdfbosdog", "bvdfnibn" },
        Children = new() { "aspdfinasg", "bnpiasdfpasf" }
    };

    public AccountDto Account { get; set; } = new()
    {
        Id = ObjectId.GenerateNewId().ToString(),
        CreatedAt = DateTime.Now,
        Payments = new()
        {
            new() { Id = ObjectId.GenerateNewId().ToString(), Amount = 94.45, Concept = "dsg0dgh8", CreatedAt = DateTime.Now, Due = DateTime.Now, Complete = false },
            new() { Id = ObjectId.GenerateNewId().ToString(), Amount = 94.45, Concept = "dsg0dgh8", CreatedAt = DateTime.Now, Due = DateTime.Now, Complete = false }
        },
        Owner = "fgsdgsdgne"
    };

    public PersonDto Person { get; set; } = new()
    {
        Id = ObjectId.GenerateNewId().ToString(),
        Name = "9",
        Contact = new()
        {
            Addresses = new()
            { new() { Street = "atansgf" }},
            Phones = new() { "1694894", "8743169" },
            Emails = new() { "64941@axpfa.com" }
        },
        Employee = new()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = "10",
            DOB = DateTime.Now,
            CURP = "684431321231",
            RFC = "ASIDBNASPDA",
            Charges = new()
            {
                new()
                { 
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "asfashf",
                    SalaryHistory = new()
                    {
                        new() { Id = ObjectId.GenerateNewId().ToString(), Reductions = new() { { "sdasnd", 10.0 } }, HourlyRate = 193.00, Period = "semana", Due = "asdasd1a", HoursWeeklyCap = 48, Closed = DateTime.Now }
                    },
                    Role = "asdaspnda"
                },
            },
            ScheduleHistory = new()
            {
                new() { Id = ObjectId.GenerateNewId().ToString(), Period = "sdfidf", Hours = new() { { new TimeSpan(), 10 }}, Location = new() { Street = "asda" }}
            }
        }
    };

    public LoginSuccessDto UserInfo { get; set; } = new()
    {
        Id = ObjectId.GenerateNewId().ToString(),
        Name = "sdgasdgn",
        CreatedAt = DateTime.Now,
        Roles = new() { "fasopfa", "asfpiahsf" },
        Email = new() { "asdbasop@pajsda.com"},
        Avatar = null
    };
}