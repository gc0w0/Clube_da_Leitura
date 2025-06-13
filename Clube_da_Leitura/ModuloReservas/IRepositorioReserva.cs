using Clube_da_Leitura.ModuloAmigo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloReservas
{
    public interface IRepositorioReserva
    {
        void InserirRegistro(Reserva registro);
        bool EditarRegistro(int id, Reserva registroAtualizado);
        Reserva SelecionarPorId(int id);
        bool ExcluirRegistro(int id);
        List<Reserva> SelecionarTodos(); 
        bool Validacoes(Func<Reserva, bool> validacao);
        List<Reserva> SelecionarTodosAbertos();


    }
}
