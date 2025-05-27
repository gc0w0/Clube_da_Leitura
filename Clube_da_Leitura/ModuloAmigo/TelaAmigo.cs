using Clube_da_Leitura.Compartilhado;
namespace Clube_da_Leitura.ModuloAmigo;

public class TelaAmigo : TelaBase<Amigo>
{
    private const string formatoColunasTabela = "{0, -10} | {1, -20} | {2, -15} | {3, -15}";
    private RepositorioAmigo repositorioAmigo;
    private TelaAmigo telaAmigo;
    public TelaAmigo(RepositorioAmigo repositorioAmigo)
    {
        modulo = "Amigos";
        repositorio = repositorioAmigo;
        
    }

    public override void ExibirCabecalhoTabela()
    {
        Console.WriteLine(formatoColunasTabela, "Id", "Nome", "Nome do Responsavel", "Telefone");
    }

    public override void ExibirLinhaTabela(Amigo a)
    {
        Console.WriteLine(formatoColunasTabela, a.id, a.nome, a.nomeReponsavel, a.telefone);
    }

    public override Amigo ObterDados()
    {
        Console.Write("Digite o novo nome: ");
        string novoNome = Console.ReadLine();

        Console.Write("Digite o novo nome do responsavel: ");
        string novoNomeResponsavel = Console.ReadLine();

        Console.Write("Digite o novo número de telefone: "); // verificar para aplicar a mascara.
        string novoTelefone = Console.ReadLine();

        //this.telaAmigo.VisualizarRegistros(mostrarCabecalho: false);

        return new Amigo(novoNome, novoNomeResponsavel, novoTelefone);
    }

}
