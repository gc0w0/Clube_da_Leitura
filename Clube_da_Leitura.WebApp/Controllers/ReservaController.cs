using Clube_da_Leitura.Infra.Database.ModuloAmigo;
using Clube_da_Leitura.Infra.Database.ModuloReservas;
using Clube_da_Leitura.Infra.Database.ModuloRevista;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloReservas;
using Clube_da_Leitura.ModuloRevista;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Clube_da_Leitura.WebApp.Controllers
{
    public class ReservaController : Controller
    {
        private IRepositorioReserva repositorioReserva;
        private IRepositorioAmigo repositorioAmigo;
        private IRepositorioRevista repositorioRevista;
        public ReservaController()
        {
            
        }

        [HttpGet]
        public IActionResult Index()
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
            var connection = new SqlConnection(connectionString);

            this.repositorioReserva = new RepositorioReservaEmBancoDeDados(connection);
            this.repositorioAmigo = new RepositorioAmigoEmBancoDeDados(connection);
            this.repositorioRevista = new RepositorioRevistaEmBancoDeDados(connection);

            List<Reserva> reservas = repositorioReserva.SelecionarTodos();
            List<Amigo> amigos = repositorioAmigo.SelecionarTodos();
            List<Revista> revistas = repositorioRevista.SelecionarTodos();

            return View("Index", reservas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Reserva reserva)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
            var conection = new SqlConnection(connectionString);

            this.repositorioReserva = new RepositorioReservaEmBancoDeDados(conection);

            repositorioReserva.InserirRegistro(reserva);

            return RedirectToAction("Index");
        }
    }
}
