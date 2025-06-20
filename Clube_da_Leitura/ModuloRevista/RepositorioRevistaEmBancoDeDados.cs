using System;
using System.Collections.Generic;
using System.Data;
using Clube_da_Leitura.Compartilhado;
using static Clube_da_Leitura.ModuloRevista.Revista;

namespace Clube_da_Leitura.ModuloRevista
{
    public class RepositorioRevistaEmBancoDeDados : RepositorioBaseEmBancoDeDados<Revista>, IRepositorioRevista
    {
        private IDbConnection dbConnection;

        protected override string SqlInserir =>
            @"INSERT INTO TBRevistas (Titulo, NumeroEdicao, AnoPublicacao, Status, CaixaId)
              VALUES (@Titulo, @NumeroEdicao, @AnoPublicacao, @Status, @CaixaId)";

        protected override string SqlEditar =>
            @"UPDATE TBRevistas
              SET Titulo = @Titulo,
                  NumeroEdicao = @NumeroEdicao,
                  AnoPublicacao = @AnoPublicacao,
                  Status = @Status,
                  CaixaId = @CaixaId
              WHERE Id = @Id";

        protected override string SqlSelecionarPorId =>
            @"SELECT * FROM TBRevistas WHERE Id = @Id";

        protected override string SqlExcluir =>
            @"DELETE FROM TBRevistas WHERE Id = @Id";

        protected override string SqlSelecionarTodos =>
            @"SELECT * FROM TBRevistas";

        public RepositorioRevistaEmBancoDeDados(IDbConnection dbConnection) : base(dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        protected override Dictionary<string, object> ObterParametros(Revista revista)
        {
            return new Dictionary<string, object>
            {
                { "@Titulo", revista.titulo },
                { "@NumeroEdicao", revista.numeroEdicao },
                { "@AnoPublicacao", revista.anoPublicacao },
                { "@Status", (int)revista.status },
                { "@CaixaId", revista.caixa.id }
            };
        }

        protected override Revista ConverterRegistro(IDataReader reader)
        {
            return new Revista
            {
                id = (int)reader["Id"],
                titulo = (string)reader["Titulo"],
                numeroEdicao = reader["NumeroEdicao"] != DBNull.Value ? (int)reader["NumeroEdicao"] : 0,
                anoPublicacao = reader["AnoPublicacao"] != DBNull.Value ? (int)reader["AnoPublicacao"] : 0,
                status = (StatusDisponveis)(int)reader["Status"],
                caixa = new ModuloCaixa.Caixa { id = (int)reader["CaixaId"] }
            };
        }

        public bool Validacoes(Func<Revista, bool> validacao)
        {
            return SelecionarTodos().Any(validacao);
        }
    }
}
