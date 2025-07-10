using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloReserva;

namespace Clube_da_Leitura.ModuloRevista;
public class Revista : EntidadeBase<Revista>
{
    public string Titulo {  get; set; }
    public int NumeroEdicao { get; set; }
    public int AnoPublicacao { get; set; }
    public StatusDisponveis Status { get; set; }
    public Caixa Caixa { get; set; }
    public List<Emprestimo> Emprestimos { get; set; } = new List<Emprestimo>(); 
    public List<Reserva> Reserva { get; set; } = new List<Reserva>();

    public Revista()
    {

    }
    public Revista(string titulo, int numeroEdicao, int anoPublicacao, Caixa caixa, StatusDisponveis status)
    {
        this.Titulo = titulo;
        this.NumeroEdicao = numeroEdicao;
        this.AnoPublicacao = anoPublicacao;
        this.Caixa = caixa;
        this.Status = status;
        caixa.Revistas.Add(this);
    }

    public enum StatusDisponveis
    {
        Disponivel = 1,
        Emprestada = 2,
        Reservada = 3,
    }

    public override void AtualizarInformacoes(Revista revistaAtualizada)
    {
        this.Titulo = revistaAtualizada.Titulo;
        this.NumeroEdicao = revistaAtualizada.NumeroEdicao;
        this.AnoPublicacao = revistaAtualizada.AnoPublicacao;
        this.Status = revistaAtualizada.Status;
    }

    public override void MostrarInformacoes()
    {
        Console.WriteLine($"ID de Registro: {Id} | Titulo: {Titulo} | Ano da Publicação: {AnoPublicacao} | Status: {Status} | Caixa: {Caixa.Etiqueta}");
    }

    public override string Validar()
    {
        string resultadoValidacao = "";

        if (string.IsNullOrEmpty(Titulo))
            resultadoValidacao += "O campo \"titulo\" é obrigatório" + "\n";

        if (Titulo.Length < 2)
            resultadoValidacao += "O campo \"titulo\" precisa ter no mínimo 3 letras" + "\n";

        if (NumeroEdicao < 0)
            resultadoValidacao += "O campo \"Numero da Edição\" não pode ser negativo" + "\n";

        if (AnoPublicacao == 0)
            resultadoValidacao += "O campo \"Ano de Publicação\" é obrigatório" + "\n";

        return resultadoValidacao;
    }

    public override string ToString()
    {
        return Titulo + " - " + Caixa + " - " + Status;
    }
}
