namespace Clube_da_Leitura.ModuloRevista
{
    public class RepositorioRevistaEmBancoDeDados : IRepositorioRevista
    {
        public bool EditarRegistro(int id, Revista registroAtualizado)
        {
            throw new NotImplementedException();
        }

        public bool ExcluirRegistro(int id)
        {
            throw new NotImplementedException();
        }

        public void InserirRegistro(Revista registro)
        {
            throw new NotImplementedException();
        }

        public Revista SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Revista> SelecionarTodos()
        {
            throw new NotImplementedException();
        }

        public bool Validacoes(Func<Revista, bool> validacao)
        {
            throw new NotImplementedException();
        }
    }
}
