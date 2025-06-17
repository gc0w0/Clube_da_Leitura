using System.Data;

namespace Clube_da_Leitura.Compartilhado;

public static class IDbCommandExtension
{
    public static void AddParametro(this IDbCommand cmd, string nome, object valor)
    {
        var parametro = cmd.CreateParameter();
        parametro.ParameterName = nome;
        parametro.Value = valor;

        cmd.Parameters.Add(parametro);        
    }
}
