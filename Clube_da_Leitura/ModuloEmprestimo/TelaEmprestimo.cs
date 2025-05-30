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
    private TelaAmigo telaAmigo;
    private TelaRevista telaRevista;
    private TelaCaixa telaCaixa;
    private RepositorioEmprestimo repositorioEmprestimo;
    protected List<Emprestimo> registros; 

    public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa, TelaAmigo telaAmigo, TelaRevista telaRevista, TelaCaixa telaCaixa)
    {
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
        this.repositorioCaixa = repositorioCaixa;
        this.telaAmigo = telaAmigo;
        this.telaRevista = telaRevista;
        this.telaCaixa = telaCaixa;
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.registros = registros;
        modulo = "Emprestimos";
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

        DateTime novaDataEmprestimo = DateTime.Now;
        int diasEmprestimo = revistaSelecionada.caixa.dias;
        DateTime novaDataDevolucao = novaDataEmprestimo.AddDays(diasEmprestimo);

        SituacoesDisponveis novaSituacao;
        novaSituacao = SituacoesDisponveis.Fechado;
        revistaSelecionada.status = Revista.StatusDisponveis.Reservada;

        return new Emprestimo(amigoSelecionado, revistaSelecionada, novaDataEmprestimo, novaDataDevolucao, novaSituacao);
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

        repositorioEmprestimo.SelecionarTodosAbertos();

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
        var repositorioEmprestimo = (RepositorioEmprestimo)repositorio;
        repositorioEmprestimo.SelecionarTodosAbertos();

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


