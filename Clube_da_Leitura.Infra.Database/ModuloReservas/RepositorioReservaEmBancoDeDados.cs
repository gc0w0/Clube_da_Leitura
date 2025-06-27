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
                { "@AmigoId", reserva.amigo.id },
                { "@RevistaId", reserva.revista.id },
                { "@DataReserva", reserva.dataReserva },
                { "@Situacao", (int)reserva.situacao }
            };
        }

        protected override Reserva ConverterRegistro(IDataReader reader)
        {
            return new Reserva
            {
                id = ConvertToInt(reader["Id"]),
                dataReserva = Convert.ToDateTime(reader["DataReserva"]),
                situacao = (SituacaoReserva)ConvertToInt(reader["Situacao"]),

                amigo = new Amigo
                {
                    id = ConvertToInt(reader["AmigoId"]),
                    nome = (string)reader["AmigoNome"],
                    nomeReponsavel = (string)reader["NomeResponsavel"],
                    telefone = (string)reader["Telefone"]
                },

                revista = new Revista
                {
                    id = ConvertToInt(reader["RevistaId"]),
                    titulo = (string)reader["Titulo"],
                    numeroEdicao = ConvertToInt(reader["NumeroEdicao"]),
                    anoPublicacao = ConvertToInt(reader["AnoPublicacao"]),
                    status = (Revista.StatusDisponveis)ConvertToInt(reader["Status"])
                }
            };
        }


        public List<Reserva> SelecionarTodosAbertos()
        {
            return SelecionarTodos()
                .Where(r => r.situacao == SituacaoReserva.Ativa)
                .ToList();
        }

        public bool Validacoes(Func<Reserva, bool> validacao)
        {
            return SelecionarTodos().Any(validacao);
        }


    }
}
