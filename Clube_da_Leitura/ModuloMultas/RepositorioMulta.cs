using Clube_da_Leitura.ModuloEmprestimo;
using Gestao_de_Equipamentos.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloMultas
{
    public class RepositorioMulta : RepositorioBaseEmMemoria<Multa>
    {
        public List<Multa> SelecionarTodosAbertos()
        {
            return SelecionarTodos()
                .Where(m => m.situacao == SituacaoMulta.Pendente)
                .ToList();
        }

        public List<Multa> SelecionarTodosFechados()
        {
            return SelecionarTodos()
                .Where(m => m.situacao == SituacaoMulta.Quitada)
                .ToList();
        }
    }
}
