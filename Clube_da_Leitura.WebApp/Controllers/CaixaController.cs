using AutoMapper;
using Clube_da_Leitura.Infra.Database.ModuloCaixa;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Clube_da_Leitura.WebApp.Controllers
{
    public class CaixaController : Controller
    {
        private IRepositorioCaixa repositorioCaixa;
        private IMapper mapper;
        public CaixaController(IMapper mapper, IRepositorioCaixa repositorioCaixa)
        {
            this.repositorioCaixa = repositorioCaixa;
            this.mapper = mapper;
        }

       [HttpGet]
        public IActionResult Index()
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
        public IActionResult Create(CadastrarCaixaViewModel viewModel)
        {           
            var caixa = mapper.Map<Caixa>(viewModel);

            repositorioCaixa.InserirRegistro(caixa);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var caixa = repositorioCaixa.SelecionarPorId(id);

            var viewModel = mapper.Map<EditarCaixaViewModel>(caixa);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditarCaixaViewModel viewModel)
        {
            var caixa = mapper.Map<Caixa>(viewModel);

            repositorioCaixa.EditarRegistro(id, caixa);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var caixa = repositorioCaixa.SelecionarPorId(id);

            var viewModel = mapper.Map<ExcluirCaixaViewModel>(caixa);

            return View(viewModel);
        }

        [HttpPost("Delete")]
        public IActionResult DeleteConfirmado(int id)
        {            
            repositorioCaixa.ExcluirRegistro(id);

            return RedirectToAction("Index");
        }
    }
}
