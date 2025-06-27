using System.Data;

namespace Clube_da_Leitura.Infra.Database.Compartilhado
{
    public static class IDbCommandExtension
    {
        public static void AddParametro(this IDbCommand cmd, string nome, object valor)
        {
            var pNome = cmd.CreateParameter();
            pNome.ParameterName = nome;
            pNome.Value = valor;
            cmd.Parameters.Add(pNome);
        }
    }
}
