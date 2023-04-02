using MongoDB.Driver;

using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Utilities.MongoDB;

namespace Fictichos.Constructora.Repository;

internal class FTaskService
    : BaseService<FTasks, NewFTaskDto, UpdatedFTaskDto>
{
    private const string MAINCOLLECTION = "tasks";

    internal FTaskService(MongoSettings container)
        : base(container, MAINCOLLECTION) { }

    internal HTTPResult<NewFTaskDto> ValidateNew(
        NewFTaskDto data, List<string> employees)
    {
        NewFTaskDto validated = data;
        
        if (validated.Ends < DateTime.Now) return new() { Code = 400 };
        validated.Assignees = ValidateAssigned(data.Assignees, employees);
        
        return new() { Code = 200, Value = validated };
    }

    internal UpdatedFTaskDto ValidateUpdate(
        UpdatedFTaskDto data,
        List<string> employees,
        FTasks original,
        List<string> material)
    {
        UpdatedFTaskDto valid = data;

        valid.Ends = TimeTrackerService.ValidateDueDate(data.Ends);

        valid.EmployeesAssigned = ValidateAssigned(
            valid.EmployeesAssigned,
            employees,
            original.EmployeesAssigned);
        
        valid.Material = ValidateMaterial(
            material, valid.Material, original.Material);

        valid.Subtasks?.ForEach(e => {
            if (e.Operation == 2) e.UpdateItem =
                ValidateUpdate(
                    e.UpdateItem!, employees, original, material);

            if (e.Operation == 0) e.NewItem =
                ValidateNew(e.NewItem!, employees).Value;

            if (e.Operation != 0 && e.NewItem is null && e.UpdateItem is null)
                valid.Subtasks.Remove(e);
        });

        return valid;
    }

    internal List<FTasksDto> ToDtoList(List<FTasks> data)
    {
        List<FTasksDto> result = new();
        data.ForEach(e => {
            result.Add(e.ToDto());
        });

        return result;
    }

    internal static List<UpdateList<string>>? ValidateAssigned(
        List<UpdateList<string>>? data,
        List<string> employees,
        List<string> original)
    {
        if (data is null || original is null) return null;

        List<UpdateList<string>> validated = data;
        validated.ForEach(e => {
            if (!employees.Where(x => x == e.NewItem).Any()
            || original.Contains(e.NewItem!))
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

    internal List<UpdateList<string>>? ValidateMaterial(
        List<string> materialList,
        List<UpdateList<string>>? newData,
        List<string> original)
    {
        if (newData is null) return null;

        List<UpdateList<string>> result = newData;
        result.ForEach(e => {
            if (e.Operation is not 1
            && (!materialList.Contains(e.NewItem!)
            || original.Contains(e.NewItem!))) result.Remove(e);
        });

        return result;
    }

    internal void Update(UpdateDto<FTasks> data)
    {
        _mainCollection.UpdateOne(data.filter, data.update);
    }
}