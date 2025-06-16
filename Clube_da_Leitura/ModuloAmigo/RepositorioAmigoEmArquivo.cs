using Clube_da_Leitura.Compartilhado;

namespace Clube_da_Leitura.ModuloAmigo
{
    public class RepositorioAmigoEmArquivo : RepositorioBaseEmArquivo<Amigo>, IRepositorioAmigo
    {

        public RepositorioAmigoEmArquivo(ClubeLeituraContextoDeDados contextoDeDados) :  base(contextoDeDados)
        {
        }

        public List<Amigo> SelecionarPorFiltro(string letra)
        {
            return SelecionarTodos()
                .Where(a => a.nome.StartsWith(letra, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Amigo> SelecionarPorFiltro2(Predicate<Amigo> condicao)
        {
            return SelecionarTodos()
                .FindAll(condicao)
                .ToList();
        }

        protected override List<Amigo> ObterRegistros()
        {
            return contexto.Amigos;
        }
    }
}
