using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloRevista;

namespace Clube_da_Leitura.ModuloReserva
{
    public class Reserva : EntidadeBase<Reserva>
    {
        public Amigo Amigo { get; set; }
        public Revista Revista { get; set; }
        public DateTime DataReserva { get; set; }
        public SituacaoReserva Situacao { get; set; }

        public Reserva()
        {

        }
        public Reserva(Amigo amigo, Revista revista)
        {
            this.Amigo = amigo;
            this.Revista = revista;
            this.DataReserva = DateTime.Now;
            RegistrarReserva();
        }

        private void RegistrarReserva()
        {
            this.Situacao = SituacaoReserva.Ativa;
            Revista.Status = Revista.StatusDisponveis.Reservada;

        }

        public override void AtualizarInformacoes(Reserva reservaAtualizada)
        {
            this.DataReserva = reservaAtualizada.DataReserva;
        }

        public override void MostrarInformacoes()
        {
            Console.WriteLine($"ID de Registro: {Id} | Amigo: {Amigo.Nome} | Revista: {Revista.Titulo} | Status: {Situacao} | Data da Reserva {DataReserva}");
        }

        public override string Validar()
        {
            string resultadoValidacao = "";

            return resultadoValidacao;
        }
    }
}
