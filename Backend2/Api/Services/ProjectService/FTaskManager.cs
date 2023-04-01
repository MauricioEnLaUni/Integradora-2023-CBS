using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Repository;

internal class FTaskManager
{
    // Not complete
    internal static NewFTaskDto? ValidateNewFTask(
        NewFTaskDto? data,
        List<string> employees)
    {
        if (data is null) return null;

        NewFTaskDto validated = data;
        if (validated.Ends < DateTime.Now) return null;
        validated.Assignees = ValidateAssigned(data.Assignees, employees);

        return validated;
    }

    internal static HTTPResult<FTasks?> GetTask(Project? source, string id)
    {
        if (source is null) return new() { Code = 400 };
        FTasks? output = source.Tasks
            .Where(x => x.Id == id)
            .SingleOrDefault();
        if (output is null) return new() { Code = 404 };

        return new() { Code = 200, Value = output };
    }

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