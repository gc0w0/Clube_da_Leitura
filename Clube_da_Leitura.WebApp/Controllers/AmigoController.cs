using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.Infra.Database.ModuloAmigo;
using Clube_da_Leitura.ModuloAmigo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Clube_da_Leitura.WebApp.Controllers
{
    
    public class AmigoController : Controller
    {
        private IRepositorioAmigo repositorioAmigo;
        
        public AmigoController()
        {
                       
        }

        [HttpGet]
        public IActionResult Index()
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
            var conection = new SqlConnection(connectionString);

            this.repositorioAmigo = new RepositorioAmigoEmBancoDeDados(conection);

            List<Amigo> amigos = repositorioAmigo.SelecionarTodos();

            return View("Index", amigos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Amigo amigo)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
            var conection = new SqlConnection(connectionString);

            this.repositorioAmigo = new RepositorioAmigoEmBancoDeDados(conection);

            repositorioAmigo.InserirRegistro(amigo);

            return RedirectToAction("Index");
        }
    }
}
