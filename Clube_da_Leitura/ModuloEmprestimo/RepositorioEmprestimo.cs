using Gestao_de_Equipamentos.Compartilhado;

namespace Clube_da_Leitura.ModuloEmprestimo
{
    public class RepositorioEmprestimo : RepositorioBase<Emprestimo>
    {
        public List<Emprestimo> SelecionarTodosAbertos()
        {
            return SelecionarTodos().Where(e => e.situacao == SituacaoEmprestimo.Aberto).ToList();
        }

        public List<Emprestimo> SelecionarTodosFechados()
        {
            return SelecionarTodos().Where(e => e.situacao == SituacaoEmprestimo.Fechado).ToList();
        }

    }
}
