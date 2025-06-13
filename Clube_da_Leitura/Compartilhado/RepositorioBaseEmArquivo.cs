using Clube_da_Leitura.Compartilhado;
public abstract class RepositorioBaseEmArquivo<T> : IRepositorio<T> where T : EntidadeBase<T>
{
    public ClubeLeituraContextoDados contexto;

    protected RepositorioBaseEmArquivo(ClubeLeituraContextoDados contextoDados)
    {
        contexto = contextoDados;
    }

    public void InserirRegistro(T registro)
    {
        List<T> registros = ObterRegistros();

        registro.id = registros.Count + 1;
        registros.Add(registro);

        contexto.SalvarEmArquivo();
    }

    public bool EditarRegistro(int id, T registroAtualizado)
    {
        var registro = SelecionarPorId(id);

        if (registro == null)
            return false;

        registro.AtualizarInformacoes(registroAtualizado);
        contexto.SalvarEmArquivo();
        return true;
    }

    public T SelecionarPorId(int id)
    {
        List<T> registros = ObterRegistros();

        return registros.FirstOrDefault(r => r.id == id);
    }

    public bool ExcluirRegistro(int id)
    {
        List<T> registros = ObterRegistros();

        var registro = SelecionarPorId(id);

        if (registro == null)
            return false;

        registros.Remove(registro);
        contexto.SalvarEmArquivo();
        return true;
    }

    public List<T> SelecionarTodos()
    {
        return ObterRegistros();
    }

    public bool Validacoes(Func<T, bool> validacao)
    {
        List<T> registros = ObterRegistros();

        return registros.Any(validacao);
    }

    protected abstract List<T> ObterRegistros();
}
