using Clube_da_Leitura.Compartilhado;

namespace Clube_da_Leitura.ModuloRevista
{
    public interface IRepositorioRevista : IRepositorio<Revista>
    {
        void InserirRegistro(Revista registro);
        bool EditarRegistro(int id, Revista registroAtualizado);
        Revista SelecionarPorId(int id);
        bool ExcluirRegistro(int id);
        List<Revista> SelecionarTodos();
        bool Validacoes(Func<Revista, bool> validacao);
    }
}
