using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;

using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Repository;

public class ProjectService
{
    public readonly IMongoCollection<Project> projectCollection;
    public readonly IMongoCollection<Person> personCollection;
    private readonly IMongoCollection<Material> materialCollection;

    public ProjectService(MongoSettings container)
    {
        projectCollection = container.Client.GetDatabase("cbs")
            .GetCollection<Project>("projects");
        personCollection = container.Client.GetDatabase("cbs")
            .GetCollection<Person>("person");
        materialCollection = container.Client.GetDatabase("cbs")
            .GetCollection<Material>("material");
    }

    public async Task<bool> NameIsUnique(string data)
    {
        FilterDefinition<Project> filter = Builders<Project>.Filter
            .Eq(x => x.Name, data);
        return await projectCollection.Find(filter)
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

    public async Task<UpdatedProjectDto?> ValidateUpdate(UpdatedProjectDto data)
    {
        if (UpdateIsEmpty(data)) return null;
        FilterDefinition<Project> filter = Builders<Project>.Filter
            .Eq(x => x.Id, data.Id);
        Project? old = await projectCollection.GetOneByFilterAsync(filter);
        if (old is null) return null;

        UpdatedProjectDto output = new();
        output.Name = await ValidateName(data.Name);
        Person? responsible = await ValidateResponsible(data.Responsible);
        output.Starts = ValidateStart(old, data.Starts);
        output.Ends = data.Ends;

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

    public async Task<Person?> ValidateResponsible(string? data)
    {
        if (data is null) return null;

        FilterDefinition<Person> respFilter = Builders<Person>
            .Filter.Eq(x => x.Id, data);
        Person? resp = await personCollection.Find(respFilter)
            .SingleOrDefaultAsync();
        if (resp is null) return null;

        return resp;
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

        validated.Payments = ValidatePayments(data.Payments);
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
            projectCollection.ReplaceOne(filter, newOwner);
        }

        return validated;
    }

    private async Task<Project?> ValidateAccountOwner(string? data)
    {
        if (data is null) return null;

        FilterDefinition<Project> filter = Builders<Project>.Filter
            .Eq(x => x.Id, data);
        Project? newProject = await projectCollection
            .GetOneByFilterAsync(filter);
        if (newProject is null) return null;
        if (newProject.PayHistory is not null) return null;

        return newProject;
    }

    private List<IndexedObjectUpdate<NewPaymentDto, UpdatedPaymentDto>>?
        ValidatePayments(List<IndexedObjectUpdate<NewPaymentDto, UpdatedPaymentDto>>? data)
    {
        if (data is null) return null;
        
        data.ForEach(e => {
            if (e.NewItem is not null) e.NewItem.Due
                = (DateTime)ValidateDueDate(e.NewItem.Due)!;
        });
        data.ForEach(e => {
            if (e.UpdateItem is not null) e.UpdateItem.Due
                = (DateTime)ValidateDueDate(e.UpdateItem.Due)!;
        });
        return data;
    }

    private DateTime? ValidateDueDate(DateTime? data)
    {
        if (data is null) return null;
        DateTime? due = DateTime.Compare((DateTime)data, DateTime.Now) < 0
            ? DateTime.Now : data;

        return due;
    }

    #endregion

    #region FTask Validation
    
    private static NewFTaskDto? ValidateNewFTask(NewFTaskDto? data)
    {
        if (data is null) return null;

        NewFTaskDto validated = data;
        if (validated.Ends < DateTime.Now) return null;

        return validated;
    }

    private async Task<UpdatedFTaskDto?> ValidateFTask(UpdatedFTaskDto? data)
    {
        if (data is null) return null;

        FTasks? original = await GetTask(data);
        if (original is null) return null;

        UpdatedFTaskDto result = data;
        result.Ends = ValidateDueDate(data.Ends);
        
        result.Subtasks?.ForEach(async (e) => {
            if (e.Operation == 2) e.UpdateItem = await ValidateFTask(e.UpdateItem);
            if (e.Operation == 0) e.NewItem = ValidateNewFTask(e.NewItem);
            if (e.Operation != 0 && e.NewItem is null && e.UpdateItem is null)
                result.Subtasks.Remove(e);
        });

        return result;
    }

    private async Task<FTasks?> GetTask(UpdatedFTaskDto data)
    {
        if (data is null) return null;
        FilterDefinition<Project> filter = Builders<Project>.Filter
            .Eq(x => x.Id, data.Parent);
        Project? project = await projectCollection.GetOneByFilterAsync(filter);
        if (project is null) return null;

        FTasks? original = project.Tasks
            .Where(x => x.Id == data.Id)
            .SingleOrDefault();
        if (original is null) return null;

        return original;
    }
    #endregion
}