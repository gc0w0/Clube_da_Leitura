using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.Compartilhado;

namespace Clube_da_Leitura.ModuloRevista
{
    public class RepositorioRevistaEmArquivo :RepositorioBaseEmArquivo<Revista>, IRepositorioRevista
    {
        public RepositorioRevistaEmArquivo(ClubeLeituraContextoDeDados contextoDeDados) : base(contextoDeDados)
        {
        }

        protected override List<Revista> ObterRegistros()
        {
            return contexto.Revistas; 
        }
    }
}
