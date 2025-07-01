using Clube_da_Leitura.Infra.Arquivos.Compartilhado;

namespace Clube_da_Leitura.ModuloAmigo
{
    public class RepositorioAmigoEmArquivo : RepositorioBaseEmArquivo<Amigo>, IRepositorioAmigo
    {

        public RepositorioAmigoEmArquivo(ClubeLeituraContextoDeDados contextoDeDados) : base(contextoDeDados)
        {
        }

        public List<Amigo> SelecionarPorFiltro(string letra)
        {
            return SelecionarTodos()
                .Where(a => a.Nome.StartsWith(letra, StringComparison.OrdinalIgnoreCase))
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
