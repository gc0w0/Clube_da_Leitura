using AutoMapper;
using Clube_da_Leitura.Infra.Database.ModuloRevista;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloRevista;
using Clube_da_Leitura.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.ComponentModel;

namespace Clube_da_Leitura.WebApp.Controllers
{
    public class RevistaController : Controller
    {
        private IRepositorioRevista repositorioRevista;
        private IRepositorioCaixa repositorioCaixa;
        private IMapper mapper;
        public RevistaController(IMapper mapper, IRepositorioRevista repositorioRevista, IRepositorioCaixa repositorioCaixa)
        {
            this.repositorioRevista = repositorioRevista;
            this.mapper = mapper;
            this.repositorioCaixa = repositorioCaixa;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Revista> revistas = repositorioRevista.SelecionarTodos();

            return View("Index", revistas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var caixas = repositorioCaixa.SelecionarTodos(); 
            var viewModel = new CadastrarRevistaViewModel(caixas);


            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CadastrarRevistaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.CaixasDisponiveis = repositorioCaixa
                    .SelecionarTodos()
                    .Select(c => new SelectListItem(c.Etiqueta, c.Id.ToString()))
                    .ToList();

                return View(viewModel);
            }

            var caixa = repositorioCaixa.SelecionarPorId(viewModel.CaixaId);

            var revista = mapper.Map<Revista>(viewModel);
            revista.Caixa = caixa; 

            repositorioRevista.InserirRegistro(revista);

            return RedirectToAction("Index");
        }




        [HttpGet]
        public IActionResult Edit(int id)
        {
            var revista = repositorioRevista.SelecionarPorId(id);

            var viewModel = mapper.Map<EditarRevistaViewModel>(revista);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditarRevistaViewModel viewModel)
        {
            var revista = mapper.Map<Revista>(viewModel);

            repositorioRevista.EditarRegistro(id, revista);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var revista = repositorioRevista.SelecionarPorId(id);

            var viewModel = mapper.Map<ExcluirRevistaViewModel>(revista);

            return View(viewModel);
        }

        [HttpPost("Delete")]
        public IActionResult DeleteConfirmado(int id)
        {
            repositorioRevista.ExcluirRegistro(id);

            return RedirectToAction("Index");
        }

    }
}
