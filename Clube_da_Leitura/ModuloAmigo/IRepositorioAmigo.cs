namespace Clube_da_Leitura.ModuloAmigo
{
    public interface IRepositorioAmigo
    {
        void InserirRegistro(Amigo registro);
        bool EditarRegistro(int id, Amigo registroAtualizado);
        Amigo SelecionarPorId(int id);
        bool ExcluirRegistro(int id);
        List<Amigo> SelecionarTodos();
        bool Validacoes(Func<Amigo, bool> validacao);
        List<Amigo> SelecionarPorFiltro(string letra);
        List<Amigo> SelecionarPorFiltro2(Predicate<Amigo> condicao);
    }
}
