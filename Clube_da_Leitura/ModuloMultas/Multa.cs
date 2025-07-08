using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.Dominio.ModuloMultas;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloRevista;

namespace Clube_da_Leitura.ModuloMultas
{
    public class Multa : EntidadeBase<Multa>
    {
        public Amigo amigo;
        public Revista revista;
        public Emprestimo emprestimo;
        public SituacaoMulta situacao;
        public float valorMulta;

        public Multa()
        {

        }
        public Multa(Amigo amigo, Revista revista, Emprestimo emprestimo, SituacaoMulta situacao, float valorMulta)
        {
            this.amigo = amigo;
            this.revista = revista;
            this.emprestimo = emprestimo;
            this.situacao = situacao;
            this.valorMulta = valorMulta;
        }

        public Multa(int diasDeAtraso)
        {
            valorMulta = diasDeAtraso * 2;

        }

        public override void AtualizarInformacoes(Multa multaAtualizada)
        {
            this.situacao = multaAtualizada.situacao;
            this.valorMulta = multaAtualizada.valorMulta;
        }

        public override void MostrarInformacoes()
        {
            Console.WriteLine($"ID de Registro: {Id} | DataDevolução {emprestimo.dataPrevistaDevolucao} | Status: {situacao} | Valor da Multa: R$:{valorMulta}");
        }

        public override string Validar()
        {
            string resultadoValidacao = "";

            return resultadoValidacao;
        }

        public override string ToString()
        {
            return valorMulta + " - " + situacao;
        }
    }
}
