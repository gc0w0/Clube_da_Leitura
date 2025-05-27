using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;

namespace Clube_da_Leitura
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var repositorioAmigo = new RepositorioAmigo();
            var telaAmigo = new TelaAmigo(repositorioAmigo);

            var telaPrincipal = new TelaPrincipal();

            while (true)
            {
                telaPrincipal.ExibirOpcoesMenu();

                if (telaPrincipal.opcaoEscolhida == "1")
                    GerenciarAmigos(telaAmigo, telaPrincipal);

                else if (telaPrincipal.opcaoEscolhida == "S")
                {
                    Console.WriteLine("Saindo do sistema...");
                    break;
                }
            }
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
