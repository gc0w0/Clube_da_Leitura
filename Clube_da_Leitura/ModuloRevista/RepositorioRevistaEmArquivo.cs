using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Clube_da_Leitura.ModuloCaixa;

namespace Clube_da_Leitura.ModuloRevista
{
    public class RepositorioRevistaEmArquivo :RepositorioBase<Revista>, IRepositorioRevista
    {
        public RepositorioRevistaEmArquivo() : base(@"C:\temp\revistas.json")
        {
        }
    }
}
