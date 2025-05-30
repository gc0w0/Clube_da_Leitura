using Clube_da_Leitura.Compartilhado;
using Gestao_de_Equipamentos.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Clube_da_Leitura.ModuloEmprestimo.Emprestimo;

namespace Clube_da_Leitura.ModuloEmprestimo
{
    public class RepositorioEmprestimo : RepositorioBase<Emprestimo>
    {
        public List<Emprestimo> SelecionarTodosAbertos()
        {
            return SelecionarTodos()
                .Where(e => e.situacao == SituacoesDisponveis.Aberto)
                .ToList();
        }

        public List<Emprestimo> SelecionarTodosFechados()
        {
            return SelecionarTodos()
                .Where(e => e.situacao == SituacoesDisponveis.Fechado)
                .ToList();
        }
    }
}
