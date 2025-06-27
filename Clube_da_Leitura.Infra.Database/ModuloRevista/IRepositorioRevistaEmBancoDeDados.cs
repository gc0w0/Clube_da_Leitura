
using Clube_da_Leitura.ModuloRevista;

namespace Clube_da_Leitura.Infra.Database.ModuloRevista
{
    public interface IRepositorioRevistaEmBancoDeDados
    {
        bool Validacoes(Func<Revista, bool> validacao);
    }
}