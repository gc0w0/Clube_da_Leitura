using Clube_da_Leitura.Infra.Database.Compartilhado;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloRevista;
using Dapper;
using System.Data;

namespace Clube_da_Leitura.Infra.Database.ModuloRevista
{
    public class RepositorioRevistaComDapper : IRepositorioRevista, IDisposable
    {
        private IDbConnection dbConnection;

        protected  string SqlInserir =>
            @"INSERT INTO TBRevistas (Titulo, NumeroEdicao, AnoPublicacao, Status, CaixaId)
              VALUES (@Titulo, @NumeroEdicao, @AnoPublicacao, @Status, @CaixaId)";

        protected  string SqlEditar =>
            @"UPDATE TBRevistas
              SET Titulo = @Titulo,
                  NumeroEdicao = @NumeroEdicao,
                  AnoPublicacao = @AnoPublicacao,
                  Status = @Status,
                  CaixaId = @CaixaId
              WHERE Id = @Id";

        protected  string SqlSelecionarPorId =>
            @"SELECT R.Id, R.Titulo, R.NumeroEdicao, R.AnoPublicacao, R.Status,
                 C.Id AS CaixaId, C.Etiqueta, C.Cor, C.Dias
          FROM TBRevistas R
          JOIN TBCaixas C ON R.CaixaId = C.Id
          WHERE R.Id = @Id";

        protected  string SqlExcluir =>
            @"DELETE FROM TBRevistas WHERE Id = @Id";

        protected  string SqlSelecionarTodos =>
            @"SELECT R.Id, R.Titulo, R.NumeroEdicao, R.AnoPublicacao, R.Status, 
            C.Id AS CaixaId, C.Etiqueta, C.Cor, C.Dias
            FROM TBRevistas R
            JOIN TBCaixas C ON R.CaixaId = C.Id";

        public RepositorioRevistaComDapper(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        //protected override Dictionary<string, object> ObterParametros(Revista revista)
        //{
        //    return new Dictionary<string, object>
        //    {
        //        { "@Titulo", revista.Titulo },
        //        { "@NumeroEdicao", revista.NumeroEdicao },
        //        { "@AnoPublicacao", revista.AnoPublicacao },
        //        { "@Status", (int)revista.Status },
        //        { "@CaixaId", revista.Caixa.Id }
        //    };
        //}

        //protected override Revista ConverterRegistro(IDataReader reader)
        //{
        //    return new Revista
        //    {
        //        Id = ConvertToInt(reader["Id"]),
        //        Titulo = (string)reader["Titulo"],
        //        NumeroEdicao = ConvertToInt(reader["NumeroEdicao"]),
        //        AnoPublicacao = ConvertToInt(reader["AnoPublicacao"]),
        //        Status = (Revista.StatusDisponveis)ConvertToInt(reader["Status"]),
        //        Caixa = new Caixa
        //        {
        //            Id = ConvertToInt(reader["CaixaId"]),
        //            Etiqueta = HasColumn(reader, "Etiqueta") ? (string)reader["Etiqueta"] : null,
        //            Cor = HasColumn(reader, "Cor") ? (Caixa.CorCaixa)ConvertToInt(reader["Cor"]) : 0,
        //            Dias = HasColumn(reader, "Dias") ? ConvertToInt(reader["Dias"]) : 0
        //        }
        //    };
        //}

        public bool Validacoes(Func<Revista, bool> validacao)
        {
            return SelecionarTodos().Any(validacao);
        }

        public void Dispose()
        {
           
        }

        public void InserirRegistro(Revista registro)
        {
            this.dbConnection.Execute(SqlInserir, new
            {
                registro.Titulo,
                registro.NumeroEdicao,
                registro.AnoPublicacao,
                registro.Status,
                CaixaId = registro.Caixa.Id
            });

        }

        public bool EditarRegistro(int id, Revista registroAtualizado)
        {
            registroAtualizado.Id = id;
            return this.dbConnection.Execute(SqlEditar, new
            {
                registroAtualizado.Id,
                registroAtualizado.Titulo,
                registroAtualizado.NumeroEdicao,
                registroAtualizado.AnoPublicacao,
                registroAtualizado.Status,
                CaixaId = registroAtualizado.Caixa.Id
            }) > 0;

        }

        public Revista SelecionarPorId(int id)
        {
            return dbConnection.Query<Revista, Caixa, Revista>(
                SqlSelecionarPorId,
                (revista, caixa) =>
                {
                    revista.Caixa = caixa;
                    return revista;
                },
                new { Id = id },
                splitOn: "CaixaId"
            ).FirstOrDefault();
        }

        public bool ExcluirRegistro(int id)
        {
            return this.dbConnection.Execute(SqlExcluir, new { Id = id }) > 0;

        }

        public List<Revista> SelecionarTodos()
        {
            return dbConnection.Query<Revista, Caixa, Revista>(
                SqlSelecionarTodos,
                (revista, caixa) =>
                {
                    revista.Caixa = caixa;
                    return revista;
                },
                splitOn: "CaixaId"
            ).ToList();
        }
    }
}
