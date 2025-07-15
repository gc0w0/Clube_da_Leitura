using AutoMapper;
using Clube_da_Leitura.Dominio.ModuloEmprestimo;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloRevista;
using Clube_da_Leitura.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace Clube_da_Leitura.WebApp.Controllers
{
    public class EmprestimoController : Controller
    {   private IRepositorioEmprestimo repositorioEmprestimo;
        private IRepositorioAmigo repositorioAmigo;
        private IRepositorioRevista repositorioRevista;
        private IMapper mapper;

        public EmprestimoController(IMapper mapper, IRepositorioEmprestimo repositorioEmprestimo, IRepositorioAmigo repositorioAmigo, IRepositorioRevista repositorioRevista)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
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
            var amigos = repositorioAmigo.SelecionarTodos();
            var revistas = repositorioRevista.SelecionarTodos();
            var viewModel = new CadastrarEmprestimoViewModel(amigos, revistas);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CadastrarEmprestimoViewModel viewModel)
        {
            if (ModelState.IsValid == false)
            {
                viewModel.RevistasDisponiveis = repositorioRevista
                    .SelecionarTodos()
                    .Select(r => new SelectListItem(r.Titulo, r.Id.ToString()))
                    .ToList();
                viewModel.AmigosDisponiveis = repositorioAmigo
                    .SelecionarTodos()
                    .Select(a => new SelectListItem(a.Nome, a.Id.ToString()))
                    .ToList();

                return View(viewModel);
            }

            var amigo = repositorioAmigo.SelecionarPorId(viewModel.AmigoId);
            var revista = repositorioRevista.SelecionarPorId(viewModel.RevistaId);

            var emprestimo = mapper.Map<Emprestimo>(viewModel);
            emprestimo.Amigo = amigo;
            emprestimo.Revista = revista;

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

        [HttpGet]
        public IActionResult RegistrarDevolucao(int id)
        {
            var emprestimo = repositorioEmprestimo.SelecionarPorId(id);

            if (emprestimo == null || emprestimo.Situacao != SituacaoEmprestimo.Aberto)
                return NotFound();

            var viewModel = new EditarEmprestimoViewModel
            {
                Id = emprestimo.Id,
                AmigoId = emprestimo.Amigo?.Id ?? 0,
                RevistaId = emprestimo.Revista?.Id ?? 0,
                NomeAmigo = emprestimo.Amigo?.Nome ?? "Desconhecido",
                TituloRevista = emprestimo.Revista?.Titulo ?? "Desconhecida",
                DataEmprestimo = emprestimo.DataEmprestimo,
                Situacao = emprestimo.Situacao
            };

            return View("RegistrarDevolucao", viewModel);
        }

        [HttpPost]
        public IActionResult RegistrarDevolucaoConfirmado(EditarEmprestimoViewModel viewModel)
        {
            var emprestimo = repositorioEmprestimo.SelecionarPorId(viewModel.Id);

            if (emprestimo == null)
                return NotFound();

            int revistaId = viewModel.RevistaId;

            if (revistaId == 0)
                return BadRequest("ID da revista não encontrado.");

            var revistaCompleta = repositorioRevista.SelecionarPorId(revistaId);

            if (revistaCompleta == null)
                return NotFound("Revista não encontrada.");

            emprestimo.DataDevolucao = viewModel.DataDevolucao;
            emprestimo.Situacao = SituacaoEmprestimo.Fechado;

            revistaCompleta.Status = Revista.StatusDisponveis.Disponivel;

            repositorioRevista.EditarRegistro(revistaCompleta.Id, revistaCompleta);
            repositorioEmprestimo.EditarRegistro(emprestimo.Id, emprestimo);

            return RedirectToAction("Index");
        }

    }
}
