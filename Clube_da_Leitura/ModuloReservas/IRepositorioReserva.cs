using Clube_da_Leitura.Compartilhado;

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
