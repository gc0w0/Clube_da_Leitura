using Gestao_de_Equipamentos.Compartilhado;

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
