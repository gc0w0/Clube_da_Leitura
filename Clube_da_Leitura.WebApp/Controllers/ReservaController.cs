using AutoMapper;
using Clube_da_Leitura.Infra.Database.ModuloReservas;
using Clube_da_Leitura.ModuloReserva;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Clube_da_Leitura.WebApp.Controllers
{
    public class ReservaController : Controller
    {
        private IRepositorioReserva repositorioReserva;
        private IMapper mapper;
        public ReservaController(IMapper mapper, IRepositorioReserva repositorioReserva)
        {
            this.repositorioReserva = repositorioReserva;
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
            return View();
        }

        [HttpPost]
        public IActionResult Create(CadastrarReservaViewModel viewModel)
        {
            var reserva = mapper.Map<Reserva>(viewModel);

            repositorioReserva.InserirRegistro(reserva);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var reserva = repositorioReserva.SelecionarPorId(id);

            var viewModel = mapper.Map<EditarReservaViewModel>(reserva);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditarReservaViewModel viewModel)
        {
            var reserva = mapper.Map<Reserva>(viewModel);

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
