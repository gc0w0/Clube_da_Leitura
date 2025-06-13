using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Clube_da_Leitura.ModuloCaixa;

namespace Clube_da_Leitura.ModuloRevista
{
    public class RepositorioRevistaEmArquivo : IRepositorioRevista
    {
        protected List<Revista> registros = new List<Revista>();
        public void InserirRegistro(Revista registro)
        {
            registro.id = registros.Count + 1;
            registros.Add(registro);
            var opcoes = new JsonSerializerOptions();
            opcoes.IncludeFields = true;
            opcoes.ReferenceHandler = ReferenceHandler.Preserve;
            string conteudoArquivo = JsonSerializer.Serialize(registros, opcoes);
            File.WriteAllText(@"C:\temp\revistas.json", conteudoArquivo);
        }
        public bool EditarRegistro(int id, Revista registroAtualizado)
        {
            throw new NotImplementedException();
        }

        public bool ExcluirRegistro(int id)
        {
            throw new NotImplementedException();
        }

        public Revista SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Revista> SelecionarTodos()
        {
            var opcoes = new JsonSerializerOptions();
            opcoes.IncludeFields = true;
            opcoes.ReferenceHandler = ReferenceHandler.Preserve;
            var conteudo = File.ReadAllText(@"C:\temp\revistas.json");
            var revistas = JsonSerializer.Deserialize<List<Revista>>(conteudo, opcoes);
            return revistas;
        }

        public bool Validacoes(Func<Revista, bool> validacao)
        {
            throw new NotImplementedException();
        }
    }
}
