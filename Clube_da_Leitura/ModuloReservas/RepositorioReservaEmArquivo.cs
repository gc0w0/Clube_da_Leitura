using Clube_da_Leitura.Compartilhado;

namespace Clube_da_Leitura.ModuloReservas
{
    public class RepositorioReservaEmArquivo : RepositorioBaseEmArquivo<Reserva>, IRepositorioReserva
    {
        public RepositorioReservaEmArquivo(ClubeLeituraContextoDados contextoDados) : base(contextoDados)
        {
        }

        public List<Reserva> SelecionarTodosAbertos()
        {
            return SelecionarTodos()
               .Where(r => r.situacao == SituacaoReserva.Ativa)
               .ToList();
        }

        protected override List<Reserva> ObterRegistros()
        {
            throw new NotImplementedException();
        }
    }
}
