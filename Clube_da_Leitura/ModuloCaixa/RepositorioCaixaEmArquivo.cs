using Clube_da_Leitura.ModuloAmigo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloCaixa
{
    public class RepositorioCaixaEmArquivo : RepositorioBase<Caixa>, IRepositorioCaixa
    {
        public RepositorioCaixaEmArquivo() : base(@"C:\temp\caixas.json")
        {
        }

    }
}
