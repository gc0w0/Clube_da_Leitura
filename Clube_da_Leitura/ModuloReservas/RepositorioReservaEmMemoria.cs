using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clube_da_Leitura.ModuloEmprestimo;
using Gestao_de_Equipamentos.Compartilhado;
namespace Clube_da_Leitura.ModuloReservas
{
    public class RepositorioReservaEmMemoria : RepositorioBase<Reserva>, IRepositorioReserva
    {
        public List<Reserva> SelecionarTodosAbertos()
        {
            return SelecionarTodos()
                .Where(r => r.situacao == SituacaoReserva.Ativa)
                .ToList();
        }
    }
}
