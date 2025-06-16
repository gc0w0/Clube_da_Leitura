using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using System.Text.Json.Serialization;
using System.Text.Json;

public abstract class RepositorioBaseEmArquivo<T> : IRepositorio<T> where T : EntidadeBase<T>
{
   
    public ClubeLeituraContextoDeDados contexto;
    protected RepositorioBaseEmArquivo(ClubeLeituraContextoDeDados contextoDeDados)
    {
        contexto = contextoDeDados;
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
