using Clube_da_Leitura.Compartilhado;

namespace Clube_da_Leitura.ModuloRevista
{
    public class RepositorioRevistaEmArquivo : RepositorioBaseEmArquivo<Revista>, IRepositorioRevista
    {
        public RepositorioRevistaEmArquivo(ClubeLeituraContextoDados contextoDados) : base(contextoDados)
        {
        }

        protected override List<Revista> ObterRegistros()
        {
            throw new NotImplementedException();
        }
    }
}
