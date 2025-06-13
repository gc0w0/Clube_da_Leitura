using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloReservas
{
    public interface IRepositorioReserva : IRepositorio<Reserva>
    {
        public List<Reserva> SelecionarTodosAbertos()
        {
            return SelecionarTodos()
                .Where(r => r.situacao == SituacaoReserva.Ativa)
                .ToList();
        }
    }
}
