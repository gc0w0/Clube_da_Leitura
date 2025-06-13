
namespace Clube_da_Leitura.ModuloAmigo;

public class RepositorioEmBancoDeDados : IRepositorioAmigo
{
    public void InserirRegistro(Amigo registro)
    {
        throw new NotImplementedException();
    }
    public bool EditarRegistro(int id, Amigo registroAtualizado)
    {
        throw new NotImplementedException();
    }

    public bool ExcluirRegistro(int id)
    {
        throw new NotImplementedException();
    }

    public List<Amigo> SelecionarPorFiltro(string letra)
    {
        throw new NotImplementedException();
    }

    public List<Amigo> SelecionarPorFiltro2(Predicate<Amigo> condicao)
    {
        throw new NotImplementedException();
    }

    public Amigo SelecionarPorId(int id)
    {
        throw new NotImplementedException();
    }

    public List<Amigo> SelecionarTodos()
    {
        throw new NotImplementedException();
    }

    public bool Validacoes(Func<Amigo, bool> validacao)
    {
        throw new NotImplementedException();
    }
}
