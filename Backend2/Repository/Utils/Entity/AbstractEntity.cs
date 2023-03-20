using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Repository
{
    [BsonIgnoreExtraElements]
    /// <summary>
    /// Provides functionality for Update and Serialize Dto.< br/>
    /// Generates a base class for the Model Classes and sets the impĺementation for the Update and Serialization functions.
    /// </summary>
    /// <typeparam name="T">Stores itself, useful for the Instantiate method.</typeparam>
    /// <typeparam name="U">Stores Dto type for Mapping.</typeparam>
    /// <typeparam name="V">Dto used for instancing</typeparam>
    public abstract class AbstractEntity<T, U, V> : BaseEntity, IRepositoryMask<T, U, V>
    where T : AbstractEntity<T, U, V>, new()
    {
        /// <summary>
        /// Instantiates a new member from Json.
        /// Calls the private constructor of the class with the provided parameters, it is necessary to implement the Generic Create methods.<br />
        /// </summary>
        public abstract T Instantiate(string dto);

        /// <summary>
        /// Instantiates a new member from Dto.
        /// </summary>
        /// <remarks>
        /// Let's the class instantiates itself through a Dto, rather than a Json. Gives some leeway so the class can be instantiated within another method.
        /// </remarks>
        public abstract T Instantiate(V dto);

        /// <summary>
        /// Generates a general Dto
        /// </summary>
        /// <remarks>
        /// Generates a general Dto, class should be used for the most general
        /// operations.<br />
        /// A class may contain other methods for Dto generation.
        /// </remarks>
        public abstract U ToDto();

        /// <summary>
        /// Generates a JSON string from an object.
        /// </summary>
        public string SerializeDto()
        {
            U data = ToDto();
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Updates every field of a class.
        /// </summary>
        /// <param name="data">New values for the class, also includes Id for
        /// the outer method to get the instance to be updated.</param>
        /// <param name="ActionsCache">Includes all of the updatable properties and is used to iterate over them.</param>
        public void Update(IUpdateDto<T> data)
		{
			foreach(var actions in data.ActionsCache)
			{
				if(data.Changes[actions.Key] is not null)
				{
					actions.Value((T)this, data.Changes[actions.Key]);
				}
			}
		}

        /// <summary>
        /// Method that updates elements in a list. This method simply adds the
        /// new elements or modifies the element at the specified index so for
        /// objects' lists it is necessary for them to be instantiated before
        /// this method is run.
        /// </summary>
        public void UpdateList(IUpdateList data)
        {
            if (data is null) return;
            switch(data.Operation)
            {
                case 0 :
                    data.props.Add(data.NewItem!);
                    break;
                case 1:
                    data.props.RemoveAt(data.Key);
                    break;
                case 2:
                    data.props[data.Key] = data;
                    break;
                default:
                    break;
            }
        }
    }
}