using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;
using MongoDB.Driver;

namespace Fictichos.Constructora.Auth;

public class TokenService
    : BaseService<TokenContainer, NewTokenDto, UpdatedTokenDto>
{
    private const string MAINCOLLECTION = "activeTokens";
    private const string SECONDARYCOLLECTION = "deadTokens";
    private List<string> Tokens { get; set; } = new();
    private readonly IMongoCollection<TokenContainer> _deadTokens;

    public TokenService(MongoSettings container)
        : base(container, MAINCOLLECTION)
    {
        _deadTokens = container.Client.GetDatabase("cbs")
            .GetCollection<TokenContainer>(SECONDARYCOLLECTION);
    }

    public TokenContainer AddToken(string token)
    {
        NewTokenDto data = new() { Token = token };
        TokenContainer output = new TokenContainer().Instantiate(data);

        Tokens.Add(token);
        _mainCollection.InsertOne(output);
        return output;
    }
}