namespace Fictichos.Constructora.Utilities
{
    public record UpdateList<T>
    {
        public int Operation { get; set; }
        public int Key { get; set; }
        public T? NewItem { get; set; }
    }

    public record IndexedObjectUpdate<T, U>
    {
        public int Operation { get; set; }
        public int Key { get; set; }
        public T? NewItem { get; set; }
        public U? UpdateItem { get; set; }
    }
}