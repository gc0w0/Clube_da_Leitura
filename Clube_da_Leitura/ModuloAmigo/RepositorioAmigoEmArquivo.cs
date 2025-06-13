namespace Clube_da_Leitura.ModuloAmigo
{
    public class RepositorioAmigoEmArquivo : RepositorioBase<Amigo>, IRepositorioAmigo
    {
        public RepositorioAmigoEmArquivo(): base(@"C:\temp\amigos.json")
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
    }
}
