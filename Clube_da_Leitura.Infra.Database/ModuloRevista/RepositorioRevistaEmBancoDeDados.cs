using Clube_da_Leitura.Infra.Database.Compartilhado;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloRevista;
using System.Data;

namespace Clube_da_Leitura.Infra.Database.ModuloRevista
{
    public class RepositorioRevistaEmBancoDeDados : RepositorioBaseEmBancoDeDados<Revista>, IRepositorioRevista, IDisposable
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
            @"SELECT R.Id, R.Titulo, R.NumeroEdicao, R.AnoPublicacao, R.Status, R.CaixaId,
       C.Etiqueta, C.Cor, C.Dias
FROM TBRevistas R
JOIN TBCaixas C ON R.CaixaId = C.Id
WHERE R.Id = @Id";

        protected override string SqlExcluir =>
            @"DELETE FROM TBRevistas WHERE Id = @Id";

        protected override string SqlSelecionarTodos =>
            @"SELECT R.Id,R.Titulo,R.NumeroEdicao,R.AnoPublicacao,R.Status,R.CaixaId,C.Etiqueta,C.Cor,C.Dias
        FROM TBRevistas R
        JOIN TBCaixas C ON R.CaixaId = C.Id";

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
                id = ConvertToInt(reader["Id"]),
                titulo = (string)reader["Titulo"],
                numeroEdicao = ConvertToInt(reader["NumeroEdicao"]),
                anoPublicacao = ConvertToInt(reader["AnoPublicacao"]),
                status = (Revista.StatusDisponveis)ConvertToInt(reader["Status"]),
                caixa = new Caixa
                {
                    id = ConvertToInt(reader["CaixaId"]),
                    etiqueta = HasColumn(reader, "Etiqueta") ? (string)reader["Etiqueta"] : null,
                    cor = HasColumn(reader, "Cor") ? (Caixa.CorCaixa)ConvertToInt(reader["Cor"]) : 0,
                    dias = HasColumn(reader, "Dias") ? ConvertToInt(reader["Dias"]) : 0
                }
            };
        }

        public bool Validacoes(Func<Revista, bool> validacao)
        {
            return SelecionarTodos().Any(validacao);
        }

        public void Dispose()
        {
            dbConnection.Dispose();
        }
    }
}
