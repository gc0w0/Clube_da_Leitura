using AutoMapper;
using Clube_da_Leitura.Infra.Database.ModuloEmprestimo;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Clube_da_Leitura.WebApp.Controllers
{
    public class EmprestimoController : Controller
    {   private IRepositorioEmprestimo repositorioEmprestimo;
        private IMapper mapper;

        public EmprestimoController(IMapper mapper, IRepositorioEmprestimo repositorioEmprestimo)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Emprestimo> emprestimos = repositorioEmprestimo.SelecionarTodos();

            return View("Index", emprestimos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CadastrarEmprestimoViewModel viewModel)
        {
            var emprestimo = mapper.Map<Emprestimo>(viewModel);

            repositorioEmprestimo.InserirRegistro(emprestimo);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var emprestimo = repositorioEmprestimo.SelecionarPorId(id);

            var viewModel = mapper.Map<EditarEmprestimoViewModel>(emprestimo);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditarEmprestimoViewModel viewModel)
        {
            var emprestimo = mapper.Map<Emprestimo>(viewModel);

            repositorioEmprestimo.EditarRegistro(id, emprestimo);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var emprestimo = repositorioEmprestimo.SelecionarPorId(id);

            var viewModel = mapper.Map<ExcluirEmprestimoViewModel>(emprestimo);

            return View(viewModel);
        }

        [HttpPost("Delete")]
        public IActionResult DeleteConfirmado(int id)
        {
            repositorioEmprestimo.ExcluirRegistro(id);

            return RedirectToAction("Index");
        }

    }
}
