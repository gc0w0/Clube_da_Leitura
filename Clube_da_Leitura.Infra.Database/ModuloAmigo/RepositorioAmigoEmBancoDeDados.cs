using Clube_da_Leitura.Dominio.ModuloMultas;
using Clube_da_Leitura.Infra.Database.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloMultas;
using System.Data;

namespace Clube_da_Leitura.Infra.Database.ModuloAmigo;
public class RepositorioAmigoEmBancoDeDados : RepositorioBaseEmBancoDeDados<Amigo>, IRepositorioAmigo, IDisposable
{
    private IDbConnection dbConnection;

    protected override string SqlInserir => @"INSERT INTO TBAmigos (Nome, NomeResponsavel, Telefone)
               VALUES (@Nome, @Responsavel, @Telefone)";

    protected override string SqlEditar => @"UPDATE TBAmigos SET Nome = @Nome, NomeResponsavel = @Responsavel, Telefone = @Telefone
                           WHERE Id = @Id";

    protected override string SqlSelecionarPorId => @"SELECT * FROM TBAmigos WHERE Id = @Id";

    protected override string SqlExcluir => @"DELETE FROM TBAmigos WHERE Id = @Id";

    protected override string SqlSelecionarTodos => @"SELECT 
    A.Id,
    A.Nome,
    A.NomeResponsavel,
    A.Telefone,
    COUNT(E.Id) AS QuantidadeEmprestimos
    FROM TBAmigos A
    LEFT JOIN TBEmprestimos E ON E.AmigoId = A.Id
    GROUP BY A.Id, A.Nome, A.NomeResponsavel, A.Telefone";

    public RepositorioAmigoEmBancoDeDados(IDbConnection dbConnection) : base(dbConnection)
    {
        this.dbConnection = dbConnection;

    }

    protected override Dictionary<string, object> ObterParametros(Amigo amigo)
    {
        return new Dictionary<string, object>
        {
            {"@Nome", amigo.nome},
            {"@Responsavel", amigo.nomeReponsavel},
            {"@Telefone", amigo.telefone}
        };
    }

    protected override Amigo ConverterRegistro(IDataReader reader)
    {
        return new Amigo
        {
            id = ConvertToInt(reader["Id"]),
            nome = (string)reader["Nome"],
            nomeReponsavel = (string)reader["NomeResponsavel"],
            telefone = (string)reader["Telefone"],
            emprestimos = Enumerable.Repeat(new Emprestimo(), SafeInt(reader, "QuantidadeEmprestimos")).ToList()
        };
    }
    public override List<Amigo> SelecionarTodos()
    {
        var amigos = base.SelecionarTodos();

        foreach (var amigo in amigos)
            amigo.multas = SelecionarMultasDoAmigo(amigo.id);

        return amigos;
    }

    private List<Multa> SelecionarMultasDoAmigo(int idAmigo)
    {
        var multas = new List<Multa>();

        using (var conexao = dbConnection)
        {
            conexao.Open();

            using var comando = conexao.CreateCommand();
            comando.CommandText = @"
            SELECT M.Id, M.Valor, M.Situacao
            FROM TBMultas M
            JOIN TBEmprestimos E ON M.EmprestimoId = E.Id
            WHERE E.AmigoId = @id AND M.Situacao = 1";

            comando.AddParametro("@id", idAmigo);

            using var leitor = comando.ExecuteReader();
            while (leitor.Read())
            {
                var multa = new Multa
                {
                    id = Convert.ToInt32(leitor["Id"]),
                    valorMulta = Convert.ToSingle(leitor["Valor"]),
                    situacao = (SituacaoMulta)Convert.ToInt32(leitor["Situacao"])
                };

                multas.Add(multa);
            }
        }

        return multas;
    }

    public List<Amigo> SelecionarPorFiltro2(Predicate<Amigo> condicao)
    {
        return SelecionarTodos().FindAll(condicao);
    }

    public bool Validacoes(Func<Amigo, bool> validacao)
    {
        return SelecionarTodos().Any(validacao);
    }
    public void Dispose()
    {
        dbConnection.Dispose();
    }
}
