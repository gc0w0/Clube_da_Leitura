using Clube_da_Leitura.Infra.Database.ModuloAmigo;
using Clube_da_Leitura.Infra.Database.ModuloRevista;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloRevista;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Clube_da_Leitura.WebApp.Controllers
{
    public class RevistaController : Controller
    {
        private IRepositorioRevista repositorioRevista;

        public RevistaController()
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
            var conection = new SqlConnection(connectionString);
            this.repositorioRevista = new RepositorioRevistaEmBancoDeDados(conection);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
            var conection = new SqlConnection(connectionString);

            this.repositorioRevista = new RepositorioRevistaEmBancoDeDados(conection);

            List<Revista> revistas = repositorioRevista.SelecionarTodos();
            return View("Index", revistas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Revista revista)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
            var conection = new SqlConnection(connectionString);
            this.repositorioRevista = new RepositorioRevistaEmBancoDeDados(conection);

            repositorioRevista.InserirRegistro(revista);

            return RedirectToAction("Index");
        }
    }
}
