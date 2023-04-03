using MongoDB.Driver;

using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Repository;

public class ProjectService
    : BaseService<Project, string, UpdatedProjectDto>
{
    private const string MAINCOLLECTION = "projects";

    public ProjectService(
        MongoSettings container) : base(container, MAINCOLLECTION) { }

    public async Task<bool> NameIsUnique(string data)
    {
        FilterDefinition<Project> filter = Builders<Project>.Filter
            .Eq(x => x.Name, data);
        return await _mainCollection.Find(filter)
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

    public async Task<HTTPResult<UpdatedProjectDto?>>
        ValidateUpdate(UpdatedProjectDto data, Project old)
    {
        if (UpdateIsEmpty(data)) return new() { Code = 400 };

        UpdatedProjectDto output = data;
        output.Name = await ValidateName(data.Name);
        output.Starts = ValidateStart(old, data.Starts);
        output.Ends = data.Ends;
        if (output.Starts is not null
            && output.Ends is not null
            && ValidateDates(output.Starts, output.Ends))
                return new() { Code = 400 };

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

    internal bool ValidateDates(DateTime? start, DateTime? ends)
    {
        if (start is null || ends is null) return false;
        DateTime st = (DateTime)start;
        DateTime end = (DateTime)ends;
        if (DateTime.Compare(st, end) >= 0) return false;

        return true;
    }

    internal List<ProjectDto> ToDtoList(List<Project> data)
    {
        List<ProjectDto> result = new();
        data.ForEach(e => {
            result.Add(e.ToDto());
        });
        return result;
    }
}