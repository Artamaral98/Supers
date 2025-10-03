using Microsoft.Extensions.Configuration;

namespace Supers.Infrastructure.Extensoes
{
    public static class Configuracoes
    {
        public static string ConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("Connection")!;
        }
    }
}
