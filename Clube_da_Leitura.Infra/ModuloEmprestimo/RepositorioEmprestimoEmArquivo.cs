using Clube_da_Leitura.Dominio.ModuloEmprestimo;
using Clube_da_Leitura.Infra.Arquivos.Compartilhado;

namespace Clube_da_Leitura.ModuloEmprestimo;

public class RepositorioEmprestimoEmArquivo : RepositorioBaseEmArquivo<Emprestimo>, IRepositorioEmprestimo
{
    public RepositorioEmprestimoEmArquivo(ClubeLeituraContextoDeDados contextoDeDados) : base(contextoDeDados)
    {
    }
    public List<Emprestimo> SelecionarTodosAbertos()
    {
        return SelecionarTodos().Where(e => e.Situacao == SituacaoEmprestimo.Aberto).ToList();
    }

    public List<Emprestimo> SelecionarTodosFechados()
    {
        return SelecionarTodos().Where(e => e.Situacao == SituacaoEmprestimo.Fechado).ToList();
    }

    protected override List<Emprestimo> ObterRegistros()
    {
        return contexto.Emprestimos;
    }
}
