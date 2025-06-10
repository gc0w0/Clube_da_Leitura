using Microsoft.Data.SqlClient;

namespace Clube_da_Leitura.ModuloAmigo;

public class RepositorioAmigoEmBancoDados : IRepositorioAmigo
{
    public void InserirRegistro(Amigo registro)
    {
        SqlConnection sqlConnection = new SqlConnection();

        sqlConnection.Open();

        sqlConnection.CreateCommand().ExecuteNonQuery(); 
        
        sqlConnection.Close();        

    }

    public bool EditarRegistro(int id, Amigo registroAtualizado)
    {
        throw new NotImplementedException();
    }

    public bool ExcluirRegistro(int id)
    {
        throw new NotImplementedException();
    }

    public Amigo SelecionarPorId(int id)
    {
        throw new NotImplementedException();
    }

    public List<Amigo> SelecionarTodos()
    {
        throw new NotImplementedException();
    }

    public bool Validacoes(Func<Amigo, bool> validacao)
    {
        throw new NotImplementedException();
    }
}
