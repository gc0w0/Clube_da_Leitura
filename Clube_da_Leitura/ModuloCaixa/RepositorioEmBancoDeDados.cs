using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloCaixa
{
    public class RepositorioEmBancoDeDados : IRepositorioCaixa
    {
        public bool EditarRegistro(int id, Caixa registroAtualizado)
        {
            throw new NotImplementedException();
        }

        public bool ExcluirRegistro(int id)
        {
            throw new NotImplementedException();
        }

        public void InserirRegistro(Caixa registro)
        {
            throw new NotImplementedException();
        }

        public Caixa SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Caixa> SelecionarTodos()
        {
            throw new NotImplementedException();
        }

        public bool Validacoes(Func<Caixa, bool> validacao)
        {
            throw new NotImplementedException();
        }
    }
}
