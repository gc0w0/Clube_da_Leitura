using Clube_da_Leitura.Infra.Database.ModuloAmigo;
using Clube_da_Leitura.ModuloAmigo;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Clube_da_Leitura.WebApp.Config
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyServices(this IServiceCollection services, IConfiguration config) //extension methods
        {
            string connectionString = config.GetConnectionString("SqlServer")!;

            services.AddTransient<IDbConnection>(provider =>
            {
                return new SqlConnection(connectionString);
            });

            services.AddTransient<IRepositorioAmigo, RepositorioAmigoEmBancoDeDados>();
        }
    }
}
