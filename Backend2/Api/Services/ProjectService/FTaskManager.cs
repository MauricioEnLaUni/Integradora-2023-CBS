using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Repository;

internal class FTaskManager
{
    internal static List<UpdateList<string>>? ValidateAssigned(
        List<UpdateList<string>>? data,
        List<string> employees,
        FTasks? original)
    {
        if (data is null || original is null) return null;

        List<UpdateList<string>> validated = data;
        validated.ForEach(e => {
            if (!employees.Where(x => x == e.NewItem).Any()
            || original.EmployeesAssigned.Contains(e.NewItem!))
                validated.Remove(e);
        });
        return validated;
    }

    internal static List<string> ValidateAssigned(
        List<string> data,
        List<string> employees)
    {
        List<string> validated = data;
        validated.ForEach(e => {
            if (!employees.Where(x => x == e).Any())
                validated.Remove(e);
        });
        return validated;
    }
}