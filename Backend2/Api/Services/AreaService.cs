using MongoDB.Bson;
using MongoDB.Driver;

using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Model;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Utilities.MongoDB;

namespace Fictichos.Constructora.Repository;

public class AreaService
    : BaseService<Area, NewAreaDto, UpdatedAreaDto>
{
    private const string MAINCOLLECTION = "areas";

    public AreaService(MongoSettings container)
        : base(container, MAINCOLLECTION) { }

    private async Task<bool> ParentIsValid(string? data)
    {
        if (data is null) return true;
        if (data == "root") return true;
        FilterDefinition<Area> filter = Builders<Area>
            .Filter
            .Eq(x => x.Id, data);
        if (await GetByAsync(filter) is not null) return false;

        return true;
    }

    private async Task<bool> NameIsUnique(string name, string? parent)
    {
        FilterDefinition<Area> filter = Builders<Area>
            .Filter
            .Eq(x => x.Name, name);
        List<Area> bulk = await GetByAsync(filter);
        List<Area> conflict = new();
        if (parent is not null)
        {
            conflict = bulk
                .Where(x => x.Name == name)
                .Where(x => x.Parent == parent)
                .ToList();
        } else 
        {
            conflict = bulk.Where(x => x.Name == name).ToList();
        }
        if (conflict.Count > 0) return false;

        return true;
    }
    
    public async Task<bool> ValidateNew(NewAreaDto data)
    {
        bool nameCheck = await NameIsUnique(data.Name, data.Parent);
        bool parentCheck = await ParentIsValid(data.Parent);
        if (!nameCheck || !parentCheck) return false;

        return true;
    }

    public async Task NewChildren(UpdateList<string> data, string main)
    {
        if (data.NewItem is null) return;
        Area? item = await _mainCollection
            .Find(Filter.ById<Area>(data.NewItem))
            .SingleOrDefaultAsync();
        if (item is null) return;

        string? parent = item.Parent;
        UpdateDefinition<Area> update = Builders<Area>
            .Update
            .Set(x => x.ModifiedAt, DateTime.Now)
            .Set(x => x.Parent, main);
        await _mainCollection
            .UpdateOneAsync(Filter.ById<Area>(data.NewItem), update);
        update = Builders<Area>
            .Update
            .Set(x => x.ModifiedAt, DateTime.Now)
            .Push(x => x.Children, data.NewItem);
        await _mainCollection
            .UpdateOneAsync(Filter.ById<Area>(main), update);
        if (parent is not null)
        {
            update = Builders<Area>
                .Update
                .Set(x => x.ModifiedAt, DateTime.Now)
                .Pull(x => x.Children, data.NewItem);
            await _mainCollection
                .UpdateOneAsync(Filter.ById<Area>(parent), data.NewItem);
        }
    }

    public async Task RemoveChildren(int index, string parent)
    {
        Area? doc = GetOneBy(Filter.ById<Area>(parent));
        if (doc is null) return;
        UpdateDefinition<Area> update = Builders<Area>
            .Update
            .Set(x => x.ModifiedAt, DateTime.Now)
            .Set("parent", BsonNull.Value);
        await _mainCollection.UpdateOneAsync(Filter.ById<Area>(doc.Children[index]), update);
        update = Builders<Area>
            .Update
            .Set(x => x.ModifiedAt, DateTime.Now)
            .Pull(x => x.Children, doc.Children[index]);
        await _mainCollection.UpdateOneAsync(Filter.ById<Area>(parent), update);
    }

    public void ValidateChildren(List<UpdateList<string>>? data, string main)
    {
        if (data is null) return;
        data.ForEach(async (e) => {
            if (e.Operation != 1)
            {
                await NewChildren(e, main);
            } else 
            {
                await RemoveChildren(e.Key, main);
            }
        });
    }

    public async Task ValidateUpdate(UpdatedAreaDto data)
    {
        Area? old = await GetOneByAsync(Filter.ById<Area>(data.Id));
        if (old is null) return;
        if (data.Name is not null && !await NameIsUnique(data.Name, data.Parent))
            return;
        if (data.Parent is not null && !await ParentIsValid(data.Parent)) return;
        old.Update(data);
        ValidateChildren(data.Children, data.Id);

        UpdateDefinition<Area> update = Builders<Area>
            .Update
            .Set(x => x.ModifiedAt, DateTime.Now);
        if (data.Name is not null) update = update.Set(x => x.Name, data.Name);
        if (data.Parent is not null)
        {
            if (data.Parent != "root")
            {
                update = update.Set(x => x.Parent, data.Parent);
                UpdateDefinition<Area> pup;
                if (old.Parent is not null)
                {
                    // Removes area from old parent
                    pup = Builders<Area>
                        .Update
                        .Set(x => x.ModifiedAt, DateTime.Now)
                        .Pull(x => x.Children, data.Id);
                    await _mainCollection.UpdateOneAsync(Filter.ById<Area>(old.Id), pup);
                }
                // Adds data to new parent
                pup = Builders<Area>.Update
                    .Set(x => x.ModifiedAt, DateTime.Now)
                    .Push(x => x.Children, data.Id);
                await _mainCollection.UpdateOneAsync(Filter.ById<Area>(data.Parent), pup);
            } else 
            {
                update = update.Set("parent", BsonNull.Value);
                if (old.Parent is not null)
                {
                    UpdateDefinition<Area> pup = Builders<Area>
                        .Update
                        .Set(x => x.ModifiedAt, DateTime.Now)
                        .Pull(x => x.Children, data.Id);
                    await _mainCollection.UpdateOneAsync(Filter.ById<Area>(old.Id), pup);
                }
            }
        }
        if (data.Head is not null) update = update.Set(x => x.Head, data.Head);
        await _mainCollection.UpdateOneAsync(Filter.ById<Area>(data.Id), update);
    }

    public async Task CleanDependencies(string id)
    {
        FilterDefinition<Area> filter = Builders<Area>
            .Filter
            .Eq(x => x.Parent, id);
        UpdateDefinition<Area> update = Builders<Area>
            .Update
            .Set(x => x.ModifiedAt, DateTime.Now)
            .Set("parent", BsonNull.Value);
        await _mainCollection.UpdateManyAsync(filter, update);
        filter = Builders<Area>
            .Filter
            .ElemMatch(x => x.Children, id);
        update = Builders<Area>
            .Update
            .Set(x => x.ModifiedAt, DateTime.Now)
            .Pull(x => x.Children, id);
        await _mainCollection.UpdateManyAsync(filter, update);
    }

    public List<AreaDto> GetByMembers(List<string> data)
    {
        FilterDefinition<Area> filter = Builders<Area>
            .Filter
            .In(x => x.Id, data);
        List<AreaDto> result = new();
        _mainCollection
            .Find(filter)
            .ToList()
                .ForEach(e => {
                    result.Add(e.ToDto());
                });
        return result;
    }
}