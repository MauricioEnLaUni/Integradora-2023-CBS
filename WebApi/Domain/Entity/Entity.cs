using Newtonsoft.Json;

namespace Fictichos.ERP.Domain;
/// <summary>
/// Base Entity for Models
/// </summary>
/// <param name="Id">Guid generated locally for new items.</param>
/// <param name="CreatedAt">When it was inserted in the database.</param>
/// <param name="ModifiedAt">Updates whenever the object does.</param>
internal class BaseEntity
{
    internal Guid Id { get; init; } = Guid.NewGuid();
    internal DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    internal DateTime ModifiedAt { get; private set; } = DateTime.UtcNow;

    internal BaseEntity() { }

    internal TModel? Deserialize<TModel>(string json)
        => JsonConvert.DeserializeObject<TModel>(json);

    internal string Serialize()
        => JsonConvert.SerializeObject(this);
}