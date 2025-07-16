using Clube_da_Leitura.ModuloAmigo;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clube_Da_Leitura.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmigoController : ControllerBase
    {
        public IRepositorioAmigo repositorioAmigo { get; set; }

        // GET: api/<AmigoController>
        [HttpGet]
        public IEnumerable<Amigo> Get()
        {
            return repositorioAmigo.SelecionarTodos();
        }
        public AmigoController(IRepositorioAmigo repositorioAmigo)
        {
            this.repositorioAmigo = repositorioAmigo;
        }

        // GET api/<AmigoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AmigoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AmigoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AmigoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
