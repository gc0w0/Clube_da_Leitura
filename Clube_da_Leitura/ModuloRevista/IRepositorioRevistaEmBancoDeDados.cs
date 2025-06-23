
namespace Clube_da_Leitura.ModuloRevista
{
    public interface IRepositorioRevistaEmBancoDeDados
    {
        bool Validacoes(Func<Revista, bool> validacao);
    }
}