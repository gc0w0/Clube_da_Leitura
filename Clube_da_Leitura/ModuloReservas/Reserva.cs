using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloReservas
{
    public class Reserva : EntidadeBase<Reserva>
    {
        public Amigo amigo;
        public Revista revista;
        public DateTime dataReserva;
        public SituacaoReserva situacao;

        public Reserva()
        {
            
        }
        public Reserva(Amigo amigo, Revista revista)
        {
            this.amigo = amigo;
            this.revista = revista;
            this.dataReserva = DateTime.Now;
            RegistrarReserva();
        }

        private void RegistrarReserva()
        {
            this.situacao = SituacaoReserva.Ativa;
            revista.status = Revista.StatusDisponveis.Reservada;

        }

        public override void AtualizarInformacoes(Reserva reservaAtualizada)
        {
            this.dataReserva = reservaAtualizada.dataReserva;
        }

        public override void MostrarInformacoes()
        {
            Console.WriteLine($"ID de Registro: {id} | Amigo: {amigo.nome} | Revista: {revista.titulo} | Status: {situacao} | Data da Reserva {dataReserva}");
        }

        public override string Validar()
        {
            string resultadoValidacao = "";

            return resultadoValidacao;
        }
    }
}
