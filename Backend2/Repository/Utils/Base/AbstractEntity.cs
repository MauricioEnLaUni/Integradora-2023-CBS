using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Repository
{
    /// <summary>
    /// Provides functionality for Update and Serialize Dto.
    /// </summary>
    /// <remarks>
    /// Generates a base class for the Model Classes and sets the impĺementation for the Update and Serialization functions.
    ///</remarks>
    [BsonIgnoreExtraElements]
    public abstract class AbstractEntity<T, U, V, W> : BaseEntity
    where T : BaseEntity, IRepositoryMask<T, U, V, W>
    where V : UpdatableDto
    {
        /// <summary>
        /// Instantiates a new member from Json.
        /// </summary>
        /// <remarks>
        /// Calls the private constructor of the class with the provided parameters, it is necessary to implement the Generic Create methods.<br />
        /// <see cref="RepositoryAsync{T, U, V}" />
        /// </remarks>
        public abstract T Instantiate(string dto);
        /// <summary>
        /// Instantiates a new member from Dto.
        /// </summary>
        /// <remarks>
        /// Let's the class instantiates itself through a Dto, rather than a Json. Gives some leeway so the class can be instantiated within another method.
        /// </remarks>
        public abstract T Instantiate(W dto);

        public abstract U ToDto();

        public string SerializeDto()
        {
            U data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        public void Update(V data, Dictionary<string, dynamic> ActionsCache)
		{
			foreach(var actions in ActionsCache)
			{
				if(data.Changes[actions.Key] is not null)
				{
					actions.Value(this, data.Changes[actions.Key]);
				}
			}
		}
    }
}