using Clube_da_Leitura.Dominio.ModuloEmprestimo;
using Clube_da_Leitura.Infra.Database.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloRevista;
using System.Data;

namespace Clube_da_Leitura.Infra.Database.ModuloEmprestimo;

public class RepositorioEmprestimoEmBancoDeDados : RepositorioBaseEmBancoDeDados<Emprestimo>, IRepositorioEmprestimo, IDisposable
{
    private IDbConnection dbConnection;

    protected override string SqlInserir =>
        @"INSERT INTO TBEmprestimos 
              (AmigoId, RevistaId, DataEmprestimo, DataDevolucao, DataPrevistaDevolucao, Situacao) 
              VALUES 
              (@AmigoId, @RevistaId, @DataEmprestimo, @DataDevolucao, @DataPrevistaDevolucao, @Situacao)";

    protected override string SqlEditar =>
        @"UPDATE TBEmprestimos SET 
              AmigoId = @AmigoId,
              RevistaId = @RevistaId,
              DataEmprestimo = @DataEmprestimo,
              DataDevolucao = @DataDevolucao,
              DataPrevistaDevolucao = @DataPrevistaDevolucao,
              Situacao = @Situacao
              WHERE Id = @Id";

    protected override string SqlSelecionarPorId =>
        @"SELECT * FROM TBEmprestimos WHERE Id = @Id";

    protected override string SqlExcluir =>
        @"DELETE FROM TBEmprestimos WHERE Id = @Id";

    protected override string SqlSelecionarTodos =>
        @"SELECT 
        E.Id,E.DataEmprestimo,E.DataDevolucao,E.DataPrevistaDevolucao,E.Situacao,A.Id AS AmigoId,A.Nome AS NomeAmigo,A.NomeResponsavel,
        A.Telefone,R.Id AS RevistaId,R.Titulo AS TituloRevista,R.NumeroEdicao,R.AnoPublicacao,R.Status
        FROM TBEmprestimos E
        JOIN TBAmigos A ON A.Id = E.AmigoId
        JOIN TBRevistas R ON R.Id = E.RevistaId
";

    public RepositorioEmprestimoEmBancoDeDados(IDbConnection dbConnection) : base(dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    protected override Dictionary<string, object> ObterParametros(Emprestimo emprestimo)
    {
        return new Dictionary<string, object>
        {
            { "@AmigoId", emprestimo.amigo.Id },
            { "@RevistaId", emprestimo.revista.Id },
            { "@DataEmprestimo", emprestimo.dataEmprestimo },
            { "@DataDevolucao", emprestimo.dataDevolucao.HasValue ? emprestimo.dataDevolucao : DBNull.Value },
            { "@DataPrevistaDevolucao", emprestimo.dataPrevistaDevolucao.HasValue ? emprestimo.dataPrevistaDevolucao : DBNull.Value },
            { "@Situacao", (int)emprestimo.situacao }
        };
    }

    protected override Emprestimo ConverterRegistro(IDataReader reader)
    {
        return new Emprestimo
        {
            Id = ConvertToInt(reader["Id"]),
            dataEmprestimo = Convert.ToDateTime(reader["DataEmprestimo"]),
            dataDevolucao = reader["DataDevolucao"] == DBNull.Value ? null : Convert.ToDateTime(reader["DataDevolucao"]),
            dataPrevistaDevolucao = reader["DataPrevistaDevolucao"] == DBNull.Value ? null : Convert.ToDateTime(reader["DataPrevistaDevolucao"]),
            situacao = (SituacaoEmprestimo)ConvertToInt(reader["Situacao"]),

            amigo = new Amigo
            {
                Id = ConvertToInt(reader["AmigoId"]),
                Nome = HasColumn(reader, "NomeAmigo") ? (string)reader["NomeAmigo"] : null,
                NomeReponsavel = HasColumn(reader, "NomeResponsavel") ? (string)reader["NomeResponsavel"] : null,
                Telefone = HasColumn(reader, "Telefone") ? (string)reader["Telefone"] : null
            },

            revista = new Revista
            {
                Id = ConvertToInt(reader["RevistaId"]),
                titulo = HasColumn(reader, "TituloRevista") ? (string)reader["TituloRevista"] : null,
                numeroEdicao = HasColumn(reader, "NumeroEdicao") ? ConvertToInt(reader["NumeroEdicao"]) : 0,
                anoPublicacao = HasColumn(reader, "AnoPublicacao") ? ConvertToInt(reader["AnoPublicacao"]) : 0,
                status = HasColumn(reader, "Status") ? (Revista.StatusDisponveis)ConvertToInt(reader["Status"]) : 0
            }
        };
    }

    public List<Emprestimo> SelecionarTodosAbertos()
    {
        return SelecionarTodos().Where(e => e.situacao == SituacaoEmprestimo.Aberto).ToList();
    }

    public List<Emprestimo> SelecionarTodosFechados()
    {
        return SelecionarTodos().Where(e => e.situacao == SituacaoEmprestimo.Fechado).ToList();
    }

    public bool Validacoes(Func<Emprestimo, bool> validacao)
    {
        return SelecionarTodos().Any(validacao);
    }

    public void Dispose()
    {
        dbConnection.Dispose();
    }
}
