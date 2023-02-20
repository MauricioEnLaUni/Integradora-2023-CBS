using dotenv.net;

namespace Fictichos.Constructora.Utilities
{
    public record DotEnvManager
    {
        public IDictionary<string, string> env { get; init; }
        public DotEnvManager()
        {
            DotEnv.Load();
            this.env = DotEnv.Read();
        }
    }
}