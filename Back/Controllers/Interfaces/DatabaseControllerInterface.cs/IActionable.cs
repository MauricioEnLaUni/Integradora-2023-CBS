using MongoDB.Bson;

namespace Fictichos.Constructora.Models
{
    public interface IActionable<T, U, V>
    {
        public T Insert(U newData);
        public T Update(ObjectId id, U newData);
        public T Delete(ObjectId id);
    }
}