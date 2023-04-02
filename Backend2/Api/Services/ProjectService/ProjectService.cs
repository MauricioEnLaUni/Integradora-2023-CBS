using MongoDB.Driver;

using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Utilities.MongoDB;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Repository;

internal class ProjectService
    : BaseService<Project, string, UpdatedProjectDto>
{
    private const string MAINCOLLECTION = "projects";
    private readonly IMongoCollection<Project> _projectCollection;
    private readonly PersonService _personService;
    private readonly MaterialService _materialService;

    internal ProjectService(
        MongoSettings container,
        PersonService person,
        MaterialService material) : base(container, MAINCOLLECTION)
    {
        _projectCollection = container.Client.GetDatabase("cbs")
            .GetCollection<Project>("projects");
        _personService = person;
        _materialService = material;
    }

    public async Task<bool> NameIsUnique(string data)
    {
        FilterDefinition<Project> filter = Builders<Project>.Filter
            .Eq(x => x.Name, data);
        return await _projectCollection.Find(filter)
            .SingleOrDefaultAsync() is null;
        
    }

    public ProjectDto To(Project data)
    {
        FTasks? last = data.Tasks
            .Where(x => !x.Complete)
            .OrderBy(x => x.Ends)
            .SingleOrDefault();
        FTasksDto? lastTask = last?.To<FTasks, FTasksDto>();
        
        return new()
        {
            Id = data.Id,
            Name = data.Name,
            Starts = data.Starts,
            Ends = data.Ends,
            LastTask = lastTask
        };
    }

    public async Task<HTTPResult<UpdatedProjectDto?>> ValidateUpdate(
        UpdatedProjectDto data,
        Project old)
    {
        if (UpdateIsEmpty(data)) return new() { Code = 400 };

        UpdatedProjectDto output = data;
        output.Name = await ValidateName(data.Name);
        output.Starts = ValidateStart(old, data.Starts);
        output.Ends = data.Ends;

        output.PayHistory =
            await ValidateAccount(old.PayHistory, output.PayHistory);
        output.Tasks =
            FTasksDriver(output.Tasks);

        return new() { Code = 200, Value = output };
    }

    private List<IndexedObjectUpdate<NewFTaskDto, UpdatedFTaskDto>>?
        FTasksDriver(
            List<IndexedObjectUpdate<NewFTaskDto, UpdatedFTaskDto>>? data)
    {
        if (data is null) return null;
        List<IndexedObjectUpdate<NewFTaskDto, UpdatedFTaskDto>> output = data;
        output.ForEach(async (e) => {
            if (e.Operation != 1 && e.NewItem is null && e.UpdateItem is null)
                output.Remove(e);
            if (e.Operation == 2) e.UpdateItem =
                await ValidateFTaskUpdate(e.UpdateItem);
        });

        return output;
    }

    public bool UpdateIsEmpty(UpdatedProjectDto data)
    {
        if (data.Name is null && data.PayHistory is null &&
            data.Responsible is null && data.Tasks is null &&
                data.Ends is null && data.Starts is null) return true;
        return false;
    }

    public async Task<string?> ValidateName(string? data)
    {
        if (data is null) return null;
        if (!await NameIsUnique(data)) return null;
        return data;
    }

    /// <summary>
    /// Project must start with their first task at the latest.
    /// </summary>
    public DateTime? ValidateStart(Project old, DateTime? data)
    {
        if (data is null) return null;
        DateTime? firstTask = old.Tasks
            .OrderBy(x => x.StartDate)
            .Select(x => x.StartDate)
            .FirstOrDefault();
        if (firstTask is null) return data;
        if (data < firstTask) data = firstTask;
        return data;
    }

    #region Account Validation

    private async Task<UpdatedAccountDto?>
        ValidateAccount(Account oldAccount, UpdatedAccountDto? data)
    {
        if (data is null) return null;

        UpdatedAccountDto validated = data;
        Project? newOwner = await ValidateAccountOwner(data.Owner);

        validated.Payments = AccountManager.ValidatePayments(data.Payments);
        oldAccount.Update(validated);

        if (newOwner is null)
        {
            validated.Owner = null;
        }
        else
        {
            UpdatedProjectDto pUp = new() { Id = newOwner.Id, Transferred = oldAccount };
            newOwner.Update(pUp);
            FilterDefinition<Project> filter = Builders<Project>.Filter
                .Eq(x => x.Id, pUp.Id);
            _projectCollection.ReplaceOne(filter, newOwner);
        }

        return validated;
    }

    private async Task<Project?> ValidateAccountOwner(string? data)
    {
        if (data is null) return null;

        FilterDefinition<Project> filter = Builders<Project>.Filter
            .Eq(x => x.Id, data);
        Project? newProject = await _projectCollection
            .GetOneByFilterAsync(filter);
        if (newProject is null) return null;
        if (newProject.PayHistory is not null) return null;

        return newProject;
    }

    #endregion

    #region FTask Validation

    private async Task<UpdatedFTaskDto?>
        ValidateFTaskUpdate(UpdatedFTaskDto? data)
    {
        List<string> employees = new();
        List<Material> materials = new();

        if (data is null) return null;
        if (data.EmployeesAssigned is not null)
        {
            employees = (await _personService
                .GetByAsync(Filter.Empty<Person>()))
                .Select(x => x.Id)
                .ToList();
        }
        if (data.Material is not null)
            materials = await _materialService
                .GetByFilterAsync(Filter.Empty<Material>());
        Project? originalDocument = GetOneBy(data.Id);

        HTTPResult<FTasks?> original =
            FTaskManager.GetTask(originalDocument, data.Id);
        if (original.Code != 200) return null;

        UpdatedFTaskDto result = data;
        result.Ends = TimeTrackerService.ValidateDueDate(data.Ends);
        
        result.Subtasks?.ForEach(async (e) => {
            if (e.Operation == 2) e.UpdateItem =
                await ValidateFTaskUpdate(e.UpdateItem);
            if (e.Operation == 0) e.NewItem =
                FTaskManager.ValidateNewFTask(e.NewItem, employees);
            if (e.Operation != 0 && e.NewItem is null && e.UpdateItem is null)
                result.Subtasks.Remove(e);
        });

        result.EmployeesAssigned = FTaskManager.ValidateAssigned(
            result.EmployeesAssigned,
            employees,
            originalDocument!.Tasks
                .Where(x => x.Id == data.Id)
                .SingleOrDefault());

        result.Material = ValidateMaterial(
                materials.Select(x => x.Id).ToList(),
                result.Material!,
                original.Value!);

        return result;
    }

    private List<UpdateList<string>>? ValidateMaterial(
        List<string> materialList,
        List<UpdateList<string>>? newData,
        FTasks original)
    {
        if (newData is null) return null;

        List<UpdateList<string>> result = newData;
        result.ForEach(e => {
            if (e.Operation is not 1
            && (!materialList.Contains(e.NewItem!)
            || original.Material.Contains(e.NewItem!))) result.Remove(e);
        });

        return result;
    }
    #endregion
}