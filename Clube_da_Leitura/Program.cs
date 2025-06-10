using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloMultas;
using Clube_da_Leitura.ModuloReservas;
using Clube_da_Leitura.ModuloRevista;
using System.Buffers;
using static Clube_da_Leitura.ModuloCaixa.Caixa;

namespace Clube_da_Leitura
{
    public interface IVoar
    {
        void Voar();
    }

    public interface INadar
    {
        void Nadar();
    }

    public abstract class Animal
    {
        string especie;
    }

    public class GralhaAzul : Animal, IVoar
    {
        public void Voar()
        {
            Console.WriteLine("A gralha está voando...");
        }
    }

    public class Galinha : Animal, IVoar
    {
        public void Voar()
        {
            Console.WriteLine("A galinha está voando...");
        }
    }

    public class Pato : Animal, IVoar, INadar
    {
        public void Nadar()
        {
            throw new NotImplementedException();
        }

        public void Voar()
        {
            Console.WriteLine("O pato está voando bem male má...");
        }
    }

    public class Cachorro : Animal, INadar
    {
        public void Nadar()
        {
            throw new NotImplementedException();
        }
    }

    public delegate int OperacaoMatematica(int a, int b);

    internal class Program
    {

        static int Somar(int x, int y)
        {
            return x + y;
        }

        static int Subtrair(int x, int y)
        {
            return x - y;
        }

        static string Multiplicar(int x, int y)
        {
            return Convert.ToString( x * y );
        }

        static void ApresentarResultadoOperacao(OperacaoMatematica op)
        {
            int a = 10;
            int b = 10;

            Console.WriteLine("Resultado da operação: " + op(a, b));

            Func<int, int, int> teste1 = Somar;

            Func<int, int, string> teste3 = Multiplicar;

            Predicate<Emprestimo> teste2 = FiltroPorStatusAberto;

            //var emprestimoSelecionado = repositorioEmprestimo.SelecionarTodos().Where(x => x.situacao == SituacaoEmprestimo.Aberto);
        }

        static bool FiltroPorStatusAberto(Emprestimo emprestimo)
        {
            return emprestimo.situacao == SituacaoEmprestimo.Aberto;
        }

        static void Main2(string[] args)
        {
            var pato = new Pato();

            ApresentarNado(new Cachorro());
            ApresentarNado(pato);

            ApresentarVoo(pato);
        }

        private static void ApresentarVoo(IVoar animal)
        {
            animal.Voar();
        }

        static void ApresentarNado(INadar animal)
        {
            animal.Nadar();
        }

        static void Main(string[] args)
        {
            Predicate<Amigo> condicao;

            //programação funcional

            //ApresentarResultadoOperacao(Subtrair);

            //ApresentarResultadoOperacao(Somar);

            var caixa = new Caixa("Etiqueta Teste", CorCaixa.Vermelha, 2);
            var revista = new Revista("Pequeno Principe", 2, 2025, caixa, Revista.StatusDisponveis.Emprestada);
            var amigo = new Amigo("Markswell", "Gabriel", "49984327736");

            var repositorioEmprestimo = new RepositorioEmprestimo();
            var emprestimo1 = new Emprestimo(amigo, revista);
            emprestimo1.situacao = SituacaoEmprestimo.Fechado;

            var emprestimo2 = new Emprestimo(amigo, revista);
            emprestimo2.situacao = SituacaoEmprestimo.Fechado;

            var emprestimo3 = new Emprestimo(amigo, revista);

            repositorioEmprestimo.InserirRegistro(emprestimo1);
            repositorioEmprestimo.InserirRegistro(emprestimo2);
            repositorioEmprestimo.InserirRegistro(emprestimo3);
         

            var repositorioAmigo = new RepositorioAmigoEmBancoDados();
            var telaAmigo = new TelaAmigo(repositorioAmigo);

            var amigo2 = new Amigo("Gregory", "Gabriel", "11111111111");

            repositorioAmigo.InserirRegistro(amigo);
            repositorioAmigo.InserirRegistro(amigo2);

            var repositorioCaixa = new RepositorioCaixa();
            var telaCaixa = new TelaCaixa(repositorioCaixa);
            var caixa2 = new Caixa("Caixa 2", CorCaixa.Amarela, 1);
            repositorioCaixa.InserirRegistro(caixa);
            repositorioCaixa.InserirRegistro(caixa2);

            var repositorioRevista = new RepositorioRevista();
            var telaRevista = new TelaRevista(repositorioCaixa, telaCaixa, repositorioRevista);
            var revista2 = new Revista("Teste2", 3, 1999, caixa2, Revista.StatusDisponveis.Disponivel);
            repositorioRevista.InserirRegistro(revista);
            repositorioRevista.InserirRegistro(revista2);

            var emprestimo4 = new Emprestimo(amigo2, revista2);
            repositorioEmprestimo.InserirRegistro(emprestimo4);
            repositorioEmprestimo.InserirRegistro(emprestimo2);
            //repositorioEmprestimo.InserirRegistro(emprestimo2);

            var repositorioMulta = new RepositorioMulta();

            var repositorioReserva = new RepositorioReserva();

            var telaEmprestimo = new TelaEmprestimo(
                repositorioEmprestimo, repositorioAmigo, repositorioRevista,
                repositorioCaixa, telaAmigo, telaRevista, telaCaixa, repositorioMulta);

            var telaMulta = new TelaMulta(repositorioMulta, repositorioAmigo, repositorioEmprestimo, telaAmigo, telaEmprestimo, emprestimo4);
            var telaReserva = new TelaReserva(repositorioEmprestimo, repositorioReserva, repositorioAmigo, repositorioRevista,
                repositorioMulta, telaAmigo, telaRevista, telaMulta);


            var telaPrincipal = new TelaPrincipal();

            while (true)
            {
                telaPrincipal.ExibirOpcoesMenu();

                if (telaPrincipal.opcaoEscolhida == "1")
                    GerenciarRegistros(telaAmigo, telaPrincipal);

                else if (telaPrincipal.opcaoEscolhida == "2")
                    GerenciarRegistros(telaCaixa, telaPrincipal);

                else if (telaPrincipal.opcaoEscolhida == "3")
                    GerenciarRegistros(telaRevista, telaPrincipal);

                else if (telaPrincipal.opcaoEscolhida == "4")
                    GerenciarEmprestimos(telaEmprestimo, telaPrincipal);

                else if (telaPrincipal.opcaoEscolhida == "5")
                    GerenciarReservar(telaReserva, telaPrincipal);

                else if (telaPrincipal.opcaoEscolhida == "S")
                {
                    Console.WriteLine("Saindo do sistema...");
                    break;
                }
            }
        }

        private static void GerenciarReservar(TelaReserva telaReserva, TelaPrincipal telaPrincipal)
        {
            telaReserva.ExibirOpcoesMenu();

            if (telaReserva.opcaoEscolhida == "1")
                telaReserva.CadastrarRegistro();
            else if (telaReserva.opcaoEscolhida == "2")
                telaReserva.CancelarReservas();//TODO Implementar Registrr devolução
            else if (telaReserva.opcaoEscolhida == "3")
                telaReserva.VisualizarReservasAbertas(mostrarCabecalho: true);
            else if (telaReserva.opcaoEscolhida == "4")
                telaReserva.RetirarRevista();//TODO Implementar Registrr devolução
        }


        private static void GerenciarEmprestimos(TelaEmprestimo telaEmprestimo, TelaPrincipal telaPrincipal)
        {
            telaEmprestimo.ExibirOpcoesMenu();

            if (telaEmprestimo.opcaoEscolhida == "1")
                telaEmprestimo.CadastrarRegistro();
            else if (telaEmprestimo.opcaoEscolhida == "2")
                telaEmprestimo.RegistrarDevolucao();//TODO Implementar Registrr devolução
            else if (telaEmprestimo.opcaoEscolhida == "3")
                telaEmprestimo.VisualizarEmprestimosAbertos(mostrarCabecalho: true);
            else if (telaEmprestimo.opcaoEscolhida == "4")
                telaEmprestimo.VisualizarEmprestimosFechados(mostrarCabecalho: true);
            else if (telaEmprestimo.opcaoEscolhida == "5")
                telaEmprestimo.VisualizarRegistros(mostrarCabecalho: true);

        }


        private static void GerenciarRegistros<T>(TelaBase<T> telaCrud, TelaPrincipal telaPrincipal) where T : EntidadeBase<T>
        {
            telaCrud.ExibirOpcoesMenu();

            if (telaCrud.opcaoEscolhida == "1")
                telaCrud.CadastrarRegistro();

            else if (telaCrud.opcaoEscolhida == "2")
                telaCrud.VisualizarRegistros(mostrarCabecalho: true);

            else if (telaCrud.opcaoEscolhida == "3")
                telaCrud.EditarRegistro();

            else if (telaCrud.opcaoEscolhida == "4")
                telaCrud.ExcluirRegistro();

            else if (telaCrud.opcaoEscolhida == "5")
            {
                TelaAmigo telaAmigo = telaCrud as TelaAmigo;
                telaAmigo.QuitarMulta();
            }
        }            

    }
}
