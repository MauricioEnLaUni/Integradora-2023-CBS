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
            IUpdateList<T> data)
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
    }
}