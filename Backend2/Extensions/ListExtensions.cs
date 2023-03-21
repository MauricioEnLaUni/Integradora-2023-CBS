using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;

namespace Fictichos.Constructora.Utilities
{
    public static class EnumExtension
    {
        /// <summary>
        /// Returns an index to be used on ForEach methods.
        /// </summary>
        public static IEnumerable<(T item, int index)>
            WithIndex<T>(this IEnumerable<T> self)       
                => self.Select((item, index) => (item, index));
        
        /// <summary>
        /// Method that updates elements in a list. This method simply adds 
        /// or modifies the element at the specified index.
        /// For objects' lists it is necessary for them to be instantiated
        /// before this method is run.
        /// </summary>
        public static void UpdateWithIndex<T>(
            this List<T> props,
            UpdateList<T> data)
        {
            if (data is null) return;
            switch(data.Operation)
            {
                case 0 :
                    props.Add(data.NewItem!);
                    break;
                case 1:
                    props.RemoveAt(data.Key);
                    break;
                case 2:
                    props[data.Key] = data.NewItem!;
                    break;
                default:
                    break;
            }
        }

        public static void UpdateObjectWithIndex<T, U, V, W>(
            this List<T> props,
            IndexedObjectUpdate<U, V> data
        )
        where T : BaseEntity, IQueryMask<T, U, V, W>, new()
        where V : DtoBase
        {
            if (data is null) return;
            switch(data.Operation)
            {
                case 0 :
                    if (data.NewItem is null) return;
                    if (props is null) props = new();
                    T newItem = new T().Instantiate(data.NewItem);
                    props.Add(newItem);
                    break;
                case 1:
                    if (props is null) return;
                    props.RemoveAt(data.Key);
                    break;
                case 2:
                    if (data.UpdateItem is null) return;
                    if (props is null) return;
                    props[data.Key].Update(data.UpdateItem);
                    break;
                default:
                    break;
            }
        }
    }
}