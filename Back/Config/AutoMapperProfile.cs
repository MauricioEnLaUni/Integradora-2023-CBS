using AutoMapper;

namespace Fictichos.Constructora.Utils.AutoMapper
{
    public class AutoMapperProfile<T, U> : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<T, U>();
        }
    }
}