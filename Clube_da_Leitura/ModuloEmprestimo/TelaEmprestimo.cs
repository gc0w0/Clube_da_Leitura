using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloRevista;
using static Clube_da_Leitura.ModuloEmprestimo.Emprestimo;
namespace Clube_da_Leitura.ModuloEmprestimo;

public class TelaEmprestimo : TelaBase<Emprestimo>
{
    private const string formatoColunasTabela = "{0, -10} | {1, -20} | {2, -25} | {3, -15}";

    private RepositorioAmigo repositorioAmigo;
    private RepositorioRevista repositorioRevista;
    private RepositorioCaixa repositorioCaixa;

    private RepositorioEmprestimo repositorioEmprestimo;

    private TelaAmigo telaAmigo;
    private TelaRevista telaRevista;
    private TelaCaixa telaCaixa;

    public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioAmigo repositorioAmigo,
        RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa, TelaAmigo telaAmigo,
        TelaRevista telaRevista, TelaCaixa telaCaixa)
    {
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
        this.repositorioCaixa = repositorioCaixa;
        this.telaAmigo = telaAmigo;
        this.telaRevista = telaRevista;
        this.telaCaixa = telaCaixa;

        this.repositorio = repositorioEmprestimo;
        this.repositorioEmprestimo = repositorioEmprestimo;

        modulo = "Emprestimos";
    }

    public override string ExibirOpcoesMenu()
    {
        Console.WriteLine($"Bem-vindo ao Clube da Leitura!\n");
        Console.WriteLine($"Digite 1 para registrar Empréstimo");
        Console.WriteLine($"Digite 2 para registrar Devolução");
        Console.WriteLine($"Digite 3 para exibir empréstimos abertos:");
        Console.WriteLine($"Digite 4 para exibir empréstimos fechados:");

        Console.WriteLine("Digite S para sair");
        Console.Write(">: ");

        opcaoEscolhida = Console.ReadLine();
        return opcaoEscolhida;
    }

    public override void ExibirCabecalhoTabela()
    {
        Console.WriteLine(formatoColunasTabela, "Id", "Data Emprestimo", "Data Devolução", "Status");
    }

    public override void ExibirLinhaTabela(Emprestimo e)
    {
        Console.WriteLine(formatoColunasTabela, e.id, e.dataEmprestimo, e.dataDevolucao, e.situacao);
    }

    public override Emprestimo ObterDados()
    {
        this.telaAmigo.VisualizarRegistros(mostrarCabecalho: false);
        Console.Write("Digite o seu ID de amigo: ");
        int idAmigo = Convert.ToInt32(Console.ReadLine());

        Amigo amigoSelecionado = repositorioAmigo.SelecionarPorId(idAmigo);

        bool jaTemEmprestimoAtivo = amigoSelecionado.emprestimos.Any(e => e.id >= 1);

        if (jaTemEmprestimoAtivo)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Este amigo já possui um empréstimo em aberto e não pode fazer outro!");
            Console.ResetColor();
            Console.ReadKey();
            return ObterDados();
        }

        this.telaRevista.VisualizarRegistros(mostrarCabecalho: false);
        Console.Write("Digite o ID da revista que deseja pegar emprestada: ");
        int idRevista = Convert.ToInt32(Console.ReadLine());

        Revista revistaSelecionada = repositorioRevista.SelecionarPorId(idRevista);

        return new Emprestimo(amigoSelecionado, revistaSelecionada);
    }

    public void VisualizarEmprestimosAbertos(bool mostrarCabecalho)
    {
        if (mostrarCabecalho)
        {
            Console.Clear();

            Console.WriteLine($"Módulo de {modulo}"); //título

            Console.WriteLine($"Visualizando registros de {modulo}..."); //subtítulo
        }

        Console.WriteLine();

        ExibirCabecalhoTabela();

        Console.WriteLine("-------------------------------------------------------------------------------------------------");

        List<Emprestimo> registros = repositorioEmprestimo.SelecionarTodosAbertos();

        foreach (var registro in registros)
        {
            if (registro == null)
                continue;

            ExibirLinhaTabela(registro);
        }

        Console.ReadLine();
    }

    public void VisualizarEmprestimosFechados(bool mostrarCabecalho)
    {
        if (mostrarCabecalho)
        {
            Console.Clear();

            Console.WriteLine($"Módulo de {modulo}"); //título

            Console.WriteLine($"Visualizando registros de {modulo}..."); //subtítulo
        }

        Console.WriteLine();

        ExibirCabecalhoTabela();

        Console.WriteLine("-------------------------------------------------------------------------------------------------");

        List<Emprestimo> registros = repositorioEmprestimo.SelecionarTodosFechados();

        foreach (var registro in registros)
        {
            if (registro == null)
                continue;

            ExibirLinhaTabela(registro);
        }

        Console.ReadLine();
    }

    public string RegistrarDevolucao()
    {
        Console.Clear();
        VisualizarEmprestimosAbertos(false);

        List<Emprestimo> registros = repositorioEmprestimo.SelecionarTodosAbertos();

        foreach (var registro in registros)
        {
            if (registro == null)
                continue;

            ExibirLinhaTabela(registro);
        }

        Console.ReadLine();
        Console.WriteLine("Insira o ID da revista para registrar a devolução: ");
        int idRevista = Convert.ToInt32(Console.ReadLine());
        Revista revistaSelecionada = repositorioRevista.SelecionarPorId(idRevista);
        revistaSelecionada.status = Revista.StatusDisponveis.Disponivel;

        return "";
    }
}


