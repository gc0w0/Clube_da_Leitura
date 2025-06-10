using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloMultas;
using System.Collections.Concurrent;
using System.Globalization;
namespace Clube_da_Leitura.ModuloAmigo;

public class TelaAmigo : TelaBase<Amigo>
{
    private const string formatoColunasTabela = "{0, -10} | {1, -20} | {2, -20} | {3, -15} | {4, -12} | {5, -5} | {6, -10}";
    RepositorioAmigo repositorioAmigo;


    public TelaAmigo(RepositorioAmigo repositorioAmigo)
    {
        modulo = "Amigos";
        repositorio = repositorioAmigo;
        this.repositorioAmigo = repositorioAmigo;
    }

    public override string ExibirOpcoesMenu()
    {
        Console.Clear();

        Console.WriteLine($"Bem-vindo ao Clube da Leitura!\n");
        Console.WriteLine($"Digite 1 para cadastrar {modulo}:");
        Console.WriteLine($"Digite 2 para exibir {modulo}:");
        Console.WriteLine($"Digite 3 para editar {modulo}:");
        Console.WriteLine($"Digite 4 para excluir {modulo}:");
        Console.WriteLine($"Digite 5 para quitar Multas {modulo}");
        Console.WriteLine($"Digite 6 para fitlrar {modulo}");
        Console.WriteLine("Digite S para sair");
        Console.Write(">: ");

        opcaoEscolhida = Console.ReadLine();

        return opcaoEscolhida;
    }
    public override void ExibirCabecalhoTabela()
    {
        Console.WriteLine(formatoColunasTabela, "Id", "Nome", "Nome do Responsavel", "Telefone", "Emprestimos", "Multas", "Valor das Multas");
    }

    public override void ExibirLinhaTabela(Amigo a)
    {
        float valorMultasPendentes = a.multas
        .Where(m => m.situacao == SituacaoMulta.Pendente)
        .Sum(m => m.valorMulta);

        string valorFormatado = valorMultasPendentes.ToString("C", new CultureInfo("pt-BR"));

        Console.WriteLine(formatoColunasTabela,
            a.id, a.nome, a.nomeReponsavel, a.telefone,
            a.emprestimos.Count, a.multas.Count, valorFormatado);
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

    public void QuitarMulta()
    {
        Console.Clear();
        Console.WriteLine("== Quitar Multa de Amigo ==");

        VisualizarRegistros(true);

        Console.Write("\nDigite o ID do amigo que deseja quitar multa: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Amigo amigo = repositorio.SelecionarPorId(id);

        if (amigo == null)
        {
            Console.WriteLine("Amigo não encontrado.");
            Console.ReadKey();
            return;
        }

        var multasPendentes = amigo.multas.Where(m => m.situacao == SituacaoMulta.Pendente).ToList();

        if (multasPendentes.Count == 0)
        {
            Console.WriteLine("Esse amigo não possui multas pendentes.");
            Console.ReadKey();
            return;
        }

        float total = multasPendentes.Sum(m => m.valorMulta);
        Console.WriteLine($"Multas pendentes encontradas. Valor total: R$ {total:0.00}");

        Console.Write("Deseja quitar todas as multas pendentes? (S/N): ");
        string resposta = Console.ReadLine().ToUpper();

        if (resposta == "S")
        {
            foreach (var multa in multasPendentes)
                multa.situacao = SituacaoMulta.Quitada;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Multas quitadas com sucesso!");
            Console.ResetColor();
        }

        Console.ReadKey();
    }

    public void FiltroAmigos()
    {
        Console.Clear();
        Console.WriteLine("Filtre amigos por letra:");
        Console.Write("Selecione a letra que deseja buscar: ");
        string letraSelecionada = Console.ReadLine();
        List<Amigo> registros = repositorioAmigo.SelecionarPorFiltro2(FiltrarIniciandoComLetraA);
        List<Amigo> registros2 = repositorioAmigo.SelecionarPorFiltro2(FiltrarIniciandoComLetraB);


        if (registros.Count == 0)
        {
            Console.WriteLine("Nenhum amigo encontrado com essa letra.");
        }
        else
        {
            foreach (Amigo amigo in registros)
            {
                ExibirLinhaTabela(amigo);

            }
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    public bool FiltrarIniciandoComLetraA(Amigo amigo)
    { 
        return amigo.nome.Contains("A"); ;
    }

    public bool FiltrarIniciandoComLetraB(Amigo amigo)
    {
        return amigo.nome.StartsWith("B");
    }
}
