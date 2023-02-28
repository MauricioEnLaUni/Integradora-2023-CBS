namespace Fictichos.Constructora.Models
{
    public interface IUpdater<T>
    {
        public static List<string> CanUpdate(T data)
        {
            throw new NotImplementedException("Need to implement this.");
        }
    }
}