using Clube_da_Leitura.Infra.Database.ModuloReservas;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloReservas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Clube_da_Leitura.WebApp.Controllers
{
    public class ReservaController : Controller
    {
        private IRepositorioReserva repositorioReserva;
        public ReservaController()
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
            var conection = new SqlConnection(connectionString);

            this.repositorioReserva = new RepositorioReservaEmBancoDeDados(conection);
        }
        public IActionResult Index()
        {   var reservas = repositorioReserva.SelecionarTodos();
            return View(reservas);
        }
    }
}
