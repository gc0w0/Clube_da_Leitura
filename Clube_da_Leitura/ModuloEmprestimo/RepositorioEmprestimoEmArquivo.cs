using Clube_da_Leitura.Compartilhado;

namespace Clube_da_Leitura.ModuloEmprestimo
{
    public class RepositorioEmprestimoEmArquivo : RepositorioBaseEmArquivo<Emprestimo>, IRepositorioEmprestimo
    {
        public RepositorioEmprestimoEmArquivo(ClubeLeituraContextoDados contextoDados) : base(contextoDados)
        {
        }

        public List<Emprestimo> SelecionarTodosAbertos()
        {
            return SelecionarTodos().Where(e => e.situacao == SituacaoEmprestimo.Aberto).ToList();
        }

        public List<Emprestimo> SelecionarTodosFechados()
        {
            return SelecionarTodos().Where(e => e.situacao == SituacaoEmprestimo.Fechado).ToList();
        }

        protected override List<Emprestimo> ObterRegistros()
        {
            throw new NotImplementedException();
        }
    }
}
