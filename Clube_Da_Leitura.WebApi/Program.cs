
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

namespace Clube_Da_Leitura.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connectionString = builder.Configuration.GetConnectionString("SqlServer");
            
            builder.Services.AddTransient<IDbConnection>(provider =>
            {
                return new SqlConnection(connectionString);
            });
            builder.Services.AddTransient<IRepositorioAmigo, RepositorioAmigoComDapper>();
            builder.Services.AddTransient<IRepositorioCaixa, RepositorioCaixaComDapper>();
            builder.Services.AddTransient<IRepositorioEmprestimo, RepositorioEmprestimoComDapper>();
            builder.Services.AddTransient<IRepositorioReserva, RepositorioReservaComDapper>();
            builder.Services.AddTransient<IRepositorioRevista, RepositorioRevistaComDapper>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
