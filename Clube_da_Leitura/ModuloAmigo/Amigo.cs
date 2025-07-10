using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloMultas;
using Clube_da_Leitura.ModuloReserva;

namespace Clube_da_Leitura.ModuloAmigo
{
    public class Amigo : EntidadeBase<Amigo>
    {
        public string Nome { get; set; }
        
        public string NomeReponsavel { get; set; }

        public string Telefone { get; set; } 

        public List<Emprestimo> emprestimos = new List<Emprestimo>();
        public List<Multa> Multas { get; set; } = new List<Multa>();
        public Multa infoMulta;
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();

        public Amigo()
        {

        }
        public Amigo(string nome, string nomeResponsavel, string telefone)
        {
            this.Nome = nome;
            this.NomeReponsavel = nomeResponsavel;
            this.Telefone = telefone;
        }

        public override void AtualizarInformacoes(Amigo amigoAtualizado)
        {
            this.Nome = amigoAtualizado.Nome;
            this.NomeReponsavel = amigoAtualizado.NomeReponsavel;
            this.Telefone = amigoAtualizado.Telefone;
            this.emprestimos = amigoAtualizado.emprestimos;
            this.infoMulta = amigoAtualizado.infoMulta;
        }

        public override void MostrarInformacoes()
        {
            Console.WriteLine($"ID de Registro: {Id} | Nome: {Nome} | Nome Responsavel {NomeReponsavel} | Telefone: {Telefone} | Emprestimos: {emprestimos.Count} | Multas: {Multas.Count}");
        }

        public override string Validar()
        {
            string resultadoValidacao = "";

            if (string.IsNullOrEmpty(Nome))
                resultadoValidacao += "O campo \"nome\" é obrigatório" + "\n";

            if (Nome.Length < 3)
                resultadoValidacao += "O campo \"nome\" precisa ter no mínimo 3 letras" + "\n";

            if (Nome.Length > 100)
                resultadoValidacao += "O campo \"nome\" pode ter no maximo 100 letras" + "\n";

            if (string.IsNullOrEmpty(NomeReponsavel))
                resultadoValidacao += "O campo \"Nome de Responsavel\" é obrigatório" + "\n";

            if (NomeReponsavel.Length < 3)
                resultadoValidacao += "O campo \"Nome de Responsavel\" precisa ter no mínimo 3 letras" + "\n";

            if (NomeReponsavel.Length > 100)
                resultadoValidacao += "O campo \"Nome de Responsavel\" pode ter no maximo 100 letras" + "\n";

            if (Telefone.Length < 10)
                resultadoValidacao += "O campo \"Telefone\" precisa ter no mínimo 10 caracteres" + "\n";

            if (Telefone.Length > 11)
                resultadoValidacao += "O campo \"Telefone\" pode ter no maximo 11 caracteres" + "\n";

            return resultadoValidacao;
        }

        public void RegistrarMulta(Multa novaMulta)
        {
            Multas.Add(novaMulta);
        }
    }
}
