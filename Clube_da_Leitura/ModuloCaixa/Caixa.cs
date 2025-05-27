using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloCaixa
{

    public class Caixa : EntidadeBase<Caixa>
    {
        public string etiqueta; //maximo de 50 caracteres
        public string cor; //seleção de paleta ou hexadecimal como fazer?? windows forms? 
        public int dias; // padrao 7
        public List<Revista> revistas = new List<Revista>();

        public Caixa(string etiqueta, string cor, int dias )
        {
            this.etiqueta = etiqueta;
            this.cor = cor;
            this.dias = dias;
        }

        public override void AtualizarInformacoes(Caixa caixaAtualizado)
        {
            this.etiqueta = caixaAtualizado.etiqueta;
            this.cor = caixaAtualizado .cor;
            this.dias = caixaAtualizado.dias;
        }

        public override void MostrarInformacoes()
        {
            Console.WriteLine($"ID de registro: {id} | Etiqueta: {etiqueta} | Cor: {cor} | Dias de Emprestimo: {dias}");
        }

        public override string Validar()
        {
            string resultadoValidacao = "";

            if (string.IsNullOrEmpty(etiqueta))
                resultadoValidacao += "O campo \"etiqueta\" é obrigatório" + "\n";

            if (etiqueta.Length > 50)
                resultadoValidacao += "O campo \"nome\" precisa ter no mínimo 3 letras" + "\n";

            if (cor.Length < 0)
                resultadoValidacao += "O campo \"cor\" precisa ser informado" + "\n";

            if (dias == 0)
                resultadoValidacao += "O campo \"dias\" não foi preenchido, periodo padrão de 7 dias" + "\n";

            return resultadoValidacao;
        }
    }
}
