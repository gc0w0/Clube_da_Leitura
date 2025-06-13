using Clube_da_Leitura.Compartilhado;

namespace Clube_da_Leitura.ModuloAmigo
{
    public interface IRepositorioAmigo : IRepositorio<Amigo>
    {
        public List<Amigo> SelecionarPorFiltro(string letra)
        {
            //return SelecionarTodos().Where(a => a.nome.StartsWith(letra, StringComparison.OrdinalIgnoreCase)).ToList();
            return SelecionarTodos().Where(a => a.nome.StartsWith(letra, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public List<Amigo> SelecionarPorFiltro2(Predicate<Amigo> condicao)
        {
            return SelecionarTodos().FindAll(condicao).ToList();
        }
    }
}
