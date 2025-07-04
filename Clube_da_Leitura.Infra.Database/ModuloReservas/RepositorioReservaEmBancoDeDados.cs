using Clube_da_Leitura.Infra.Database.Compartilhado;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloReservas;
using Clube_da_Leitura.ModuloRevista;
using System.Data;

namespace Clube_da_Leitura.Infra.Database.ModuloReservas
{
    public class RepositorioReservaEmBancoDeDados : RepositorioBaseEmBancoDeDados<Reserva>, IRepositorioReserva
    {
        private IDbConnection dbConnection;

        protected override string SqlInserir =>
            @"INSERT INTO TBReservas (AmigoId, RevistaId, DataReserva, Situacao)
              VALUES (@AmigoId, @RevistaId, @DataReserva, @Situacao)";

        protected override string SqlEditar =>
            @"UPDATE TBReservas
              SET AmigoId = @AmigoId,
                  RevistaId = @RevistaId,
                  DataReserva = @DataReserva,
                  Situacao = @Situacao
              WHERE Id = @Id";

        protected override string SqlSelecionarPorId =>
            @"SELECT * FROM TBReservas WHERE Id = @Id";

        protected override string SqlExcluir =>
            @"DELETE FROM TBReservas WHERE Id = @Id";

        protected override string SqlSelecionarTodos =>
            @"SELECT R.Id, R.DataReserva, R.Situacao,
             A.Id AS AmigoId, A.Nome AS AmigoNome, A.NomeResponsavel, A.Telefone,
             V.Id AS RevistaId, V.Titulo, V.NumeroEdicao, V.AnoPublicacao, V.Status
      FROM TBReservas R
      JOIN TBAmigos A ON R.AmigoId = A.Id
      JOIN TBRevistas V ON R.RevistaId = V.Id";

        public RepositorioReservaEmBancoDeDados(IDbConnection dbConnection) : base(dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        protected override Dictionary<string, object> ObterParametros(Reserva reserva)
        {
            return new Dictionary<string, object>
            {
                { "@AmigoId", reserva.Amigo.Id },
                { "@RevistaId", reserva.Revista.Id },
                { "@DataReserva", reserva.DataReserva },
                { "@Situacao", (int)reserva.Situacao }
            };
        }

        protected override Reserva ConverterRegistro(IDataReader reader)
        {
            return new Reserva
            {
                Id = ConvertToInt(reader["Id"]),
                DataReserva = Convert.ToDateTime(reader["DataReserva"]),
                Situacao = (SituacaoReserva)ConvertToInt(reader["Situacao"]),

                Amigo = new Amigo
                {
                    Id = ConvertToInt(reader["AmigoId"]),
                    Nome = (string)reader["AmigoNome"],
                    NomeReponsavel = (string)reader["NomeResponsavel"],
                    Telefone = (string)reader["Telefone"]
                },

                Revista = new Revista
                {
                    Id = ConvertToInt(reader["RevistaId"]),
                    Titulo = (string)reader["Titulo"],
                    NumeroEdicao = ConvertToInt(reader["NumeroEdicao"]),
                    AnoPublicacao = ConvertToInt(reader["AnoPublicacao"]),
                    status = (Revista.StatusDisponveis)ConvertToInt(reader["Status"])
                }
            };
        }


        public List<Reserva> SelecionarTodosAbertos()
        {
            return SelecionarTodos()
                .Where(r => r.Situacao == SituacaoReserva.Ativa)
                .ToList();
        }

        public bool Validacoes(Func<Reserva, bool> validacao)
        {
            return SelecionarTodos().Any(validacao);
        }


    }
}
