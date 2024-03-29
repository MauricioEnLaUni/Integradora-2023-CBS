using dotenv.net;

namespace Fictichos.Constructora.Utilities
{
    public record DotEnvManager
    {
        public IDictionary<string, string> Env { get; init; }
        public DotEnvManager()
        {
            DotEnv.Load();
            Env = DotEnv.Read();
        }
    }

    public static class Constants
    {
        public const string COOKIENAME = "LachterCookie";
    }
}