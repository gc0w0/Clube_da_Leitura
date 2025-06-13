using Clube_da_Leitura.ModuloCaixa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloReservas
{
    public class RepositorioReservaEmArquivo : IRepositorioReserva
    {
        protected List<Reserva> registros = new List<Reserva>();
        public void InserirRegistro(Reserva registro)
        {

            registro.id = registros.Count + 1;
            registros.Add(registro);
            var opcoes = new JsonSerializerOptions();
            opcoes.IncludeFields = true;
            opcoes.ReferenceHandler = ReferenceHandler.Preserve;
            string conteudoArquivo = JsonSerializer.Serialize(registros, opcoes);
            File.WriteAllText(@"C:\temp\reservas.json", conteudoArquivo);
        }
        public bool EditarRegistro(int id, Reserva registroAtualizado)
        {
            throw new NotImplementedException();
        }

        public bool ExcluirRegistro(int id)
        {
            throw new NotImplementedException();
        }


        public Reserva SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Reserva> SelecionarTodos()
        {
            var opcoes = new JsonSerializerOptions();
            opcoes.IncludeFields = true;
            opcoes.ReferenceHandler = ReferenceHandler.Preserve;
            var conteudo = File.ReadAllText(@"C:\temp\reservas.json");
            var reservas = JsonSerializer.Deserialize<List<Reserva>>(conteudo, opcoes);
            return reservas;
        }

        public List<Reserva> SelecionarTodosAbertos()
        {
            return SelecionarTodos()
               .Where(r => r.situacao == SituacaoReserva.Ativa)
               .ToList();
        }

        public bool Validacoes(Func<Reserva, bool> validacao)
        {
            throw new NotImplementedException();
        }
    }
}
