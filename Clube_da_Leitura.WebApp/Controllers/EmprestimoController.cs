using Clube_da_Leitura.Infra.Database.ModuloEmprestimo;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloRevista;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Clube_da_Leitura.WebApp.Controllers
{
    public class EmprestimoController : Controller
    {   private IRepositorioEmprestimo repositorioEmprestimo;
        private IRepositorioAmigo repositorioAmigo;
        private IRepositorioRevista repositorioRevista;
        public EmprestimoController( )
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
            var conection = new SqlConnection(connectionString);
            this.repositorioEmprestimo = new RepositorioEmprestimoEmBancoDeDados(conection);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
            var conection = new SqlConnection(connectionString);
            this.repositorioEmprestimo = new RepositorioEmprestimoEmBancoDeDados(conection);
            
            List<Emprestimo> emprestimos = repositorioEmprestimo.SelecionarTodos();

            return View("Index", emprestimos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Emprestimo emprestimo)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
            var conection = new SqlConnection(connectionString);
            this.repositorioEmprestimo = new RepositorioEmprestimoEmBancoDeDados(conection);

            repositorioEmprestimo.InserirRegistro(emprestimo);

            return RedirectToAction("Index");
        }
    }
}
