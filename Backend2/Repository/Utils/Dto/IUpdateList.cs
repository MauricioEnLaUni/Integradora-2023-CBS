namespace Fictichos.Constructora.Utilities
{
    public interface IUpdateList<T>
    {
        public int Operation { get; set; }
        public int Key { get; set; }
        public T? NewItem { get; set; }
    }

    public interface IUpdateObjectList<T>
    {
        public int Operation { get; set; }
        public dynamic Key { get; set; }
        public T NewItem { get; set; }
    }
}