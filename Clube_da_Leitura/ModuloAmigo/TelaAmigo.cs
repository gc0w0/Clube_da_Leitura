using Clube_da_Leitura.Compartilhado;
namespace Clube_da_Leitura.ModuloAmigo;

public class TelaAmigo : TelaBase<Amigo>
{
    private const string formatoColunasTabela = "{0, -10} | {1, -20} | {2, -20} | {3, -15} | {4, -12} | {5, -10}";
    RepositorioAmigo repositorioAmigo;

    public TelaAmigo(RepositorioAmigo repositorioAmigo)
    {
        modulo = "Amigos";
        repositorio = repositorioAmigo;
    }
   
    public override void ExibirCabecalhoTabela()
    {
        Console.WriteLine(formatoColunasTabela, "Id", "Nome", "Nome do Responsavel", "Telefone", "Emprestimos", "Multas");
    }
    
    public override void ExibirLinhaTabela(Amigo a)
    {
        Console.WriteLine(formatoColunasTabela, a.id, a.nome, a.nomeReponsavel, a.telefone, a.emprestimos.Count, a.multa.Count);
    }

    public override Amigo ObterDados()
    {
        Console.Write("Digite o novo nome: ");
        string novoNome = Console.ReadLine();

        Console.Write("Digite o novo nome do responsavel: ");
        string novoNomeResponsavel = Console.ReadLine();

        Console.Write("Digite o novo número de telefone: "); // verificar para aplicar a mascara.
        string novoTelefone = Console.ReadLine();

        var repositorioAmigo = (RepositorioAmigo)repositorio;

        bool duplicado = repositorioAmigo.Validacoes(a => a.nome == novoNome || a.telefone == novoTelefone);

        if (duplicado)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nJá existe um amigo com esse nome e telefone!");
            Console.ResetColor();
            return ObterDados(); // se só chamar o metodo ele salva oq foi inserido antes e acaba inserindo os duplicados.
        }

        //this.telaAmigo.VisualizarRegistros(mostrarCabecalho: false);

        return new Amigo(novoNome, novoNomeResponsavel, novoTelefone);
    }

}
