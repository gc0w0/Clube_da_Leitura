using Clube_da_Leitura.ModuloCaixa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloReservas
{
    public class RepositorioReservaEmArquivo :RepositorioBase<Reserva>, IRepositorioReserva
    {
        public RepositorioReservaEmArquivo() : base(@"C:\temp\reservas.json")
        {
        }
        public List<Reserva> SelecionarTodosAbertos()
        {
            return SelecionarTodos()
               .Where(r => r.situacao == SituacaoReserva.Ativa)
               .ToList();
        }

    }
}
