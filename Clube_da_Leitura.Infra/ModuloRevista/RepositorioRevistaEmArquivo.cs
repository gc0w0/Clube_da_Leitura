using Clube_da_Leitura.Infra.Arquivos.Compartilhado;
using Clube_da_Leitura.ModuloRevista;

namespace Clube_da_Leitura.Infra.Arquivos.ModuloRevista;

public class RepositorioRevistaEmArquivo : RepositorioBaseEmArquivo<Revista>, IRepositorioRevista
{
    public RepositorioRevistaEmArquivo(ClubeLeituraContextoDeDados contextoDeDados) : base(contextoDeDados)
    {
    }

    protected override List<Revista> ObterRegistros()
    {
        return contexto.Revistas;
    }
}
