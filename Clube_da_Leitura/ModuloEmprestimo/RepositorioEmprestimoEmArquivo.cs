using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.Compartilhado;

namespace Clube_da_Leitura.ModuloEmprestimo
{
    public class RepositorioEmprestimoEmArquivo : RepositorioBaseEmArquivo<Emprestimo>, IRepositorioEmprestimo
    {
        public RepositorioEmprestimoEmArquivo(ClubeLeituraContextoDeDados contextoDeDados) : base(contextoDeDados)
        {
        }
        public List<Emprestimo> SelecionarTodosAbertos()
        {
            return SelecionarTodos().Where(e => e.situacao == SituacaoEmprestimo.Aberto).ToList();
        }

        public List<Emprestimo> SelecionarTodosFechados()
        {
            return SelecionarTodos().Where(e => e.situacao == SituacaoEmprestimo.Fechado).ToList();
        }

        protected override List<Emprestimo> ObterRegistros()
        {
            return contexto.Emprestimos;
        }
    }
}
