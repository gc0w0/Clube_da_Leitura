using Clube_da_Leitura.ModuloAmigo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloCaixa
{
    public class RepositorioCaixaEmArquivo : IRepositorioCaixa
    {
        protected List<Caixa> registros = new List<Caixa>();
        public void InserirRegistro(Caixa registro)
        {
            registro.id = registros.Count + 1;
            registros.Add(registro);
            var opcoes = new JsonSerializerOptions();
            opcoes.IncludeFields = true;
            opcoes.ReferenceHandler = ReferenceHandler.Preserve;
            string conteudoArquivo = JsonSerializer.Serialize(registros, opcoes);
            File.WriteAllText(@"C:\temp\caixas.json", conteudoArquivo);
        }
        public bool EditarRegistro(int id, Caixa registroAtualizado)
        {
            throw new NotImplementedException();
        }

        public bool ExcluirRegistro(int id)
        {
            throw new NotImplementedException();
        }


        public Caixa SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Caixa> SelecionarTodos()
        {
            var opcoes = new JsonSerializerOptions();
            opcoes.IncludeFields = true;
            opcoes.ReferenceHandler = ReferenceHandler.Preserve;
            var conteudo = File.ReadAllText(@"C:\temp\caixas.json");
            var caixas = JsonSerializer.Deserialize<List<Caixa>>(conteudo, opcoes);
            return caixas;
        }

        public bool Validacoes(Func<Caixa, bool> validacao)
        {
            throw new NotImplementedException();
        }
    }
}
