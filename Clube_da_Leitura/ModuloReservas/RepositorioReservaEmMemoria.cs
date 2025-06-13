using Gestao_de_Equipamentos.Compartilhado;
namespace Clube_da_Leitura.ModuloReservas
{
    public class RepositorioReservaEmMemoria : RepositorioBaseEmMemoria<Reserva>, IRepositorioReserva
    {
        public List<Reserva> SelecionarTodosAbertos()
        {
            return SelecionarTodos()
                .Where(r => r.situacao == SituacaoReserva.Ativa)
                .ToList();
        }
    }
}
