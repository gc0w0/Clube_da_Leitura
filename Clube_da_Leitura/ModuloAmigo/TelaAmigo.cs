using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloMultas;
using Gestao_de_Equipamentos.Compartilhado;
using System.Globalization;
namespace Clube_da_Leitura.ModuloAmigo;

public class TelaAmigo : TelaBase<Amigo>
{
    private const string formatoColunasTabela = "{0, -10} | {1, -20} | {2, -20} | {3, -15} | {4, -12} | {5, -5} | {6, -10}";
    IRepositorioAmigo repositorioAmigo;

    public TelaAmigo(IRepositorioAmigo repositorioAmigo)
    {
        modulo = "Amigos";
        this.repositorioAmigo = repositorioAmigo;
        //repositorio = (RepositorioBase<Amigo>)repositorioAmigo;
    }

    public override void CadastrarRegistro()
    {
        Console.Clear();

        Console.WriteLine($"Modulo de {modulo}");

        Console.WriteLine($"Cadastrando {modulo}...");

        Console.WriteLine();

        Amigo registro = ObterDados();

        string resultadoValidacao = registro.Validar();

        if (resultadoValidacao != "")
        {
            Console.WriteLine(resultadoValidacao);
            Console.ReadKey();
            CadastrarRegistro();
            return;
        }

        repositorio.InserirRegistro(registro);

        Console.WriteLine("Registro inserido com sucesso \n");
        Console.ReadKey();
    }

    public override void VisualizarRegistros(bool mostrarCabecalho)
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

        var registros = repositorioAmigo.SelecionarTodos();

        for (int i = 0; i < registros.Count; i++)
        {
            var registro = registros[i];

            if (registro == null)
                continue;

            ExibirLinhaTabela(registro);
        }

        Console.ReadLine();
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

        var repositorioAmigo = (RepositorioAmigoEmMemoria)repositorio;

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
  
}
