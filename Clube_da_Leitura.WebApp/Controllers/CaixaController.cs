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

        [HttpGet]
        public IActionResult Index() //action, ação
        {
            List<Caixa> caixas = repositorioCaixa.SelecionarTodos();

            return View("Index", caixas);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Caixa caixa)

        {   repositorioCaixa.InserirRegistro(caixa);
            return RedirectToAction("Index");
        }
    }
}
