using Clube_da_Leitura.Infra.Database.ModuloRevista;
using Clube_da_Leitura.ModuloRevista;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Clube_da_Leitura.WebApp.Controllers
{
    public class RevistaController : Controller
    {
        private IRepositorioRevista repositorioRevista;

        public RevistaController( )
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
            var conection = new SqlConnection(connectionString);
            this.repositorioRevista = new RepositorioRevistaEmBancoDeDados(conection);
        }

        public IActionResult Index()
        {   var revistas = repositorioRevista.SelecionarTodos();
            return View(revistas);
        }
    }
}
