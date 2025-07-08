using Clube_da_Leitura.Compartilhado;

namespace Clube_da_Leitura.ModuloReserva
{
    public interface IRepositorioReserva : IRepositorio<Reserva>
    {
        public List<Reserva> SelecionarTodosAbertos()
        {
            return SelecionarTodos()
                .Where(r => r.Situacao == SituacaoReserva.Ativa)
                .ToList();
        }
    }
}
