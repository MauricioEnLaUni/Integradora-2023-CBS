using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Repository;

public static class AccountManager
{
    public static List<IndexedObjectUpdate<NewPaymentDto, UpdatedPaymentDto>>?
        ValidatePayments(List<IndexedObjectUpdate<NewPaymentDto, UpdatedPaymentDto>>? data)
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
}