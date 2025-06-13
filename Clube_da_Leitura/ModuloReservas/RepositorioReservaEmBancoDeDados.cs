using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloReservas
{
    public class RepositorioReservaEmBancoDeDados : IRepositorioReserva
    {
        public void InserirRegistro(Reserva registro)
        {
            throw new NotImplementedException();
        }
        public bool EditarRegistro(int id, Reserva registroAtualizado)
        {
            throw new NotImplementedException();
        }

        public bool ExcluirRegistro(int id)
        {
            throw new NotImplementedException();
        }


        public Reserva SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Reserva> SelecionarTodos()
        {
            throw new NotImplementedException();
        }

        public List<Reserva> SelecionarTodosAbertos()
        {
            throw new NotImplementedException();
        }

        public bool Validacoes(Func<Reserva, bool> validacao)
        {
            throw new NotImplementedException();
        }
    }
}
