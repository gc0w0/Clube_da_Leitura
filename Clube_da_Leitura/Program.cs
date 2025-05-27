using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloRevista;

namespace Clube_da_Leitura
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var repositorioAmigo = new RepositorioAmigo();
            var telaAmigo = new TelaAmigo(repositorioAmigo);

            var repositorioCaixa = new RepositorioCaixa();
            var telaCaixa = new TelaCaixa(repositorioCaixa);

            var repositorioRevista = new RepositorioRevista();
            var telaRevista = new TelaRevista(repositorioCaixa, telaCaixa, repositorioRevista);

            var telaPrincipal = new TelaPrincipal();

            while (true)
            {
                telaPrincipal.ExibirOpcoesMenu();

                if (telaPrincipal.opcaoEscolhida == "1")
                    GerenciarAmigos(telaAmigo, telaPrincipal);

                else if (telaPrincipal.opcaoEscolhida == "2")
                    GerenciarCaixas(telaCaixa, telaPrincipal);

                else if (telaPrincipal.opcaoEscolhida == "3")
                    GerenciarRevistas(telaRevista, telaPrincipal);

                else if (telaPrincipal.opcaoEscolhida == "S")
                {
                    Console.WriteLine("Saindo do sistema...");
                    break;
                }
            }
        }

        private static void GerenciarRevistas(TelaRevista telaRevista, TelaPrincipal telaPrincipal)
        {
            telaRevista.ExibirOpcoesMenu();

            if (telaRevista.opcaoEscolhida == "1")
                telaRevista.CadastrarRegistro();
            else if (telaRevista.opcaoEscolhida == "2")
                telaRevista.VisualizarRegistros(mostrarCabecalho: true);
            else if (telaRevista.opcaoEscolhida == "3")
                telaRevista.EditarRegistro();
            else if (telaRevista.opcaoEscolhida == "4")
                telaRevista.ExcluirRegistro();
        }

        private static void GerenciarCaixas(TelaCaixa telaCaixa, TelaPrincipal telaPrincipal)
        {
            telaCaixa.ExibirOpcoesMenu();

            if (telaCaixa.opcaoEscolhida == "1")
                telaCaixa.CadastrarRegistro();
            else if (telaCaixa.opcaoEscolhida == "2")
                telaCaixa.VisualizarRegistros(mostrarCabecalho: true);
            else if (telaCaixa.opcaoEscolhida == "3")
                telaCaixa.EditarRegistro();
            else if (telaCaixa.opcaoEscolhida == "4")
                telaCaixa.ExcluirRegistro();
        }

        private static void GerenciarAmigos(TelaAmigo telaAmigo, TelaPrincipal telaPrincipal)
        {
            telaAmigo.ExibirOpcoesMenu();

            if (telaAmigo.opcaoEscolhida == "1")
                telaAmigo.CadastrarRegistro();
            else if (telaAmigo.opcaoEscolhida == "2")
                telaAmigo.VisualizarRegistros(mostrarCabecalho: true);
            else if (telaAmigo.opcaoEscolhida == "3")
                telaAmigo.EditarRegistro();
            else if (telaAmigo.opcaoEscolhida == "4")
                telaAmigo.ExcluirRegistro();
        }


    }
}
