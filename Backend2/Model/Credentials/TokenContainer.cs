using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Dto;
using Newtonsoft.Json;

namespace Fictichos.Constructora.Auth;

public class TokenContainer
    : BaseEntity, IQueryMask<TokenContainer, NewTokenDto, UpdatedTokenDto>
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
    public bool Active { get; set; }
    public TokenContainer() { }

    private TokenContainer(NewTokenDto data)
    {
        Token = data.Token;
        Expires = CreatedAt.AddMinutes(30);
        Active = true;
    }

    public TokenContainer Instantiate(NewTokenDto data)
    {
        return new(data);
    }

    public TokenDto ToDto()
    {
        return new()
        {
            Id = Id,
            Token = Token,
            Expires = Expires
        };
    }

    public string Serialize()
    {
        TokenDto data = ToDto();
        return JsonConvert.SerializeObject(data);
    }

    public void Update(UpdatedTokenDto data)
    {
        
    }
}