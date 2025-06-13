using Clube_da_Leitura.ModuloAmigo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloEmprestimo
{
    public interface IRepositorioEmprestimo
    {
        void InserirRegistro(Emprestimo registro);
        bool EditarRegistro(int id, Emprestimo registroAtualizado);
        Emprestimo SelecionarPorId(int id);
        bool ExcluirRegistro(int id);
        List<Emprestimo> SelecionarTodos();
        bool Validacoes(Func<Emprestimo, bool> validacao);
        List<Emprestimo> SelecionarTodosAbertos();
        List<Emprestimo> SelecionarTodosFechados();
    }
}
