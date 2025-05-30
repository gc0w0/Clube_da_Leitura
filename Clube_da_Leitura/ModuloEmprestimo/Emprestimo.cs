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
        public List<Amigo> amigos = new List<Amigo>();
        public List<Revista> revistas = new List<Revista>();
        public DateTime dataEmprestimo;
        public DateTime dataDevolucao;
        public string situacao;//Aberto / Concluido / Atrasado

        public Emprestimo(Amigo amigo, Revista revista, DateTime dataEmprestimo, DateTime dataDevolucao, string situacao)
        {

            this.dataEmprestimo = dataEmprestimo;
            this.dataDevolucao = dataDevolucao;
            //amigo.emprestimo.Add(this);
            revista.emprestimos.Add(this);
            amigo.emprestimos.Add(this);
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

        //public string ObterDataDevolucao()
        //{
        //    throw new NotImplementedException();
        //}

        public string RegistrarDevolucao()
        {
            throw new NotImplementedException();
        }
    }

}
