
namespace Clube_da_Leitura.Compartilhado;

public abstract class EntidadeBase<T>
{
    public int id;

    public abstract void MostrarInformacoes();

    public abstract void AtualizarInformacoes(T registroAtualizado);

    public abstract string Validar();
}
