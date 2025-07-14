using Clube_da_Leitura.Infra.Database.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using Dapper;
using System.Data;

namespace Clube_da_Leitura.Infra.Database.ModuloCaixa;

public class RepositorioCaixaComDapper : IRepositorioCaixa, IDisposable
{
    private IDbConnection dbConnection;

    public RepositorioCaixaComDapper(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    protected  string SqlInserir =>
        @"INSERT INTO TBCaixas (Etiqueta, Cor, Dias) VALUES (@Etiqueta, @Cor, @Dias)";

    protected  string SqlEditar =>
        @"UPDATE TBCaixas SET Etiqueta = @Etiqueta, Cor = @Cor, Dias = @Dias WHERE Id = @Id";

    protected  string SqlSelecionarPorId =>
        @"SELECT Etiqueta, Cor, Dias FROM TBCaixas WHERE Id = @Id";

    protected  string SqlExcluir =>
        @"DELETE FROM TBCaixas WHERE Id = @Id";

    protected  string SqlSelecionarTodos =>
        @"SELECT Etiqueta, Cor, Dias FROM TBCaixas";


    public void InserirRegistro(Caixa registro)
    {
        this.dbConnection.Execute(SqlInserir, registro);
    }

    public bool EditarRegistro(int id, Caixa registroAtualizado)
    {
        registroAtualizado.Id = id;
        return this.dbConnection.Execute(SqlEditar, registroAtualizado) > 0;
    }

    public Caixa SelecionarPorId(int id)
    {
        return this.dbConnection.QueryFirst<Caixa>(SqlSelecionarPorId, new { Id = id });
    }

    public bool ExcluirRegistro(int id)
    {
        return this.dbConnection.Execute(SqlExcluir, new { Id = id }) > 0;
    }

    public List<Caixa> SelecionarTodos()
    {
        return this.dbConnection.Query<Caixa>(SqlSelecionarTodos).ToList();
    }
    public void Dispose()
    {

    }
    public bool Validacoes(Func<Caixa, bool> validacao)
    {
        return SelecionarTodos().Any(validacao);
    }
}
