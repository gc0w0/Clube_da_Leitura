using Clube_da_Leitura.Infra.Database.ModuloEmprestimo;
using Clube_da_Leitura.ModuloEmprestimo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Clube_da_Leitura.WebApp.Controllers
{
    public class EmprestimoController : Controller
    {   private IRepositorioEmprestimo repositorioEmprestimo;

        public EmprestimoController( )
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
            var conection = new SqlConnection(connectionString);
            this.repositorioEmprestimo = new RepositorioEmprestimoEmBancoDeDados(conection);
        }

        public IActionResult Index()
        {
            var emprestimos = repositorioEmprestimo.SelecionarTodos();
            return View(emprestimos);
        }
    }
}
