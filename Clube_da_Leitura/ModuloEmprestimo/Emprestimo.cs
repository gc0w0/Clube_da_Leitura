using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloMultas;
using Clube_da_Leitura.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloEmprestimo
{

    public class Emprestimo : EntidadeBase<Emprestimo>
    {
        public Amigo amigo;
        public Revista revista;
        public DateTime? dataEmprestimo, dataDevolucao, dataPrevistaDevolucao;
        public SituacaoEmprestimo situacao;//Aberto / Concluido / Atrasado
        public List<Multa> multa = new List<Multa>();
        public RepositorioMulta repositorioMulta;

        public Emprestimo()
        {
            
        }
        public Emprestimo(Amigo amigo, Revista revista)
        {
            this.amigo = amigo;
            this.revista = revista;            
            amigo.emprestimos.Add(this);
            revista.emprestimos.Add(this);

            RegistrarEmprestimo();
        }

        private void RegistrarEmprestimo()
        {
            this.situacao = SituacaoEmprestimo.Aberto;

            //this.dataEmprestimo = DateTime.Now; //TODO alterando pra 2 dias antes pra testar 
            this.dataEmprestimo = new DateTime(2025, 06, 01); // para teste


            int diasEmprestimo = revista.caixa.dias;
            dataPrevistaDevolucao = DateTime.Now.AddDays(diasEmprestimo - 5);

            //dataDevolucao = DateTime.Now.AddDays(-1);
        }

       

        public override void AtualizarInformacoes(Emprestimo emprestimoAtualizado)
        {
            this.dataEmprestimo = emprestimoAtualizado.dataEmprestimo;
            this.dataPrevistaDevolucao = emprestimoAtualizado.dataPrevistaDevolucao;
        }

        public override void MostrarInformacoes()
        {
            Console.WriteLine($"ID de Registro: {id} | Data Emprestimo: {dataEmprestimo} | DataDevolução {dataPrevistaDevolucao} | Status: {situacao}");
        }

        public override string Validar()
        {
            string resultadoValidacao = "";

            return resultadoValidacao;
        }

        public void RegistrarDevolucao(DateTime dataDevolucao)
        {
            int diasDeAtraso = (int)((dataDevolucao - dataPrevistaDevolucao)?.TotalDays ?? 0);

            if (diasDeAtraso > 0)
            {
                float valorMulta = diasDeAtraso * 2;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Essa devolução está {diasDeAtraso} dias atrasada. Multa de R$ {valorMulta:0.00}");
                Console.ResetColor();

                Console.Write("Deseja pagar a multa agora? (S/N): ");
                string resposta = Console.ReadLine().ToUpper();

                if (resposta == "S")
                {
                    Multa multaPaga = new Multa(diasDeAtraso);
                    multaPaga.situacao = SituacaoMulta.Quitada;
                    this.amigo.multas.Add(multaPaga);
                    this.amigo.multas.Remove(multaPaga);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Multa paga com sucesso.");
                    Console.ResetColor();

                }
                else
                {
                    Multa multaPendente = new Multa(diasDeAtraso);
                    multaPendente.situacao = SituacaoMulta.Pendente;
                    this.amigo.multas.Add(multaPendente);

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Multa pendente registrada.");
                    Console.ResetColor();
                }
            }
            this.amigo.emprestimos.Remove(this);
            situacao = SituacaoEmprestimo.Fechado;
        }
    }

}
