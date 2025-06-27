namespace Clube_da_Leitura.Compartilhado
{
    public interface IRepositorio<T> where T : EntidadeBase<T>
    {
        void InserirRegistro(T registro);

        bool EditarRegistro(int id, T registroAtualizado);

        T SelecionarPorId(int id);

        bool ExcluirRegistro(int id);

        List<T> SelecionarTodos();

        bool Validacoes(Func<T, bool> validacao);
    }
}
