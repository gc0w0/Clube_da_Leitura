using Clube_da_Leitura.Infra.Database.ModuloAmigo;
using Clube_da_Leitura.Infra.Database.ModuloCaixa;
using Clube_da_Leitura.Infra.Database.ModuloEmprestimo;
using Clube_da_Leitura.Infra.Database.ModuloReservas;
using Clube_da_Leitura.Infra.Database.ModuloRevista;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloReserva;
using Clube_da_Leitura.ModuloRevista;
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

            services.AddTransient<IRepositorioAmigo, RepositorioAmigoComDapper>();
            services.AddTransient<IRepositorioCaixa, RepositorioCaixaComDapper>();
            services.AddTransient<IRepositorioEmprestimo, RepositorioEmprestimoEmBancoDeDados>();
            services.AddTransient<IRepositorioReserva, RepositorioReservaEmBancoDeDados>();
            services.AddTransient<IRepositorioRevista, RepositorioRevistaEmBancoDeDados>();


        }
    }
}
