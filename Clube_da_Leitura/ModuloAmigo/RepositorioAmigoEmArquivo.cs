
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Clube_da_Leitura.ModuloAmigo;
public class RepositorioAmigoEmArquivo : IRepositorioAmigo
{
    protected List<Amigo> registros = new List<Amigo>();

    public void InserirRegistro(Amigo registro)
    {
        registro.id = registros.Count + 1;
        registros.Add(registro);

        var opcoes = new JsonSerializerOptions();
        opcoes.IncludeFields = true;
        opcoes.ReferenceHandler = ReferenceHandler.Preserve;
        string conteudoArquivo = JsonSerializer.Serialize(registros, opcoes);

        File.WriteAllText(@"C:\temp\amigos.json", conteudoArquivo);
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
        var opcoes = new JsonSerializerOptions();
        opcoes.IncludeFields = true;
        opcoes.ReferenceHandler = ReferenceHandler.Preserve;
        var conteudo = File.ReadAllText(@"C:\temp\amigos.json");
        var amigos = JsonSerializer.Deserialize<List<Amigo>>(conteudo, opcoes);
        return amigos;
    }

    public bool Validacoes(Func<Amigo, bool> validacao)
    {
        throw new NotImplementedException();
    }
}
