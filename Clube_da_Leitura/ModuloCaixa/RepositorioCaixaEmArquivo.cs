using Clube_da_Leitura.Compartilhado;
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
    public class RepositorioCaixaEmArquivo : RepositorioBaseEmArquivo<Caixa>, IRepositorioCaixa
    {
        public RepositorioCaixaEmArquivo(ClubeLeituraContextoDeDados contextoDeDados) : base(contextoDeDados)
        {
        }

        protected override List<Caixa> ObterRegistros()
        {
            return contexto.Caixas;
        }
    }
}
