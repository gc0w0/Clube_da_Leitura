using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloRevista;

namespace Clube_da_Leitura.ModuloCaixa
{

    public class Caixa : EntidadeBase<Caixa>
    {
        public string Etiqueta { get; set; } 
        public CorCaixa Cor { get; set; }
        public int Dias { get; set; }
        public List<Revista> Revistas { get; set; } = new List<Revista>();
        public enum CorCaixa
        {
            Vermelha = 1,
            Azul = 2,
            Verde = 3,
            Amarela = 4,
            Roxa = 5,
            Laranja = 6
        }

        public Caixa()
        {

        }
        public Caixa(string etiqueta, CorCaixa cor, int dias)
        {
            this.Etiqueta = etiqueta;
            this.Cor = cor;
            this.Dias = dias;
        }

        public override void AtualizarInformacoes(Caixa caixaAtualizado)
        {
            this.Etiqueta = caixaAtualizado.Etiqueta;
            this.Cor = caixaAtualizado.Cor;
            this.Dias = caixaAtualizado.Dias;
        }

        public override void MostrarInformacoes()
        {
            Console.WriteLine($"ID de registro: {Id} | Etiqueta: {Etiqueta} | Cor: {Cor} | Dias de Emprestimo: {Dias}");
        }

        public override string Validar()
        {
            string resultadoValidacao = "";

            if (string.IsNullOrEmpty(Etiqueta))
                resultadoValidacao += "O campo \"etiqueta\" é obrigatório" + "\n";

            if (Etiqueta.Length > 50)
                resultadoValidacao += "O campo \"etiqueta\" precisa ter no mínimo 3 letras" + "\n";

            if (Cor < 0)
                resultadoValidacao += "O campo \"cor\" precisa ser informado" + "\n";

            if (Dias == 0)
            {
                resultadoValidacao += "O campo \"dias\" não foi preenchido, periodo padrão de 7 dias" + "\n";
                Dias = 7; // testar
            }

            return resultadoValidacao;
        }
    }
}
