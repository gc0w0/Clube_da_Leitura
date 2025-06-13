
namespace Clube_da_Leitura.ModuloCaixa;

public interface IRepositorioCaixa
{
    void InserirRegistro(Caixa registro);
    bool EditarRegistro(int id, Caixa registroAtualizado);
    Caixa SelecionarPorId(int id);
    bool ExcluirRegistro(int id);
    List<Caixa> SelecionarTodos();
    bool Validacoes(Func<Caixa, bool> validacao);
}
