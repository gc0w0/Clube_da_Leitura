
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Clube_da_Leitura.ModuloAmigo;
public class RepositorioAmigoEmArquivo : IRepositorioAmigo
{
    protected List<Amigo> registros = new List<Amigo>();
    private const string caminhoArquivo = @"C:\temp\amigos.json";
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
            registros = new List<Amigo>();
            return;
        }

        var opcoes = new JsonSerializerOptions
        {
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.Preserve
        };

        string conteudo = File.ReadAllText(caminhoArquivo);
        registros = JsonSerializer.Deserialize<List<Amigo>>(conteudo, opcoes) ?? new List<Amigo>();
    }

    public void InserirRegistro(Amigo registro)
    {
        CarregarDoArquivo();
        registro.id = registros.Count + 1;
        registros.Add(registro);
        SalvarEmArquivo();
    }
    public bool EditarRegistro(int id, Amigo registroAtualizado)
    
    {
        CarregarDoArquivo();
        var registro = registros.FirstOrDefault(r => r.id == id);

        if (registro == null)
            return false;

        registro.AtualizarInformacoes(registroAtualizado);
        SalvarEmArquivo();
        return true;
    }

    public bool ExcluirRegistro(int id)
    {
        CarregarDoArquivo();
        var registro = registros.FirstOrDefault(r => r.id == id);

        if (registro == null)
            return false;

        registros.Remove(registro);
        SalvarEmArquivo();
        return true;
    }

    public List<Amigo> SelecionarPorFiltro(string letra)
    {
        CarregarDoArquivo();
        return SelecionarTodos().Where(a => a.nome.StartsWith(letra, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Amigo> SelecionarPorFiltro2(Predicate<Amigo> condicao)
    {
        CarregarDoArquivo();
        return SelecionarTodos().FindAll(condicao).ToList();
    }

    public Amigo SelecionarPorId(int id)
    {
        CarregarDoArquivo();
        return registros.FirstOrDefault(e => e.id == id);
    }

    public List<Amigo> SelecionarTodos()
    {
        CarregarDoArquivo();
        return registros;
    }

    public bool Validacoes(Func<Amigo, bool> validacao)
    {
        CarregarDoArquivo();
        return registros.Any(validacao);
    }
}
