using Fictichos.Constructora.Dto;

namespace Fictichos.Constructora.Abstraction
{
    public interface IJwtProvider
    {
        public string Generate(LoginResponseDto usr);
    }
}