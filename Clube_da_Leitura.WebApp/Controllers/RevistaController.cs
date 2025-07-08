using AutoMapper;
using Clube_da_Leitura.Infra.Database.ModuloRevista;
using Clube_da_Leitura.ModuloRevista;
using Clube_da_Leitura.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Clube_da_Leitura.WebApp.Controllers
{
    public class RevistaController : Controller
    {
        private IRepositorioRevista repositorioRevista;
        private IMapper mapper;
        public RevistaController(IMapper mapper, IRepositorioRevista repositorioRevista)
        {
            this.repositorioRevista = repositorioRevista;
            this.mapper = mapper;
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
            return View();
        }

        [HttpPost]
        public IActionResult Create(CadastrarRevistaViewModel viewModel)
        {
            var revista = mapper.Map<Revista>(viewModel);

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
