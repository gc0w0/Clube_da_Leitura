using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloMultas
{
    public class Multa : EntidadeBase<Multa>
    {
        public Amigo amigo;
        public Revista revista;
        public Emprestimo emprestimo;
        public SituacaoMulta situacao;
        public float valorMulta;


        public Multa(Amigo amigo, Revista revista, Emprestimo emprestimo, SituacaoMulta situacao, float valorMulta)
        {
            this.amigo = amigo;
            this.revista = revista;
            this.emprestimo = emprestimo;
            this.situacao = situacao;
            this.valorMulta = valorMulta;
            RegistrarMulta();
            amigo.multas.Add(this);
            emprestimo.multa.Add(this);

        }

        public Multa(int diasDeAtraso)
        {
            valorMulta = diasDeAtraso * 2;

        }

        public void RegistrarMulta()
        {
            this.valorMulta = 2.0f;
            this.situacao = SituacaoMulta.Quitada;

            if (DateTime.Now > emprestimo.dataPrevistaDevolucao)
            {
                valorMulta = +2.0f;
                situacao = SituacaoMulta.Pendente;
            }
        }

        public override void AtualizarInformacoes(Multa multaAtualizada)
        {
            this.situacao = multaAtualizada.situacao;
            this.valorMulta = multaAtualizada.valorMulta;
        }

        public override void MostrarInformacoes()
        {
            Console.WriteLine($"ID de Registro: {id} | DataDevolução {emprestimo.dataPrevistaDevolucao} | Status: {situacao} | Valor da Multa: R$:{valorMulta}");
        }

        public override string Validar()
        {
            string resultadoValidacao = "";

            return resultadoValidacao;
        }
    }
}
