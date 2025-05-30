using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloEmprestimo;

namespace Clube_da_Leitura.ModuloRevista;
public class Revista : EntidadeBase<Revista>
{
    public string titulo; //minimo de 2 caracteres com no maximo 100
    public int numeroEdicao;
    public int anoPublicacao; 
    public string status;
    public Caixa caixa;
    public List<Emprestimo> emprestimos = new List<Emprestimo>(); // em haver se precisa ou não

    public Revista(string titulo, int numeroEdicao, int anoPublicacao, Caixa caixa, string status)
    { 
        this.titulo = titulo;
        this.numeroEdicao = numeroEdicao;
        this.anoPublicacao = anoPublicacao;
        this.caixa = caixa;
        this.status = status;
        caixa.revistas.Add(this);
    }

    public override void AtualizarInformacoes(Revista revistaAtualizada)
    {
        this.titulo = revistaAtualizada.titulo;
        this.numeroEdicao = revistaAtualizada.numeroEdicao;
        this.anoPublicacao = revistaAtualizada.anoPublicacao;
        this.status = revistaAtualizada.status;
    }

    public override void MostrarInformacoes()
    {
        Console.WriteLine($"ID de Registro: {id} | Titulo: {titulo} | Ano da Publicação: {anoPublicacao} | Status: {status} | Caixa: {caixa.etiqueta}");
    }

    public override string Validar()
    {
        string resultadoValidacao = "";

        if (string.IsNullOrEmpty(titulo))
            resultadoValidacao += "O campo \"titulo\" é obrigatório" + "\n";

        if (titulo.Length < 2)
            resultadoValidacao += "O campo \"titulo\" precisa ter no mínimo 3 letras" + "\n";

        if (numeroEdicao < 0)
            resultadoValidacao += "O campo \"Numero da Edição\" não pode ser negativo" + "\n";

        if (anoPublicacao == 0)
            resultadoValidacao += "O campo \"Ano de Publicação\" é obrigatório" + "\n";

        return resultadoValidacao;
    }
}
