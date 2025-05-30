using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
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
        public DateTime dataEmprestimo;
        public DateTime dataDevolucao;
        public SituacaoEmprestimo situacao;//Aberto / Concluido / Atrasado

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
            revista.status = Revista.StatusDisponveis.Emprestada;

            this.dataEmprestimo = DateTime.Now;

            int diasEmprestimo = revista.caixa.dias;
            dataDevolucao = DateTime.Now.AddDays(diasEmprestimo);
        }

       

        public override void AtualizarInformacoes(Emprestimo emprestimoAtualizado)
        {
            this.dataEmprestimo = emprestimoAtualizado.dataEmprestimo;
            this.dataDevolucao = emprestimoAtualizado.dataDevolucao;
        }

        public override void MostrarInformacoes()
        {
            Console.WriteLine($"ID de Registro: {id} | Data Emprestimo: {dataEmprestimo} | DataDevolução {dataDevolucao} | Status: {situacao}");
        }

        public override string Validar()
        {
            string resultadoValidacao = "";

            return resultadoValidacao;
        }
    }

}
