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
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
            var conection = new SqlConnection(connectionString);

            this.repositorioAmigo = new RepositorioAmigoEmBancoDeDados(conection);
        }

        public IActionResult Index()
        {   var amigos = repositorioAmigo.SelecionarTodos();
            return View(amigos);
        }
    }
}
