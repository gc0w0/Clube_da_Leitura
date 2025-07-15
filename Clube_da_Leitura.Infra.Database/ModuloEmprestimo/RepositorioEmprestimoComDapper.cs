using Clube_da_Leitura.Dominio.ModuloEmprestimo;
using Clube_da_Leitura.Infra.Database.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloRevista;
using Dapper;
using System.Data;

namespace Clube_da_Leitura.Infra.Database.ModuloEmprestimo;

public class RepositorioEmprestimoComDapper : IRepositorioEmprestimo, IDisposable
{
    private IDbConnection dbConnection;
    public RepositorioEmprestimoComDapper(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    protected  string SqlInserir =>
        @"INSERT INTO TBEmprestimos 
              (AmigoId, RevistaId, DataEmprestimo, DataDevolucao, DataPrevistaDevolucao, Situacao) 
              VALUES 
              (@AmigoId, @RevistaId, @DataEmprestimo, @DataDevolucao, @DataPrevistaDevolucao, @Situacao)";

    protected  string SqlEditar =>
        @"UPDATE TBEmprestimos SET 
              AmigoId = @AmigoId,
              RevistaId = @RevistaId,
              DataEmprestimo = @DataEmprestimo,
              DataDevolucao = @DataDevolucao,
              DataPrevistaDevolucao = @DataPrevistaDevolucao,
              Situacao = @Situacao
              WHERE Id = @Id";

    protected  string SqlSelecionarPorId =>
       @"
        SELECT 
            E.Id, E.DataEmprestimo, E.DataDevolucao, E.DataPrevistaDevolucao, E.Situacao,
            A.Id AS AmigoId, A.Nome AS Nome, A.NomeResponsavel, A.Telefone,
            R.Id AS RevistaId, R.Titulo, R.NumeroEdicao, R.AnoPublicacao, R.Status
        FROM TBEmprestimos E
        JOIN TBAmigos A ON A.Id = E.AmigoId
        JOIN TBRevistas R ON R.Id = E.RevistaId
        WHERE E.Id = @Id";

    protected  string SqlExcluir =>
        @"DELETE FROM TBEmprestimos WHERE Id = @Id";

    protected  string SqlSelecionarTodos =>
        @"SELECT 
            E.Id, E.DataEmprestimo, E.DataDevolucao, E.DataPrevistaDevolucao, E.Situacao,
            A.Id AS AmigoId, A.Nome AS Nome, A.NomeResponsavel, A.Telefone,
            R.Id AS RevistaId, R.Titulo, R.NumeroEdicao, R.AnoPublicacao, R.Status
            FROM TBEmprestimos E
            JOIN TBAmigos A ON A.Id = E.AmigoId
            JOIN TBRevistas R ON R.Id = E.RevistaId";


    #region Obter Parametros
    //protected  Dictionary<string, object> ObterParametros(Emprestimo emprestimo)
    //{
    //    return new Dictionary<string, object>
    //    {
    //        { "@AmigoId", emprestimo.Amigo.Id },
    //        { "@RevistaId", emprestimo.Revista.Id },
    //        { "@DataEmprestimo", emprestimo.DataEmprestimo },
    //        { "@DataDevolucao", emprestimo.DataDevolucao.HasValue ? emprestimo.DataDevolucao : DBNull.Value },
    //        { "@DataPrevistaDevolucao", emprestimo.DataPrevistaDevolucao.HasValue ? emprestimo.DataPrevistaDevolucao : DBNull.Value },
    //        { "@Situacao", (int)emprestimo.Situacao }
    //    };
    //}
    #endregion
    #region Converter Registro
    //protected  Emprestimo ConverterRegistro(IDataReader reader)
    //{
    //    return new Emprestimo
    //    {
    //        Id = ConvertToInt(reader["Id"]),
    //        DataEmprestimo = Convert.ToDateTime(reader["DataEmprestimo"]),
    //        DataDevolucao = reader["DataDevolucao"] == DBNull.Value ? null : Convert.ToDateTime(reader["DataDevolucao"]),
    //        DataPrevistaDevolucao = reader["DataPrevistaDevolucao"] == DBNull.Value ? null : Convert.ToDateTime(reader["DataPrevistaDevolucao"]),
    //        Situacao = (SituacaoEmprestimo)ConvertToInt(reader["Situacao"]),

    //        Amigo = new Amigo
    //        {
    //            Id = ConvertToInt(reader["AmigoId"]),
    //            Nome = HasColumn(reader, "NomeAmigo") ? (string)reader["NomeAmigo"] : null,
    //            NomeResponsavel = HasColumn(reader, "NomeResponsavel") ? (string)reader["NomeResponsavel"] : null,
    //            Telefone = HasColumn(reader, "Telefone") ? (string)reader["Telefone"] : null
    //        },

    //        Revista = new Revista
    //        {
    //            Id = ConvertToInt(reader["RevistaId"]),
    //            Titulo = HasColumn(reader, "TituloRevista") ? (string)reader["TituloRevista"] : null,
    //            NumeroEdicao = HasColumn(reader, "NumeroEdicao") ? ConvertToInt(reader["NumeroEdicao"]) : 0,
    //            AnoPublicacao = HasColumn(reader, "AnoPublicacao") ? ConvertToInt(reader["AnoPublicacao"]) : 0,
    //            Status = HasColumn(reader, "Status") ? (Revista.StatusDisponveis)ConvertToInt(reader["Status"]) : 0
    //        }
    //    };
    //}
    #endregion
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

    public void InserirRegistro(Emprestimo registro)
    {
        dbConnection.Execute(SqlInserir, new
        {
            AmigoId = registro.Amigo.Id,
            RevistaId = registro.Revista.Id,
            registro.DataEmprestimo,
            registro.DataDevolucao,
            registro.DataPrevistaDevolucao,
            registro.Situacao
        });
    }

    public bool EditarRegistro(int id, Emprestimo registroAtualizado)
    {
        registroAtualizado.Id = id;
        return dbConnection.Execute(SqlEditar, new
        {
            AmigoId = registroAtualizado.Amigo.Id,
            RevistaId = registroAtualizado.Revista.Id,
            registroAtualizado.DataEmprestimo,
            registroAtualizado.DataDevolucao,
            registroAtualizado.DataPrevistaDevolucao,
            registroAtualizado.Situacao,
            Id = id
        }) > 0;
    }

    public Emprestimo SelecionarPorId(int id)
    {
        return dbConnection.Query<Emprestimo, Amigo, Revista, Emprestimo>(
        SqlSelecionarPorId,
        (e, a, r) =>
        {
            e.Amigo = a;
            e.Revista = r;
            return e;
        },
        new { Id = id },
        splitOn: "AmigoId,RevistaId"
    ).FirstOrDefault();
    }

    public bool ExcluirRegistro(int id)
    {
        return this.dbConnection.Execute(SqlExcluir, new { Id = id }) > 0;

    }
    public List<Emprestimo> SelecionarTodos()
    {
        return dbConnection.Query<Emprestimo, Amigo, Revista, Emprestimo>(
            SqlSelecionarTodos,
            (e, a, r) =>
            {
                e.Amigo = a;
                e.Revista = r;
                return e;
            },
            splitOn: "AmigoId,RevistaId"
        ).ToList();
    }

    public void Dispose()
    {

    }
}
