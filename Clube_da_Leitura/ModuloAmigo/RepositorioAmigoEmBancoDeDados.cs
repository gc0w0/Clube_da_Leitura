using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Clube_da_Leitura.Compartilhado;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;
namespace Clube_da_Leitura.ModuloAmigo;
public class RepositorioAmigoEmBancoDeDados : RepositorioBaseEmBancoDeDados<Amigo> ,IRepositorioAmigo
{
    private IDbConnection dbConnection;
      
    protected override string SqlInserir => @"INSERT INTO TBAmigos (Nome, NomeResponsavel, Telefone)
               VALUES (@Nome, @Responsavel, @Telefone)";

    protected override string SqlEditar => @"UPDATE TBAmigos SET Nome = @Nome, NomeResponsavel = @Responsavel, Telefone = @Telefone
                           WHERE Id = @Id";

    protected override string SqlSelecionarPorId => @"SELECT * FROM TBAmigos WHERE Id = @Id";

    protected override string SqlExcluir => @"DELETE FROM TBAmigos WHERE Id = @Id";

    protected override string SqlSelecionarTodos => "SELECT * FROM TBAmigos";

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
            id = (int)reader["Id"],
            nome = (string)reader["Nome"],
            nomeReponsavel = (string)reader["NomeResponsavel"],
            telefone = (string)reader["Telefone"]
        };
    }

    public List<Amigo> SelecionarPorFiltro2(Predicate<Amigo> condicao)
    {
        return SelecionarTodos().FindAll(condicao);
    }

    public bool Validacoes(Func<Amigo, bool> validacao)
    {
        return SelecionarTodos().Any(validacao);
    }
    
}
