using Clube_da_Leitura.Infra.Database.ModuloAmigo;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.WebApp.Config;
using Clube_da_Leitura.WebApp.Profiles;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Clube_da_Leitura.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddAutoMapper();

        builder.Services.AddDependencyServices(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}
