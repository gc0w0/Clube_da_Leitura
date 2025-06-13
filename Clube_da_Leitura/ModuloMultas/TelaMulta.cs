using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloRevista;

namespace Clube_da_Leitura.ModuloMultas
{
    public class TelaMulta : TelaBase<Multa>
    {
        private RepositorioMulta repositorioMulta;
        private IRepositorioAmigo repositorioAmigo;
        private RepositorioRevista repositorioRevista;
        public RepositorioEmprestimo repositorioEmprestimo;
        private Emprestimo emprestimos;
        private List<Multa> multas;
        private TelaAmigo telaAmigo;
        private TelaRevista telaRevista;
        private TelaMulta telaMulta;
        public TelaEmprestimo telaEmprestimo;

        public TelaMulta(RepositorioMulta repositorioMulta, IRepositorioAmigo repositorioAmigo, RepositorioEmprestimo repositorioEmprestimo, TelaAmigo telaAmigo, TelaEmprestimo telaEmprestimo, Emprestimo emprestimos)
        {
            this.repositorioMulta = repositorioMulta;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.telaAmigo = telaAmigo;
            this.telaEmprestimo = telaEmprestimo;
            this.emprestimos = emprestimos;
            this.repositorio = repositorioMulta;
            modulo = "Multas";
        }

        private const string formatoColunasTabela = "{0, -10} | {1, -20} | {2, -25} | {3, -15}";
        public override string ExibirOpcoesMenu()
        {
            Console.Clear();
            Console.WriteLine($"Bem-vindo ao Clube da Leitura!\n");
            Console.WriteLine($"Digite 1 para visualizar multas em Aberto");
            Console.WriteLine($"Digite 2 para quitar Multas");
            Console.WriteLine($"Digite 3 para exibir multas de Amigo:");
            Console.WriteLine($"Digite 4 para Gerar Multas:");

            Console.WriteLine("Digite S para sair");
            Console.Write(">: ");

            opcaoEscolhida = Console.ReadLine();
            return opcaoEscolhida;
        }

        public override void ExibirCabecalhoTabela()
        {
            Console.WriteLine(formatoColunasTabela, "Id", "Data Devolução", "Status", "Valor Multa");
        }

        public override void ExibirLinhaTabela(Multa m)
        {
            Console.WriteLine(formatoColunasTabela, m.id, emprestimos.dataPrevistaDevolucao, m.situacao, m.valorMulta);
        }

        public override Multa ObterDados()
        {
            throw new NotImplementedException();
        }

        public void VisualizarMultasAbertas(bool mostrarCabecalho)
        {
            if (mostrarCabecalho)
            {
                Console.Clear();

                Console.WriteLine($"Módulo de {modulo}"); //título

                Console.WriteLine($"Visualizando {modulo} em aberto..."); //subtítulo
            }

            Console.WriteLine();

            ExibirCabecalhoTabela();

            Console.WriteLine("-------------------------------------------------------------------------------------------------");

            List<Multa> registros = repositorioMulta.SelecionarTodosAbertos();

            foreach (var registro in registros)
            {
                if (registro == null)
                    continue;

                ExibirLinhaTabela(registro);
            }

            Console.ReadLine();
        }

        public void QuitarMultas()
        {
            Console.Clear();

            Console.WriteLine($"Módulo de {modulo}");

            Console.WriteLine($"Registrando devoluções em aberto de {modulo}...");

            Console.WriteLine();

            ExibirCabecalhoTabela();

            var registros = repositorioMulta.SelecionarTodosAbertos();

            foreach (var registro in registros)
            {
                if (registro == null)
                    continue;
                ExibirLinhaTabela(registro);
            }

            Console.ReadLine();
            Console.Write("Insira o ID da Multa para registrar o pagamento e a devolução da Revista: ");
            int idMulta = Convert.ToInt32(Console.ReadLine());

            Multa multaSelecionada = repositorioMulta.SelecionarPorId(idMulta);
            multaSelecionada.situacao = SituacaoMulta.Quitada;
            Emprestimo emprestimoSelecionado = repositorioEmprestimo.SelecionarPorId(idMulta);
            emprestimoSelecionado.situacao = SituacaoEmprestimo.Fechado;
            

            Console.WriteLine("Pagamento da Multa no valor de \"R$:{0}\" registrada com sucesso e Revista: \"{1}\" devolvida.", multaSelecionada.valorMulta, emprestimoSelecionado.revista.titulo);
            Console.ReadKey();


        }

        public void ExibirMultasAmigo(bool mostrarCabecalho)
        {
            throw new NotImplementedException();
        }

        public void GerarMultas()
        {
            Console.Clear();
            Console.WriteLine("Gerando multas por atraso...\n");

            var emprestimos = repositorioEmprestimo.SelecionarTodos();

            foreach (Emprestimo e in emprestimos)
            {
                if (e == null || e.situacao != SituacaoEmprestimo.Aberto)
                    continue;

                if (DateTime.Now.Date > e.dataPrevistaDevolucao.Date)
                {
                    int diasAtraso = (DateTime.Now.Date - e.dataPrevistaDevolucao.Date).Days;
                    int valorMulta = diasAtraso * 2; 
                    var multaExistente = e.multa?.FirstOrDefault(m => m.situacao == SituacaoMulta.Pendente);

                    if (multaExistente != null)
                    {
                        multaExistente.valorMulta = valorMulta;
                        Console.WriteLine($"Multa atualizada para {e.amigo.nome} - R$ {valorMulta} ({diasAtraso} dias de atraso)");
                    }
                    else
                    {
                        var novaMulta = new Multa(e.amigo, e.revista, e, SituacaoMulta.Pendente, valorMulta);
                        repositorioMulta.InserirRegistro(novaMulta);
                        e.situacao = SituacaoEmprestimo.Atrasado;

                        Console.WriteLine($"Multa criada para {e.amigo.nome} - R$ {valorMulta} ({diasAtraso} dias de atraso)");
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nMultas processadas com sucesso!");
            Console.ResetColor();
            Console.ReadKey();
        }

    }
}
