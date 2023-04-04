using Fictichos.Constructora.Dto;
using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;
using MongoDB.Driver;

namespace Fictichos.Constructora.Auth;

public class TokenService
    : BaseService<TokenContainer, NewTokenDto, UpdatedTokenDto>
{
    private const string MAINCOLLECTION = "activeTokens";
    private List<string> Tokens { get; set; } = new();

    public TokenService(MongoSettings container)
        : base(container, MAINCOLLECTION)
    {
        
        IndexKeysDefinition<TokenContainer> indexKeysDefinition =
            Builders<TokenContainer>.IndexKeys.Ascending(x => x.Expires);
        CreateIndexOptions<TokenContainer> indexOptions =
            new()
            { ExpireAfter = TimeSpan.Zero };
        _mainCollection.Indexes.CreateOne(new CreateIndexModel<TokenContainer>(indexKeysDefinition, indexOptions));

    }

    public TokenContainer AddToken(string token)
    {
        NewTokenDto data = new() { Token = token };
        TokenContainer output = new TokenContainer().Instantiate(data);

        _mainCollection.InsertOne(output);
        return output;
    }
}