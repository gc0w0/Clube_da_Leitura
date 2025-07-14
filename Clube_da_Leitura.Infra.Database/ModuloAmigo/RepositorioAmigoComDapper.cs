using Clube_da_Leitura.Dominio.ModuloMultas;
using Clube_da_Leitura.Infra.Database.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloMultas;
using Dapper;
using Microsoft.Win32;
using System.Data;

namespace Clube_da_Leitura.Infra.Database.ModuloAmigo;
public class RepositorioAmigoComDapper : IRepositorioAmigo, IDisposable
{
    private IDbConnection dbConnection;

    public RepositorioAmigoComDapper(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    protected  string SqlInserir => @"INSERT INTO TBAmigos (Nome, NomeResponsavel, Telefone)
               VALUES (@Nome, @NomeResponsavel, @Telefone)";

    protected  string SqlEditar => @"UPDATE TBAmigos SET Nome = @Nome, NomeResponsavel = @NomeResponsavel, Telefone = @Telefone
                           WHERE Id = @Id";

    protected  string SqlSelecionarPorId => @"SELECT Id,Nome, NomeResponsavel, Telefone FROM TBAmigos WHERE Id = @Id";

    protected  string SqlExcluir => @"DELETE FROM TBAmigos WHERE Id = @Id";

    protected  string SqlSelecionarTodos => @"SELECT 
    A.Id,
    A.Nome,
    A.NomeResponsavel,
    A.Telefone,
    COUNT(E.Id) AS QuantidadeEmprestimos
    FROM TBAmigos A
    LEFT JOIN TBEmprestimos E ON E.AmigoId = A.Id
    GROUP BY A.Id, A.Nome, A.NomeResponsavel, A.Telefone";

    public void InserirRegistro(Amigo registro)
    {
        this.dbConnection.Execute(SqlInserir, registro );
    }

    public bool EditarRegistro(int id, Amigo registroAtualizado)
    {
        registroAtualizado.Id = id;
       return this.dbConnection.Execute(SqlEditar, registroAtualizado) > 0;
    }

    public Amigo SelecionarPorId(int id)
    {
        return this.dbConnection.QueryFirst<Amigo>(SqlSelecionarPorId, new { Id = id });
    }

    public bool ExcluirRegistro(int id)
    {
        return this.dbConnection.Execute(SqlExcluir, new {Id = id}) > 0;
    }

    public List<Amigo> SelecionarTodos()
    {
        return this.dbConnection.Query<Amigo>(SqlSelecionarTodos).ToList();
    }

    public bool Validacoes(Func<Amigo, bool> validacao)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        
    }
}
