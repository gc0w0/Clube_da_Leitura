using Clube_da_Leitura.Compartilhado;

namespace Clube_da_Leitura.ModuloEmprestimo
{
    public interface IRepositorioEmprestimo : IRepositorio<Emprestimo>
    {
        public List<Emprestimo> SelecionarTodosAbertos();


        public List<Emprestimo> SelecionarTodosFechados();

    }
}
