using Clube_da_Leitura.Compartilhado;

namespace Clube_da_Leitura.ModuloEmprestimo
{
    public interface IRepositorioEmprestimo : IRepositorio<Emprestimo>
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
