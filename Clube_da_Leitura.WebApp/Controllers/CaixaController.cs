using Clube_da_Leitura.Infra.Database.ModuloCaixa;
using Clube_da_Leitura.ModuloCaixa;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Clube_da_Leitura.WebApp.Controllers
{
    public class CaixaController : Controller
    {
        private IRepositorioCaixa repositorioCaixa;

        public CaixaController()
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
            var conection = new SqlConnection(connectionString);
                 
            this.repositorioCaixa = new RepositorioCaixaEmBancoDeDados(conection);
        }

        public IActionResult Index() //action, ação
        {
            var caixas = repositorioCaixa.SelecionarTodos();

            return View(caixas);
        }
    }
}
