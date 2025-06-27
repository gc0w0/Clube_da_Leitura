using Clube_da_Leitura.Infra.Arquivos.Compartilhado;

namespace Clube_da_Leitura.ModuloReservas;

public class RepositorioReservaEmArquivo : RepositorioBaseEmArquivo<Reserva>, IRepositorioReserva
{
    public RepositorioReservaEmArquivo(ClubeLeituraContextoDeDados contextoDeDados) : base(contextoDeDados)
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
        return contexto.Reservas;
    }
}
