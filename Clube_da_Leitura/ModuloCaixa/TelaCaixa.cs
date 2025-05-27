using Clube_da_Leitura.Compartilhado;
using System.Runtime.InteropServices;
namespace Clube_da_Leitura.ModuloCaixa;

internal class TelaCaixa : TelaBase<Caixa>
{
    private const string formatoColunasTabela = "{0, -10} | {1, -20} | {2, -15} | {3, -15}";
    private RepositorioCaixa repositorioCaixa;
    private TelaCaixa telaCaixa;
    public TelaCaixa(RepositorioCaixa repositorioCaixa)
    {
        modulo = "Caixas";
        repositorio = repositorioCaixa;
    }
    public override void ExibirCabecalhoTabela()
    {
        Console.WriteLine(formatoColunasTabela, "Id", "Etiqueta", "Cor", "Dias");
    }

    public override void ExibirLinhaTabela(Caixa c)
    {
        Console.WriteLine(formatoColunasTabela, c.id, c.etiqueta, c.cor, c.dias);
    }

    public override Caixa ObterDados()
    {
        Console.Write("Digite a nova etiqueta da caixa: ");
        string novaEtiqueta = Console.ReadLine();

        Console.Write("Digite a nova cor da caixa: ");
        string novaCor = Console.ReadLine();

        Console.Write("Digite a nova quantidade de dias de emprestimo ou 0 para o padrão: ");
        int novoDiasEmprestimo = int.Parse(Console.ReadLine());
        if (novoDiasEmprestimo == 0)
            novoDiasEmprestimo = 7;

        return new Caixa(novaEtiqueta, novaCor, novoDiasEmprestimo);
    }
}
