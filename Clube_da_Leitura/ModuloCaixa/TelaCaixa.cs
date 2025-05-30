using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using System.Runtime.InteropServices;
using static Clube_da_Leitura.ModuloCaixa.Caixa;
namespace Clube_da_Leitura.ModuloCaixa;

public class TelaCaixa : TelaBase<Caixa>
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
        Console.Write($"{c.id,-10} | {c.etiqueta,-20} | ");
        ImprimirComCor(c.cor, c.cor.ToString().PadRight(15)); 
        Console.WriteLine($" | {c.dias,-15}");
    }

    //TODO Vinculando a cor a opção selecionada
    private void ImprimirComCor(CorCaixa cor, string texto)
    {
        switch (cor)
        {
            case CorCaixa.Vermelha:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case CorCaixa.Azul:
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case CorCaixa.Verde:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case CorCaixa.Amarela:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case CorCaixa.Roxa:
                Console.ForegroundColor = ConsoleColor.Magenta;
                break;
            case CorCaixa.Laranja:
                Console.ForegroundColor = ConsoleColor.DarkYellow; 
                break;

            default:
                Console.ResetColor();
                break;
        }

        Console.Write(texto);
        Console.ResetColor();
    }

    public override Caixa ObterDados()
    {
        Console.Write("Digite a nova etiqueta da caixa: ");
        string novaEtiqueta = Console.ReadLine();

        Console.WriteLine("Escolha a cor da caixa: ");
        foreach (CorCaixa cor in Enum.GetValues(typeof(CorCaixa)))
        {
            Console.ForegroundColor = ObterCorConsole(cor);
            Console.WriteLine($"| {(int)cor} - {cor}");
            Console.ResetColor();
        }

        int corSelecionada;
        Console.Write("Digite o numero da Cor: ");
        while (!int.TryParse(Console.ReadLine(), out corSelecionada) || !Enum.IsDefined(typeof(CorCaixa), corSelecionada))
        {
            Console.Write("Cor inválida. Escolha novamente: ");
        }

        CorCaixa novaCor = (CorCaixa)corSelecionada;

        Console.Write("Digite a nova quantidade de dias de emprestimo ou 0 para o padrão: ");
        string dias = Console.ReadLine();
        int novoDiasEmprestimo;

        if (!int.TryParse(dias, out novoDiasEmprestimo) || novoDiasEmprestimo == 0)
            novoDiasEmprestimo = 7;

        var repositorioCaixa = (RepositorioCaixa)repositorio;

        bool duplicado = repositorioCaixa.ValidarDuplicidade(a => a.etiqueta == novaEtiqueta);
        if (duplicado)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nJá existe uma caixa com essa etiqueta!");
            Console.ResetColor();
            return ObterDados(); 
        }



        return new Caixa(novaEtiqueta, novaCor, novoDiasEmprestimo);
    }
    //TODO Mostra as cores na hora de Obter Dados
    private ConsoleColor ObterCorConsole(CorCaixa cor)
    {
        return cor switch
        {
            CorCaixa.Vermelha => ConsoleColor.Red,
            CorCaixa.Azul => ConsoleColor.Blue,
            CorCaixa.Verde => ConsoleColor.Green,
            CorCaixa.Amarela => ConsoleColor.Yellow,
            CorCaixa.Roxa => ConsoleColor.Magenta,
            CorCaixa.Laranja => ConsoleColor.DarkYellow,
            _ => ConsoleColor.White
        };
    }
}
