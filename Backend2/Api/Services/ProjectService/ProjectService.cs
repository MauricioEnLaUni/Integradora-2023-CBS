using MongoDB.Driver;

using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Utilities.MongoDB;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Repository;

public class ProjectService
{
    private readonly IMongoCollection<Project> _projectCollection;
    private readonly PersonService _personService;
    private readonly MaterialService _materialService;

    public ProjectService(
        MongoSettings container,
        PersonService person,
        MaterialService material)
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

        return new() { Code = 200, Value = output };
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

    private UpdatedFTaskDto? ValidateFTaskUpdate(UpdatedFTaskDto? data)
    {
        if (data is null) return null;
        List<string> employees = _personService
            .GetByFilter(Filter.EmptyFilter<Person>())
            .Select(x => x.Id)
            .ToList();
        Project? originalDocument = GetOneByFilter(data.Id);

        HTTPResult<FTasks?> original =
            FTaskManager.GetTask(originalDocument, data.Id);
        if (original.Code != 200) return null;

        UpdatedFTaskDto result = data;
        result.Ends = TimeTrackerService.ValidateDueDate(data.Ends);
        
        result.Subtasks?.ForEach(e => {
            if (e.Operation == 2) e.UpdateItem =
                ValidateFTaskUpdate(e.UpdateItem);
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
                
        

        return result;
    }

    private List<UpdateList<string>>? ValidateMaterial(
        List<string> materialList,
        List<UpdateList<string>> newData,
        FTasks original)
    {
        List<UpdateList<string>> result = newData;
        result.ForEach(e => {
            if (e.Operation is not 1
            && (!materialList.Contains(e.NewItem!)
            || original.Material.Contains(e.NewItem!))) result.Remove(e);
        });

        return result;
    }
    #endregion

    #region CRUD
    public async Task<Project> InsertOneAsync(string data)
    {
        Project output = new Project().Instantiate(data);
        await _projectCollection.InsertOneAsync(output);
        return output;
    }

    public void Clear()
    {
        _projectCollection.DeleteMany(Filter.EmptyFilter<Project>());
    }

    public Project? GetOneByFilter(
        FilterDefinition<Project> filter)
    {
        return _projectCollection
            .Find(filter)
            .SingleOrDefault();
    }

    public async Task<Project?> GetOneByFilterAsync(
        FilterDefinition<Project> filter)
    {
        return await _projectCollection.GetOneByFilterAsync(filter);
    }

    public async Task<List<Project>> GetAllAsync()
    {
        return await _projectCollection
            .Find(Filter.EmptyFilter<Project>())
            .ToListAsync();
    }
    #endregion
}