using Fictichos.Constructora.Model;

namespace Fictichos.Constructora.Abstraction
{
    public interface IJwtProvider
    {
        public string Generate(User usr);
    }
}