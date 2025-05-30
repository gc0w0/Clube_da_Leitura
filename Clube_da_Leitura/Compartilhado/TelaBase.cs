using Gestao_de_Equipamentos.Compartilhado;
namespace Clube_da_Leitura.Compartilhado;

public abstract class TelaBase<T> where T : EntidadeBase<T>
{

    public string opcaoEscolhida;

    public string titulo;

    public string modulo;

    public RepositorioBase<T> repositorio;

    public string ExibirOpcoesMenu()
    {
        Console.Clear();

        Console.WriteLine($"Bem-vindo ao Clube da Leitura!\n");
        Console.WriteLine($"Digite 1 para cadastrar {modulo}:");
        Console.WriteLine($"Digite 2 para exibir {modulo}:");
        Console.WriteLine($"Digite 3 para editar {modulo}:");
        Console.WriteLine($"Digite 4 para excluir {modulo}:");
        Console.WriteLine("Digite S para sair");
        Console.Write(">: ");

        opcaoEscolhida = Console.ReadLine();

        return opcaoEscolhida;
    }

    public void CadastrarRegistro()
    {
        Console.Clear();

        Console.WriteLine($"Modulo de {modulo}");

        Console.WriteLine($"Cadastrando {modulo}...");

        Console.WriteLine();

        T registro = ObterDados();

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

    public void EditarRegistro()
    {
        Console.Clear();

        Console.WriteLine($"Módulo de {modulo}"); //título

        Console.WriteLine($"Editando {modulo}..."); //subtítulo

        Console.WriteLine();

        VisualizarRegistros(mostrarCabecalho: false);

        Console.Write($"Digite o {modulo} que deseja editar: ");
        var id = int.Parse(Console.ReadLine());

        T registro = ObterDados();

        bool conseguiuEditar = repositorio.EditarRegistro(id, registro);

        if (conseguiuEditar == false)
        {
            Console.WriteLine("Não foi possível editar o registro selecionado");
            Console.ReadKey();
            EditarRegistro();
            return;
        }

        Console.WriteLine($"{modulo} editado com sucesso!");
        Console.ReadKey();
    }

    public void VisualizarRegistros(bool mostrarCabecalho)
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

        List<T> registros = repositorio.SelecionarTodos();

        for (int i = 0; i < registros.Count; i++)
        {
            T registro = registros[i];

            if (registro == null)
                continue;

            ExibirLinhaTabela(registro);
        }

        Console.ReadLine();
    }

    public void ExcluirRegistro(Func<T, bool> condicaoDeVinculo = null)
    {
        Console.Clear();

        Console.WriteLine($"Módulo de {modulo}");

        Console.WriteLine($"Editando {modulo}...");

        Console.WriteLine();

        VisualizarRegistros(mostrarCabecalho: false);

        Console.Write($"Digite o {modulo} que deseja excluir: ");
        var id = int.Parse(Console.ReadLine());

        T registro = repositorio.SelecionarPorId(id);

        //TODO Verifica vinculo, se foi passada uma condição
        if (condicaoDeVinculo != null && RegistroPossuiVinculo(registro, condicaoDeVinculo))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nEste registro possui vínculos e não pode ser excluído!");
            Console.ResetColor();
            Console.ReadKey();
            return;
        }

        bool conseguiuExcluir = repositorio.ExcluirRegistro(id);

        if (conseguiuExcluir == false)
        {
            Console.WriteLine("Não foi possível excluir o registro selecionado");
            Console.ReadKey();
            ExcluirRegistro();
            return;
        }

        Console.WriteLine("Chamado removido com sucesso!");
        Console.ReadKey();
    }

    public abstract T ObterDados();

    public abstract void ExibirCabecalhoTabela();

    public abstract void ExibirLinhaTabela(T registro);

    protected bool RegistroPossuiVinculo(T registro, Func<T, bool> condicaoDeVinculo)
    {
        return condicaoDeVinculo(registro);
    }
}

