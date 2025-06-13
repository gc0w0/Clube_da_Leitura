using Clube_da_Leitura.Compartilhado;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloMultas;
using Gestao_de_Equipamentos.Compartilhado;

namespace Clube_da_Leitura.ModuloAmigo
{
    public class RepositorioAmigoEmMemoria : RepositorioBase<Amigo>, IRepositorioAmigo
    {
        public List<Amigo> SelecionarPorFiltro(string letra)
        {
            //return SelecionarTodos().Where(a => a.nome.StartsWith(letra, StringComparison.OrdinalIgnoreCase)).ToList();
            return SelecionarTodos().Where(a => a.nome.StartsWith(letra, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public List<Amigo> SelecionarPorFiltro2(Predicate <Amigo> condicao)
        {
            return SelecionarTodos().FindAll(condicao).ToList();
        }

       
    }


}
