using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloMultas;
using Clube_da_Leitura.ModuloReservas;
using Clube_da_Leitura.ModuloRevista;
using static Clube_da_Leitura.ModuloCaixa.Caixa;

namespace Clube_da_Leitura
{
    internal class Program
    {      
        static void Main(string[] args)
        {                        
            
            var repositorioAmigo = new RepositorioAmigoEmArquivo();
            var telaAmigo = new TelaAmigo(repositorioAmigo);
            //var amigo = new Amigo("Markswell", "Gabriel", "49984327736");
            //var amigo2 = new Amigo("Gregory", "Gabriel", "11111111111");

            //repositorioAmigo.InserirRegistro(amigo); 
            //repositorioAmigo.InserirRegistro(amigo2);

            var repositorioCaixa = new RepositorioCaixaEmArquivo();
            var telaCaixa = new TelaCaixa(repositorioCaixa);
            //var caixa = new Caixa("Etiqueta Teste", CorCaixa.Vermelha, 2);
            //var caixa2 = new Caixa("Caixa 2", CorCaixa.Amarela, 1);
            //repositorioCaixa.InserirRegistro(caixa);
            //repositorioCaixa.InserirRegistro(caixa2);

            var repositorioRevista = new RepositorioRevistaEmArquivo();
            var telaRevista = new TelaRevista(repositorioCaixa, telaCaixa, repositorioRevista);
            //var revista = new Revista("Pequeno Principe", 2, 2025, caixa, Revista.StatusDisponveis.Emprestada);
            //var revista2 = new Revista("Teste2", 3, 1999, caixa2, Revista.StatusDisponveis.Disponivel);
            //repositorioRevista.InserirRegistro(revista);
            //repositorioRevista.InserirRegistro(revista2);

            var repositorioEmprestimo = new RepositorioEmprestimoEmArquivo();
            //var emprestimo = new Emprestimo(amigo, revista);
            //var emprestimo2 = new Emprestimo(amigo2, revista2);
            //repositorioEmprestimo.InserirRegistro(emprestimo);
            //repositorioEmprestimo.InserirRegistro(emprestimo2);
            //repositorioEmprestimo.InserirRegistro(emprestimo2);

            var repositorioMulta = new RepositorioMulta();

            var repositorioReserva = new RepositorioReservaEmArquivo();
            //var reserva = new Reserva(amigo, revista);
            //repositorioReserva.InserirRegistro(reserva);
            var telaEmprestimo = new TelaEmprestimo(
                repositorioEmprestimo,  repositorioAmigo,  repositorioRevista,  
                repositorioCaixa,  telaAmigo,  telaRevista,  telaCaixa, repositorioMulta);

            //var telaMulta = new TelaMulta(repositorioMulta, repositorioAmigo, repositorioEmprestimo, telaAmigo, telaEmprestimo, emprestimo);
            var telaReserva = new TelaReserva(repositorioEmprestimo, repositorioReserva, repositorioAmigo, repositorioRevista, 
                repositorioMulta, telaAmigo, telaRevista);

           
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
            else if (telaAmigo.opcaoEscolhida == "5")
                telaAmigo.QuitarMulta();
            else if (telaAmigo.opcaoEscolhida == "6")
                telaAmigo.FiltroAmigos();
        }



    }
}
