namespace Fictichos.ERP.Company.Commands.CreateCompany;

public sealed record CreateCompanyCommand(string Name, string Activity, string Relation) : ICommand<Guid>;