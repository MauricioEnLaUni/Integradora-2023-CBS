namespace Fictichos.Constructora.Utilities
{
    public interface IUpdateList
    {
        public int Operation { get; set; }
        public int Key { get; set; }
        public dynamic? NewItem { get; set; }
        List<dynamic> props { get; set; }
    }
}