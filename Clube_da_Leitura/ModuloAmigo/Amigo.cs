using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloMultas;
using Clube_da_Leitura.ModuloReservas;

namespace Clube_da_Leitura.ModuloAmigo
{
    public class Amigo : EntidadeBase<Amigo>
    {
        public string nome, nomeReponsavel, telefone; //minimo 3 caracteres no maximo 100
        //minimo 3 caracteres no maximo 100
        //implementar Validação de caracteres (formato validado: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX)
        public List<Emprestimo> emprestimos = new List<Emprestimo>();
        //public RepositorioAmigo repositorioAmigo;
        public List<Multa> multas = new List<Multa>();
        public Multa infoMulta;
        public List<Reserva> reserva = new List<Reserva>();
        //public List<Revista> revista = new List<Revista>();

        public Amigo()
        {
            
        }

        public Amigo(string nome, string nomeResponsavel, string telefone)
        {
            this.nome = nome;
            this.nomeReponsavel = nomeResponsavel;
            this.telefone = telefone;
        }

        public override void AtualizarInformacoes(Amigo amigoAtualizado)
        {
            this.nome = amigoAtualizado.nome;
            this.nomeReponsavel = amigoAtualizado.nomeReponsavel;
            this.telefone = amigoAtualizado.telefone;
            this.emprestimos = amigoAtualizado.emprestimos;
            this.infoMulta = amigoAtualizado.infoMulta;
        }

        public override void MostrarInformacoes()
        {
            Console.WriteLine($"ID de Registro: {id} | Nome: {nome} | Nome Responsavel {nomeReponsavel} | Telefone: {telefone} | Emprestimos: {emprestimos.Count} | Multas: {multas.Count}");
        }

        public override string Validar()
        {
            string resultadoValidacao = "";

            if (string.IsNullOrEmpty(nome))
                resultadoValidacao += "O campo \"nome\" é obrigatório" + "\n";

            if (nome.Length < 3)
                resultadoValidacao += "O campo \"nome\" precisa ter no mínimo 3 letras" + "\n";

            if (nome.Length > 100)
                resultadoValidacao += "O campo \"nome\" pode ter no maximo 100 letras" + "\n";

            if (string.IsNullOrEmpty(nomeReponsavel))
                resultadoValidacao += "O campo \"Nome de Responsavel\" é obrigatório" + "\n";

            if (nomeReponsavel.Length < 3)
                resultadoValidacao += "O campo \"Nome de Responsavel\" precisa ter no mínimo 3 letras" + "\n";

            if (nomeReponsavel.Length > 100)
                resultadoValidacao += "O campo \"Nome de Responsavel\" pode ter no maximo 100 letras" + "\n";

            if (telefone.Length < 10)
                resultadoValidacao += "O campo \"Telefone\" precisa ter no mínimo 10 caracteres" + "\n";

            if (telefone.Length > 11)
                resultadoValidacao += "O campo \"Telefone\" pode ter no maximo 11 caracteres" + "\n";

            return resultadoValidacao;
        }

        public void RegistrarMulta(Multa novaMulta)
        {
            multas.Add(novaMulta);
        }
    }
}
