namespace Clube_da_Leitura.ModuloEmprestimo
{
    public class RepositorioEmprestimoEmBancoDeDados : IRepositorioEmprestimo
    {
        public bool EditarRegistro(int id, Emprestimo registroAtualizado)
        {
            throw new NotImplementedException();
        }

        public bool ExcluirRegistro(int id)
        {
            throw new NotImplementedException();
        }

        public void InserirRegistro(Emprestimo registro)
        {
            throw new NotImplementedException();
        }

        public Emprestimo SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Emprestimo> SelecionarTodos()
        {
            throw new NotImplementedException();
        }

        public List<Emprestimo> SelecionarTodosAbertos()
        {
            throw new NotImplementedException();
        }

        public List<Emprestimo> SelecionarTodosFechados()
        {
            throw new NotImplementedException();
        }

        public bool Validacoes(Func<Emprestimo, bool> validacao)
        {
            throw new NotImplementedException();
        }
    }
}
