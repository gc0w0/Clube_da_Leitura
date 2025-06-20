using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.Compartilhado
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
    