using AutoMapper;
using Clube_da_Leitura.Infra.Database.ModuloReservas;
using Clube_da_Leitura.ModuloReserva;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloRevista;

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
            List<Amigo> amigos = repositorioAmigo.SelecionarTodos();
            List<Revista> revistas = repositorioRevista.SelecionarTodos();

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
