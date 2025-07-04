using Clube_da_Leitura.Infra.Database.ModuloAmigo;
using Clube_da_Leitura.Infra.Database.ModuloCaixa;
using Clube_da_Leitura.Infra.Database.ModuloRevista;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloRevista;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Clube_da_Leitura.WebApp.Controllers
{
    public class RevistaController : Controller
    {
        private IRepositorioRevista repositorioRevista;
        private IRepositorioCaixa repositorioCaixa;
        public RevistaController()
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
            var conection = new SqlConnection(connectionString);

            this.repositorioRevista = new RepositorioRevistaEmBancoDeDados(conection);
            this.repositorioCaixa = new RepositorioCaixaEmBancoDeDados(conection);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
            var conection = new SqlConnection(connectionString);

            this.repositorioRevista = new RepositorioRevistaEmBancoDeDados(conection);
            this.repositorioCaixa = new RepositorioCaixaEmBancoDeDados(conection);

            List<Revista> revistas = repositorioRevista.SelecionarTodos();
            List<Caixa> caixas = repositorioCaixa.SelecionarTodos();
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
            this.repositorioCaixa = new RepositorioCaixaEmBancoDeDados(conection);

            repositorioRevista.InserirRegistro(revista);

            return RedirectToAction("Index");
        }
    }
}
