using Gestao_de_Equipamentos.Compartilhado;

namespace Clube_da_Leitura.ModuloEmprestimo
{
    public class RepositorioEmprestimoEmMemoria : RepositorioBase<Emprestimo>, IRepositorioEmprestimo
    {
        public List<Emprestimo> SelecionarPorFiltro(string letra)
        {
            throw new NotImplementedException();
        }

        public List<Emprestimo> SelecionarPorFiltro2(Predicate<Emprestimo> condicao)
        {
            throw new NotImplementedException();
        }

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
