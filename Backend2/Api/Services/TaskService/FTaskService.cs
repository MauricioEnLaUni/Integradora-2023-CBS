using MongoDB.Driver;

using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Repository;

internal class FTaskService
    : BaseService<FTasks, NewFTaskDto, UpdatedFTaskDto>
{
    private const string MAINCOLLECTION = "tasks";

    internal FTaskService(MongoSettings container)
        : base(container, MAINCOLLECTION) { }

    internal async Task<HTTPResult<NewFTaskDto>> ValidateNew(NewFTaskDto data)
    {
        NewFTaskDto validated = data;
        
        if (!await NameIsUnique(data.Name)) return new() { Code = 409 };
        if (validated.Ends < DateTime.Now) return new() { Code = 400 };
        
        return new() { Code = 200, Value = validated };
    }

    internal async Task<bool> NameIsUnique(string name)
    {
        FilterDefinition<FTasks> filter = Builders<FTasks>
            .Filter
            .Eq(x => x.Name, name);
        if ((await GetByAsync(filter)).Count > 0) return false;
        return true;
    }

    internal List<FTasksDto> ToDtoList(List<FTasks> data)
    {
        List<FTasksDto> result = new();
        data.ForEach(e => {
            result.Add(e.ToDto());
        });

        return result;
    }
}