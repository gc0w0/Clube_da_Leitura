using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using System.Text.Json.Serialization;
using System.Text.Json;

public class RepositorioBase<T> : IRepositorio<T> where T : EntidadeBase<T>
{
    protected List<T> registros = new List<T>();
    public string caminhoArquivo;

    protected RepositorioBase(string caminhoArquivo)
    {
        this.caminhoArquivo = caminhoArquivo;
        CarregarDoArquivo();
    }
    public void SalvarEmArquivo()
    {
        var opcoes = new JsonSerializerOptions
        {
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };

        string conteudo = JsonSerializer.Serialize(registros, opcoes);
        File.WriteAllText(caminhoArquivo, conteudo);
    }
    public void CarregarDoArquivo()
    {
        if (!File.Exists(caminhoArquivo))
        {
            registros = new List<T>();
            return;
        }

        var opcoes = new JsonSerializerOptions
        {
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.Preserve
        };

        string conteudo = File.ReadAllText(caminhoArquivo);
        registros = JsonSerializer.Deserialize<List<T>>(conteudo, opcoes) ?? new List<T>();
    }

    public void InserirRegistro(T registro)
    {
        CarregarDoArquivo();
        registro.id = registros.Count + 1;
        registros.Add(registro);
        SalvarEmArquivo();
    }

    public bool EditarRegistro(int id, T registroAtualizado)
    {
        CarregarDoArquivo();
        var registro = SelecionarPorId(id);

        if (registro == null)
            return false;

        registro.AtualizarInformacoes(registroAtualizado);
        SalvarEmArquivo();
        return true;
    }

    public T SelecionarPorId(int id)
    {
        CarregarDoArquivo();
        return registros.FirstOrDefault(r => r.id == id);
    }

    public bool ExcluirRegistro(int id)
    {
        CarregarDoArquivo();
        var registro = SelecionarPorId(id);

        if (registro == null)
            return false;

        registros.Remove(registro);
        SalvarEmArquivo();
        return true;
    }

    public List<T> SelecionarTodos()
    {
        CarregarDoArquivo();
        return registros;
    }

    public bool Validacoes(Func<T, bool> validacao)
    {
        CarregarDoArquivo();
        return registros.Any(validacao);
    }
}
