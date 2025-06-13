using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using Gestao_de_Equipamentos.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Clube_da_Leitura.ModuloCaixa.Caixa;
using static Clube_da_Leitura.ModuloRevista.Revista;

namespace Clube_da_Leitura.ModuloRevista
{
    public class TelaRevista : TelaBase<Revista>
    {
        private const string formatoColunasTabela = "{0, -10} | {1, -20} | {2, -20} | {3, -15}";
        private IRepositorioCaixa repositorioCaixa;
        private IRepositorioRevista repositorioRevista;
        private TelaCaixa telaCaixa;

        public TelaRevista(IRepositorioCaixa repositorioCaixa, TelaCaixa telaCaixa,
            IRepositorioRevista repositorioRevista)
        {
            this.telaCaixa = telaCaixa;
            this.repositorioCaixa = repositorioCaixa;

            repositorio = (RepositorioBase<Revista>)repositorioRevista;
            modulo = "Revistas";

        }

        public override void ExibirCabecalhoTabela()
        {
            Console.WriteLine(formatoColunasTabela, "Id", "Titulo", "Ano de Publicação", "Status");
        }

        public override void ExibirLinhaTabela(Revista r)
        {
            Console.WriteLine(formatoColunasTabela, r.id, r.titulo, r.anoPublicacao, r.status);
        }

        public override Revista ObterDados()
        {
            Console.Write("Digite o novo Titulo: ");
            string novoTitulo = Console.ReadLine();

            Console.Write("Digite o novo numero da Edição: ");
            int novaEdicao = int.Parse(Console.ReadLine());

            Console.Write("Digite o novo ano de publicação: ");
            int novoAnoPublicacao = int.Parse(Console.ReadLine());

            Console.WriteLine("Escolha o status da revista (ou pressione Enter para 'Disponivel'): ");
            foreach (StatusDisponveis status in Enum.GetValues(typeof(StatusDisponveis)))
            {
                Console.WriteLine($"| {(int)status} - {status}");
            }
            Console.Write("Digite o numero da Opção: ");
            string entradaStatus = Console.ReadLine();
            StatusDisponveis novoStatus;

            if (string.IsNullOrEmpty(entradaStatus))
            {
                novoStatus = StatusDisponveis.Disponivel;
            }
            else
            {
                int statusSelecionado;
                while (!int.TryParse(entradaStatus, out statusSelecionado) || !Enum.IsDefined(typeof(StatusDisponveis), statusSelecionado))
                {
                    Console.Write("Opção inválida. Escolha novamente: ");
                    entradaStatus = Console.ReadLine();
                }
                novoStatus = (StatusDisponveis)statusSelecionado;
            }

            var repositorioRevista = (RepositorioRevista)repositorio;

            bool tituloDuplicado = repositorioRevista.Validacoes(a => a.titulo == novoTitulo);
            bool edicaoDuplicada = repositorioRevista.Validacoes(a => a.numeroEdicao == novoAnoPublicacao);
            if (tituloDuplicado)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nJá existe uma revista com esse \"Titulo\"!");
                Console.ResetColor();
                return ObterDados(); 
            }

            else if (edicaoDuplicada)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nJá existe uma revista com essa \"Edição\"!");
                Console.ResetColor();
                return ObterDados();
            }

            this.telaCaixa.VisualizarRegistros(mostrarCabecalho: false);

            Console.Write("Digite o ID do Caixa que deseja selecionar: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine());
            Caixa caixaSelecionado = repositorioCaixa.SelecionarPorId(idCaixa);

            return new Revista(novoTitulo, novaEdicao, novoAnoPublicacao, caixaSelecionado, novoStatus);
        }
    }
}
