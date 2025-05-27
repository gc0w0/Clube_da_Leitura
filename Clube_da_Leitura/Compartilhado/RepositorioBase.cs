using Clube_da_Leitura.Compartilhado;

namespace Gestao_de_Equipamentos.Compartilhado;

public class RepositorioBase<T> where T : EntidadeBase<T>
{
    protected List<T> registros = new List<T>();

    public void InserirRegistro(T registro)
    {
        registro.id = registros.Count + 1;
        registros.Add(registro);
    }

    public bool EditarRegistro(int id, T registroAtualizado)
    {
        T? registro = SelecionarPorId(id);

        if (registro == null)
            return false;

        registro.AtualizarInformacoes(registroAtualizado);

        return true;
    }


    public T SelecionarPorId(int id)
    {
        return registros.FirstOrDefault(e => e.id == id);
    }


    public bool ExcluirRegistro(int id)
    {
        T? registro = SelecionarPorId(id);
        if (registro == null)
            return false;
        registros.Remove(registro);
        return true;
    }

    public List<T> SelecionarTodos()
    {
        return registros;
    }

}

