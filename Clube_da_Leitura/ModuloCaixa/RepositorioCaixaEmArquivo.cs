using Clube_da_Leitura.Compartilhado;

namespace Clube_da_Leitura.ModuloCaixa
{
    public class RepositorioCaixaEmArquivo : RepositorioBaseEmArquivo<Caixa>, IRepositorioCaixa
    {
        public RepositorioCaixaEmArquivo(ClubeLeituraContextoDados contextoDados) : base(contextoDados)
        {
        }

        protected override List<Caixa> ObterRegistros()
        {
            return contexto.Caixas;
        }
    }
}
