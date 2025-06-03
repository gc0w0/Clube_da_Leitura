using Clube_da_Leitura.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloReservas
{
    public class TelaReserva : TelaBase<Reserva>
    {
        public override string ExibirOpcoesMenu()
        {
            Console.Clear();
            Console.WriteLine($"Bem-vindo ao Clube da Leitura!\n");
            Console.WriteLine($"Digite 1 para registrar Reservas");
            Console.WriteLine($"Digite 2 para cancelar Reservas");
            Console.WriteLine($"Digite 3 para exibir Reservas ativas:");

            Console.WriteLine("Digite S para sair");
            Console.Write(">: ");

            opcaoEscolhida = Console.ReadLine();
            return opcaoEscolhida;
        }

        public override void ExibirCabecalhoTabela()
        {
            throw new NotImplementedException();
        }

        public override void ExibirLinhaTabela(Reserva registro)
        {
            throw new NotImplementedException();
        }

        public override Reserva ObterDados()
        {
            throw new NotImplementedException();
        }
    }
}
