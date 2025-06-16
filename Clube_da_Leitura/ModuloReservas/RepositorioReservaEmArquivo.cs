using Clube_da_Leitura.ModuloCaixa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Clube_da_Leitura.Compartilhado;

namespace Clube_da_Leitura.ModuloReservas
{
    public class RepositorioReservaEmArquivo : RepositorioBaseEmArquivo<Reserva>, IRepositorioReserva
    {
        public RepositorioReservaEmArquivo(ClubeLeituraContextoDeDados contextoDeDados) : base(contextoDeDados)
        {
        }
        public List<Reserva> SelecionarTodosAbertos()
        {
            return SelecionarTodos()
               .Where(r => r.situacao == SituacaoReserva.Ativa)
               .ToList();
        }

        protected override List<Reserva> ObterRegistros()
        {
            return contexto.Reservas;
        }
    }
}
