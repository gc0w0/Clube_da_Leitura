using Clube_da_Leitura.Infra.Database.Compartilhado;
using Clube_da_Leitura.ModuloMultas;
using System.Data;

namespace Clube_da_Leitura.Infra.Database.ModuloMultas
{
    public class RepositorioMultaEmBancoDeDados : RepositorioBaseEmBancoDeDados<Multa>
    {
        private IDbConnection dbConnection;

        protected override string SqlInserir =>
            @"INSERT INTO TBMultas (EmprestimoId, Valor, Situacao) 
          VALUES (@EmprestimoId, @Valor, @Situacao)";

        protected override string SqlEditar => throw new NotImplementedException();
        protected override string SqlExcluir => @"DELETE FROM TBMultas WHERE Id = @Id";
        protected override string SqlSelecionarPorId => throw new NotImplementedException();
        protected override string SqlSelecionarTodos => throw new NotImplementedException();

        public RepositorioMultaEmBancoDeDados(IDbConnection dbConnection) : base(dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        protected override Dictionary<string, object> ObterParametros(Multa multa)
        {
            return new Dictionary<string, object>
        {
            { "@EmprestimoId", multa.emprestimo.id },
            { "@Valor", multa.valorMulta },
            { "@Situacao", (int)multa.situacao }
        };
        }

        protected override Multa ConverterRegistro(IDataReader reader)
        {
            throw new NotImplementedException(); // Só se você for usar SELECT depois
        }
    }

}
