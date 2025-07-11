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
            { "@AmigoId", emprestimo.Amigo.Id },
            { "@RevistaId", emprestimo.Revista.Id },
            { "@DataEmprestimo", emprestimo.DataEmprestimo },
            { "@DataDevolucao", emprestimo.DataDevolucao.HasValue ? emprestimo.DataDevolucao : DBNull.Value },
            { "@DataPrevistaDevolucao", emprestimo.DataPrevistaDevolucao.HasValue ? emprestimo.DataPrevistaDevolucao : DBNull.Value },
            { "@Situacao", (int)emprestimo.Situacao }
        };
    }

    protected override Emprestimo ConverterRegistro(IDataReader reader)
    {
        return new Emprestimo
        {
            Id = ConvertToInt(reader["Id"]),
            DataEmprestimo = Convert.ToDateTime(reader["DataEmprestimo"]),
            DataDevolucao = reader["DataDevolucao"] == DBNull.Value ? null : Convert.ToDateTime(reader["DataDevolucao"]),
            DataPrevistaDevolucao = reader["DataPrevistaDevolucao"] == DBNull.Value ? null : Convert.ToDateTime(reader["DataPrevistaDevolucao"]),
            Situacao = (SituacaoEmprestimo)ConvertToInt(reader["Situacao"]),

            Amigo = new Amigo
            {
                Id = ConvertToInt(reader["AmigoId"]),
                Nome = HasColumn(reader, "NomeAmigo") ? (string)reader["NomeAmigo"] : null,
                NomeResponsavel = HasColumn(reader, "NomeResponsavel") ? (string)reader["NomeResponsavel"] : null,
                Telefone = HasColumn(reader, "Telefone") ? (string)reader["Telefone"] : null
            },

            Revista = new Revista
            {
                Id = ConvertToInt(reader["RevistaId"]),
                Titulo = HasColumn(reader, "TituloRevista") ? (string)reader["TituloRevista"] : null,
                NumeroEdicao = HasColumn(reader, "NumeroEdicao") ? ConvertToInt(reader["NumeroEdicao"]) : 0,
                AnoPublicacao = HasColumn(reader, "AnoPublicacao") ? ConvertToInt(reader["AnoPublicacao"]) : 0,
                Status = HasColumn(reader, "Status") ? (Revista.StatusDisponveis)ConvertToInt(reader["Status"]) : 0
            }
        };
    }

    public List<Emprestimo> SelecionarTodosAbertos()
    {
        return SelecionarTodos().Where(e => e.Situacao == SituacaoEmprestimo.Aberto).ToList();
    }

    public List<Emprestimo> SelecionarTodosFechados()
    {
        return SelecionarTodos().Where(e => e.Situacao == SituacaoEmprestimo.Fechado).ToList();
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
