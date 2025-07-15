using AutoMapper;
using Clube_da_Leitura.Infra.Database.ModuloReservas;
using Clube_da_Leitura.ModuloReserva;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloRevista;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clube_da_Leitura.WebApp.Controllers
{
    public class ReservaController : Controller
    {
        private IRepositorioReserva repositorioReserva;
        private IRepositorioAmigo repositorioAmigo;
        private IRepositorioRevista repositorioRevista;
        private IMapper mapper;
        public ReservaController(IMapper mapper, IRepositorioReserva repositorioReserva, IRepositorioAmigo repositorioAmigo, IRepositorioRevista repositorioRevista)
        {
            this.repositorioReserva = repositorioReserva;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Reserva> reservas = repositorioReserva.SelecionarTodos();

            return View("Index", reservas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var amigos = repositorioAmigo.SelecionarTodos();
            var revistas = repositorioRevista.SelecionarTodos();
            var viewModel = new CadastrarReservaViewModel(amigos, revistas);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CadastrarReservaViewModel viewModel)
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

            var reserva = mapper.Map<Reserva>(viewModel);
            reserva.Amigo = amigo;
            reserva.Revista = revista;

            repositorioReserva.InserirRegistro(reserva);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var reserva = repositorioReserva.SelecionarPorId(id);

            var viewModel = mapper.Map<EditarReservaViewModel>(reserva);

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

        [HttpPost]
        public IActionResult Edit(int id, EditarReservaViewModel viewModel)
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
            var reserva = mapper.Map<Reserva>(viewModel);
            var amigo = repositorioAmigo.SelecionarPorId(viewModel.AmigoId);
            var revista = repositorioRevista.SelecionarPorId(viewModel.RevistaId);

            reserva.Amigo = amigo;
            reserva.Revista = revista;

            repositorioReserva.EditarRegistro(id, reserva);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var reserva = repositorioReserva.SelecionarPorId(id);
            var viewModel = mapper.Map<ExcluirReservaViewModel>(reserva);

            return View(viewModel);
        }

        [HttpPost("Delete")]
        public IActionResult DeleteConfirmado(int id)
        {
            repositorioReserva.ExcluirRegistro(id);

            return RedirectToAction("Index");
        }
    }
}
