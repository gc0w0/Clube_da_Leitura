using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloCaixa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloRevista
{
    public class TelaRevista : TelaBase<Revista>
    {
        private const string formatoColunasTabela = "{0, -10} | {1, -20} | {2, -15} | {3, -15}";
        private RepositorioCaixa repositorioCaixa;
        private TelaCaixa telaCaixa;

        public TelaRevista(RepositorioCaixa repositorioCaixa, TelaCaixa telaCaixa, RepositorioRevista repositorioRevista)
        {
            this.telaCaixa = telaCaixa;
            this.repositorioCaixa = repositorioCaixa;
            repositorio = repositorioRevista;
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

            Console.Write("Digite o novo status do livro: ");
            string novoStatus = Console.ReadLine();

            this.telaCaixa.VisualizarRegistros(mostrarCabecalho: false);

            Console.Write("Digite o ID do Caixa que deseja selecionar: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine());
            Caixa caixaSelecionado = repositorioCaixa.SelecionarPorId(idCaixa);

            return new Revista(novoTitulo, novaEdicao, novoAnoPublicacao, caixaSelecionado, novoStatus);
        }
    }
}
