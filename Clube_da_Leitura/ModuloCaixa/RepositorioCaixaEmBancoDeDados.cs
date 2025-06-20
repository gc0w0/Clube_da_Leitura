using System;
using System.Collections.Generic;
using System.Data;
using Clube_da_Leitura.Compartilhado;

namespace Clube_da_Leitura.ModuloCaixa
{
    public class RepositorioCaixaEmBancoDeDados : RepositorioBaseEmBancoDeDados<Caixa>, IRepositorioCaixa
    {
        private IDbConnection dbConnection;

        protected override string SqlInserir =>
            @"INSERT INTO TBCaixas (Etiqueta, Cor, Dias) VALUES (@Etiqueta, @Cor, @Dias)";

        protected override string SqlEditar =>
            @"UPDATE TBCaixas SET Etiqueta = @Etiqueta, Cor = @Cor, Dias = @Dias WHERE Id = @Id";

        protected override string SqlSelecionarPorId =>
            @"SELECT * FROM TBCaixas WHERE Id = @Id";

        protected override string SqlExcluir =>
            @"DELETE FROM TBCaixas WHERE Id = @Id";

        protected override string SqlSelecionarTodos =>
            @"SELECT * FROM TBCaixas";

        public RepositorioCaixaEmBancoDeDados(IDbConnection dbConnection) : base(dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        protected override Dictionary<string, object> ObterParametros(Caixa caixa)
        {
            return new Dictionary<string, object>
            {
                {"@Etiqueta", caixa.etiqueta},
                {"@Cor", (int)caixa.cor},
                {"@Dias", caixa.dias}
            };
        }

        protected override Caixa ConverterRegistro(IDataReader reader)
        {
            return new Caixa
            {
                id = (int)reader["Id"],
                etiqueta = (string)reader["Etiqueta"],
                cor = (Caixa.CorCaixa)(int)reader["Cor"],
                dias = (int)reader["Dias"]
            };
        }

        public bool Validacoes(Func<Caixa, bool> validacao)
        {
            return SelecionarTodos().Any(validacao);
        }
    }
}
