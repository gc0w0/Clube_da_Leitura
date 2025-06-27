using Clube_da_Leitura.Infra.Arquivos.Compartilhado;

namespace Clube_da_Leitura.ModuloCaixa;

public class RepositorioCaixaEmArquivo : RepositorioBaseEmArquivo<Caixa>, IRepositorioCaixa
{
    public RepositorioCaixaEmArquivo(ClubeLeituraContextoDeDados contextoDeDados) : base(contextoDeDados)
    {
    }

    protected override List<Caixa> ObterRegistros()
    {
        return contexto.Caixas;
    }
}
