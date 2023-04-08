using Microsoft.Extensions.Options;

using Fictichos.Constructora.Auth;

namespace Fictichos.Constructora.Options;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;
    private const string SECTIONNAME = "JWT:ACCESS";

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SECTIONNAME).Bind(options);
    }
}