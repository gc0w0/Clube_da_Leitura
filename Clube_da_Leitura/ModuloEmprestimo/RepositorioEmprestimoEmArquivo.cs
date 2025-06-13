using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Clube_da_Leitura.ModuloAmigo;

namespace Clube_da_Leitura.ModuloEmprestimo
{
    public class RepositorioEmprestimoEmArquivo : IRepositorioEmprestimo
    {
        protected List<Emprestimo> registros = new List<Emprestimo>();
        public void InserirRegistro(Emprestimo registro)
        {
            registro.id = registros.Count + 1;
            registros.Add(registro);

            var opcoes = new JsonSerializerOptions();
            opcoes.IncludeFields = true;
            opcoes.ReferenceHandler = ReferenceHandler.Preserve;
            string conteudoArquivo = JsonSerializer.Serialize(registros, opcoes);

            File.WriteAllText(@"C:\temp\emprestimos.json", conteudoArquivo);
        }
        public bool EditarRegistro(int id, Emprestimo registroAtualizado)
        {
            throw new NotImplementedException();
        }

        public bool ExcluirRegistro(int id)
        {
            throw new NotImplementedException();
        }

        public Emprestimo SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Emprestimo> SelecionarTodos()
        {
            var opcoes = new JsonSerializerOptions();
            opcoes.IncludeFields = true;
            opcoes.ReferenceHandler = ReferenceHandler.Preserve;
            var conteudo = File.ReadAllText(@"C:\temp\emprestimos.json");
            var emprestimos = JsonSerializer.Deserialize<List<Emprestimo>>(conteudo, opcoes);
            return emprestimos;
        }

        public bool Validacoes(Func<Emprestimo, bool> validacao)
        {
            throw new NotImplementedException();
        }

        public List<Emprestimo> SelecionarTodosAbertos()
        {
            return SelecionarTodos().Where(e => e.situacao == SituacaoEmprestimo.Aberto).ToList();
        }

        public List<Emprestimo> SelecionarTodosFechados()
        {
            return SelecionarTodos().Where(e => e.situacao == SituacaoEmprestimo.Fechado).ToList();
        }

    }
}
