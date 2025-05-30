using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloRevista;
using static Clube_da_Leitura.ModuloCaixa.Caixa;

namespace Clube_da_Leitura
{
    internal class Program
    {      
        static void Main(string[] args)
        {                        
            var repositorioAmigo = new RepositorioAmigo();
            var telaAmigo = new TelaAmigo(repositorioAmigo);
            var amigo = new Amigo("Markswell", "Gabriel", "49984327736");
            var amigo2 = new Amigo("Gregory", "Gabriel", "11111111111");

            repositorioAmigo.InserirRegistro(amigo);
            repositorioAmigo.InserirRegistro(amigo2);


            var repositorioCaixa = new RepositorioCaixa();
            var telaCaixa = new TelaCaixa(repositorioCaixa);
            var caixa = new Caixa("Etiqueta Teste", CorCaixa.Vermelha, 7);
            repositorioCaixa.InserirRegistro(caixa);

            var repositorioRevista = new RepositorioRevista();
            var telaRevista = new TelaRevista(repositorioCaixa, telaCaixa, repositorioRevista);
            var revista = new Revista("Pequeno Principe", 2, 2025, caixa, Revista.StatusDisponveis.Disponivel);
            repositorioRevista.InserirRegistro(revista);


            var repositorioEmprestimo = new RepositorioEmprestimo();

            var telaEmprestimo = new TelaEmprestimo(
                repositorioEmprestimo,  repositorioAmigo,  repositorioRevista,  
                repositorioCaixa,  telaAmigo,  telaRevista,  telaCaixa );
           
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

                else if (telaPrincipal.opcaoEscolhida == "4")
                    GerenciarEmprestimos(telaEmprestimo, telaPrincipal);

                else if (telaPrincipal.opcaoEscolhida == "S")
                {
                    Console.WriteLine("Saindo do sistema...");
                    break;
                }
            }
        }

        private static void GerenciarEmprestimos(TelaEmprestimo telaEmprestimo, TelaPrincipal telaPrincipal)
        {
            telaEmprestimo.ExibirOpcoesMenu();

            if (telaEmprestimo.opcaoEscolhida == "1")
                telaEmprestimo.CadastrarRegistro();
            else if (telaEmprestimo.opcaoEscolhida == "2")
                telaEmprestimo.VisualizarEmprestimosAbertos(mostrarCabecalho: true);
            else if (telaEmprestimo.opcaoEscolhida == "3")
                telaEmprestimo.VisualizarEmprestimosFechados(mostrarCabecalho: true);
            else if (telaEmprestimo.opcaoEscolhida == "4")
                telaEmprestimo.RegistrarDevolucao();
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
                telaCaixa.ExcluirRegistro(a => a.revistas.Count > 0); //passa por parametro a validação de registro vinculados.
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
                //TODO Verificar os Emprestimos de Amigo.
                //TODO Feito para que possa validar relacionamentos em qualqeur lugar.
                telaAmigo.ExcluirRegistro(a => a.emprestimos.Count > 0); //passa por parametro a validação registro vinculados.
        }



    }
}
