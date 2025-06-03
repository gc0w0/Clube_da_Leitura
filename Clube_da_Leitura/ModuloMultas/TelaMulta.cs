using Clube_da_Leitura.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloMultas
{
    public class TelaMulta : TelaBase<Multa>
    {

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
            throw new NotImplementedException();
        }

        public override void ExibirLinhaTabela(Multa registro)
        {
            throw new NotImplementedException();
        }

        public override Multa ObterDados()
        {
            throw new NotImplementedException();
        }
    }
}
