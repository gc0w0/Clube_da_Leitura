using Clube_da_Leitura.ModuloAmigo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloRevista
{
    public interface IRepositorioRevista
    {
        void InserirRegistro(Revista registro);
        bool EditarRegistro(int id, Revista registroAtualizado);
        Revista SelecionarPorId(int id);
        bool ExcluirRegistro(int id);
        List<Revista> SelecionarTodos();
        bool Validacoes(Func<Revista, bool> validacao);
    }
}
