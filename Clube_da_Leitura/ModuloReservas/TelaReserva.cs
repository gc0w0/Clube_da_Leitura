using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloMultas;
using Clube_da_Leitura.ModuloRevista;

namespace Clube_da_Leitura.ModuloReservas
{
    public class TelaReserva : TelaBase<Reserva>
    {
        private const string formatoColunasTabela = "{0, -10} | {1, -20} | {2, -25} | {3, -18} | {4, -10}";
        private RepositorioEmprestimo repositorioEmprestimo;
        private RepositorioReserva repositorioReserva;
        private IRepositorioAmigo repositorioAmigo;
        private RepositorioRevista repositorioRevista;
        private RepositorioMulta repositorioMulta;

        private TelaAmigo telaAmigo;
        private TelaRevista telaRevista;
        private TelaMulta telaMulta;
        public Emprestimo emprestimo;
        public Amigo amigo;
        public Revista revista;
        public Multa multa;

        public TelaReserva(RepositorioEmprestimo repositorioEmprestimo, RepositorioReserva repositorioReserva,
            IRepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista, RepositorioMulta repositorioMulta,
            TelaAmigo telaAmigo, TelaRevista telaRevista, TelaMulta telaMulta)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.repositorioReserva = repositorioReserva;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
            this.repositorioMulta = repositorioMulta;
            this.telaAmigo = telaAmigo;
            this.telaRevista = telaRevista;
            this.telaMulta = telaMulta;
            repositorio = repositorioReserva;

            modulo = "Reservas";
        }

        public override string ExibirOpcoesMenu()
        {
            Console.Clear();
            Console.WriteLine($"Bem-vindo ao Clube da Leitura!\n");
            Console.WriteLine($"Digite 1 para registrar Reservas");
            Console.WriteLine($"Digite 2 para cancelar Reservas");
            Console.WriteLine($"Digite 3 para exibir Reservas ativas:");
            Console.WriteLine($"Digite 4 para retirar revista:");

            Console.WriteLine("Digite S para sair");
            Console.Write(">: ");

            opcaoEscolhida = Console.ReadLine();
            return opcaoEscolhida;
        }

        public override void ExibirCabecalhoTabela()
        {
            Console.WriteLine(formatoColunasTabela, "Id", "Amigo", "Revista", "Status", "Data Reserva");
        }

        public override void ExibirLinhaTabela(Reserva r)
        {
            Console.WriteLine(formatoColunasTabela, r.id, r.amigo.nome, r.revista.titulo, r.situacao, r.dataReserva);
        }

        public override Reserva ObterDados()
        {
            this.telaAmigo.VisualizarRegistros(mostrarCabecalho: false);
            Console.Write("Digite o seu ID de amigo: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());

            Amigo amigoSelecionado = repositorioAmigo.SelecionarPorId(idAmigo);

            bool jaTemEmprestimoAtivo = amigoSelecionado.emprestimos.Any(e => e.id >= 1);
            bool temMultaPendente = amigoSelecionado.multas
            .Any(m => m.situacao == SituacaoMulta.Pendente);

            if (temMultaPendente)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Este amigo possui uma multa em aberto e não pode realizar reservas até a quitação!");
                Console.ResetColor();
                Console.ReadKey();
                return ObterDados(); 
            }

            if (jaTemEmprestimoAtivo)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Este amigo já possui um empréstimo em aberto e não pode fazer outro!");
                Console.ResetColor();
                Console.ReadKey();
                return ObterDados();
            }


            this.telaRevista.VisualizarRegistros(mostrarCabecalho: false);
            Console.Write("Digite o ID da revista que deseja pegar emprestada: ");
            int idRevista = Convert.ToInt32(Console.ReadLine());

            Revista revistaSelecionada = repositorioRevista.SelecionarPorId(idRevista);

            bool revistaEmprestada = revistaSelecionada.emprestimos
                .Any(e => e.situacao == SituacaoEmprestimo.Aberto || e.situacao == SituacaoEmprestimo.Atrasado);

            bool revistaReservada = revistaSelecionada.reserva
            .Any(r => r.situacao == SituacaoReserva.Ativa);

            if (revistaEmprestada)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Essa revista está emprestada no momento, tente outra!");
                Console.ResetColor();
                Console.ReadKey();
                return ObterDados();
            }

            else if (revistaReservada)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Essa revista já está reservada por outra pessoa.");
                Console.ResetColor();
                Console.ReadKey();
                return ObterDados();
            }


            return new Reserva(amigoSelecionado, revistaSelecionada);


        }

        public void VisualizarReservasAbertas(bool mostrarCabecalho)
        {
            if (mostrarCabecalho)
            {
                Console.Clear();

                Console.WriteLine($"Módulo de {modulo}"); //título

                Console.WriteLine($"Visualizando reservas em aberto de {modulo}..."); //subtítulo
            }

            Console.WriteLine();

            ExibirCabecalhoTabela();

            Console.WriteLine("-------------------------------------------------------------------------------------------------");

            List<Reserva> registros = repositorioReserva.SelecionarTodosAbertos();

            foreach (var registro in registros)
            {
                if (registro == null)
                    continue;

                ExibirLinhaTabela(registro);
            }

            Console.ReadLine();
        }

        public void CancelarReservas()
        {
            Console.Clear();

            Console.WriteLine($"Módulo de {modulo}");

            Console.WriteLine($"Cancelando reservas em aberto de {modulo}...");

            Console.WriteLine();

            ExibirCabecalhoTabela();

            Console.WriteLine("-------------------------------------------------------------------------------------------------");

            List<Reserva> registros = repositorioReserva.SelecionarTodosAbertos();

            foreach (var registro in registros)
            {
                if (registro == null)
                    continue;

                ExibirLinhaTabela(registro);
            }

            Console.ReadLine();
            Console.Write("Insira o ID da revista para cancelar a reserva: ");
            int idRevista = Convert.ToInt32(Console.ReadLine());
            Revista revistaSelecionada = repositorioRevista.SelecionarPorId(idRevista);
            revistaSelecionada.status = Revista.StatusDisponveis.Disponivel;
            //Emprestimo emprestimoSelecionado = repositorioEmprestimo.SelecionarPorId(idRevista);
            //emprestimoSelecionado.situacao = SituacaoEmprestimo.Fechado;
            Reserva reservaSelecionada = repositorioReserva.SelecionarPorId(idRevista);
            reservaSelecionada.situacao = SituacaoReserva.Cancelada;
            Console.WriteLine("O Cancelamento da reserva da revista {0} registrada com sucesso", revistaSelecionada.titulo);
            Console.ReadKey();
        }

        public Emprestimo RetirarRevista()
        {
            Console.Clear();

            Console.WriteLine($"Módulo de {modulo}");

            Console.WriteLine($"Retirando resvistas reservadas de {modulo}...");

            Console.WriteLine();

            ExibirCabecalhoTabela();

            Console.WriteLine("-------------------------------------------------------------------------------------------------");

            List<Reserva> registros = repositorioReserva.SelecionarTodosAbertos();
            
            foreach (var registro in registros)
            {
                if (registro == null)
                    continue;

                ExibirLinhaTabela(registro);
            }

            Console.ReadLine();
            Console.Write("Insira o ID do amigo  para retirar a revista: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());
            Revista revistaSelecionada = repositorioRevista.SelecionarPorId(idAmigo);
            revistaSelecionada.status = Revista.StatusDisponveis.Emprestada;
            Emprestimo emprestimoSelecionado = repositorioEmprestimo.SelecionarPorId(idAmigo);
            emprestimoSelecionado.situacao = SituacaoEmprestimo.Aberto;
            Reserva reservaSelecionada = repositorioReserva.SelecionarPorId(idAmigo);
            reservaSelecionada.situacao = SituacaoReserva.Concluida;
            Amigo amigoSelecionado = repositorioAmigo.SelecionarPorId(idAmigo);

            Console.WriteLine("A retirada da reserva da revista {0} registrada com sucesso", revistaSelecionada.titulo);
            Console.WriteLine("Emprestimo registrado.");
            Console.ReadKey();

            var emprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada);
            return emprestimo;
        }
    }
}

