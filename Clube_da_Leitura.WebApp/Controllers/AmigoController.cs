using AutoMapper;
using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.Infra.Database.ModuloAmigo;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Clube_da_Leitura.WebApp.Controllers
{
    
    public class AmigoController : Controller
    {
        private IRepositorioAmigo repositorioAmigo;
        private IMapper mapper;

        public AmigoController(IMapper mapper, IRepositorioAmigo repositorioAmigo)
        {            
            this.repositorioAmigo = repositorioAmigo;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {            
            List<Amigo> amigos = repositorioAmigo.SelecionarTodos();

            return View("Index", amigos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CadastrarAmigoViewModel viewModel)
        {           
            var amigo = mapper.Map<Amigo>(viewModel);

            repositorioAmigo.InserirRegistro(amigo);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var amigo = repositorioAmigo.SelecionarPorId(id);

            var viewModel = mapper.Map<EditarAmigoViewModel>(amigo);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditarAmigoViewModel viewModel)
        {
            var amigo = mapper.Map<Amigo>(viewModel);

            repositorioAmigo.EditarRegistro(id, amigo);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var amigo = repositorioAmigo.SelecionarPorId(id);

            var multa = new ModuloMultas.Multa();
            multa.valorMulta = 10;
            multa.situacao = Dominio.ModuloMultas.SituacaoMulta.Quitada;

            amigo.Multas.Add(multa);

            var viewModel = mapper.Map<ExcluirAmigoViewModel>(amigo);

            return View(viewModel);
        }

        [HttpPost("Delete")]
        public IActionResult DeleteConfirmado(int id)
        {            
            repositorioAmigo.ExcluirRegistro(id);

            return RedirectToAction("Index");
        }

    }
}
