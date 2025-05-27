using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.Compartilhado
{
    public class TelaPrincipal
    {
        public string opcaoEscolhida;

        public void ExibirOpcoesMenu()
        {
            Console.Clear();

            Console.WriteLine("Bem-vindo ao Clube da Leitura!\n");

            Console.WriteLine("Digite 1 para gerenciar Amigos:");
            Console.WriteLine("Digite 2 para gerenciar Caixas:");
            Console.WriteLine("Digite 3 para gerenciar Revistas");
            Console.WriteLine("Digite 4 para gerenciar Empréstimos");

            Console.WriteLine("Digite S para sair");
            Console.Write(">: ");

            opcaoEscolhida = Console.ReadLine();
        }
    }
}
