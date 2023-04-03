using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Repository;

internal class AccountService
    : BaseService<Account, NewAccountDto, UpdatedAccountDto>
{
    private const string MAINCOLLECTION = "accounts";
    internal AccountService(MongoSettings container)
        : base(container, MAINCOLLECTION) { }

    internal List<IndexedObjectUpdate<NewPaymentDto, UpdatedPaymentDto>>?
        ValidatePayments(
            List<IndexedObjectUpdate<NewPaymentDto, UpdatedPaymentDto>>? data)
    {
        if (data is null) return null;
        
        data.ForEach(e => {
            if (e.NewItem is not null) e.NewItem.Due
                = (DateTime)TimeTrackerService.ValidateDueDate(e.NewItem.Due)!;
        });
        data.ForEach(e => {
            if (e.UpdateItem is not null) e.UpdateItem.Due
                = (DateTime)TimeTrackerService.ValidateDueDate(e.UpdateItem.Due)!;
        });
        return data;
    }

    internal bool NameIsUnique(string name, List<string> names)
    {
        if (names.Contains(name)) return false;
        return true;
    }

    internal HTTPResult<Account> ValidateNew(
        List<Account> accounts,
        NewAccountDto data,
        Project? owner)
    {
        if (owner is null) return new() { Code = 404 };
        if (!NameIsUnique(data.Name, accounts.Select(x => x.Id).ToList()))
            return new() { Code = 409 };
        
        Account item = new(data);
        
        return new() { Code = 200, Value = item };
    }

    internal UpdatedAccountDto ValidateUpdate(
        UpdatedAccountDto data,
        List<Account> accounts,
        Project owner)
    {
        UpdatedAccountDto result = data;
        if (result.Name is not null && !NameIsUnique(
            result.Name, accounts.Select(x => x.Id).ToList()))
                result.Name = null;
        
        result.Payments = ValidatePayments(result.Payments);

        return result;
    }
}