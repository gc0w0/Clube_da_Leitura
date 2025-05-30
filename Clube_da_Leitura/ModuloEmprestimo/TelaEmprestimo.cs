using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloRevista;
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

    public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa, TelaAmigo telaAmigo, TelaRevista telaRevista, TelaCaixa telaCaixa)
    {
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioRevista = repositorioRevista;
        this.repositorioCaixa = repositorioCaixa;
        this.telaAmigo = telaAmigo;
        this.telaRevista = telaRevista;
        this.telaCaixa = telaCaixa;
        repositorio = repositorioEmprestimo;
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

        this.telaRevista.VisualizarRegistros(mostrarCabecalho: false);
        Console.Write("Digite o ID da revista que deseja pegar emprestada: ");
        int idRevista = Convert.ToInt32(Console.ReadLine());

        Revista revistaSelecionada = repositorioRevista.SelecionarPorId(idRevista);

        DateTime novaDataEmprestimo = DateTime.Now;
        int diasEmprestimo = revistaSelecionada.caixa.dias;
        DateTime novaDataDevolucao = novaDataEmprestimo.AddDays(diasEmprestimo);

        string novoStatus = "Banana";

        return new Emprestimo(amigoSelecionado, revistaSelecionada, novaDataEmprestimo, novaDataDevolucao, novoStatus);
    }
}
