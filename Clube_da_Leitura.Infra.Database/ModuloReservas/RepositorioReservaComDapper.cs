using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloReserva;
using Clube_da_Leitura.ModuloRevista;
using Dapper;
using System.Data;

namespace Clube_da_Leitura.Infra.Database.ModuloReservas
{
    public class RepositorioReservaComDapper : IRepositorioReserva
    {
        private IDbConnection dbConnection;

        protected string SqlInserir =>
            @"INSERT INTO TBReservas (AmigoId, RevistaId, DataReserva, Situacao)
              VALUES (@AmigoId, @RevistaId, @DataReserva, @Situacao)";

        protected string SqlEditar =>
            @"UPDATE TBReservas
              SET AmigoId = @AmigoId,
                  RevistaId = @RevistaId,
                  DataReserva = @DataReserva,
                  Situacao = @Situacao
              WHERE Id = @Id";

        protected string SqlSelecionarPorId =>
            @"SELECT R.Id, R.DataReserva, R.Situacao,
            A.Id AS AmigoId, A.Nome AS AmigoNome, A.NomeResponsavel, A.Telefone,
            V.Id AS RevistaId, V.Titulo, V.NumeroEdicao, V.AnoPublicacao, V.Status
            FROM TBReservas R
            JOIN TBAmigos A ON R.AmigoId = A.Id
            JOIN TBRevistas V ON R.RevistaId = V.Id
            WHERE R.Id = @Id";

        protected string SqlExcluir =>
            @"DELETE FROM TBReservas WHERE Id = @Id";

        protected string SqlSelecionarTodos =>
            @"SELECT 
        R.Id, R.DataReserva, R.Situacao,
        A.Id AS AmigoId, A.Nome, A.NomeResponsavel, A.Telefone,
        V.Id AS RevistaId, V.Titulo, V.NumeroEdicao, V.AnoPublicacao, V.Status
    FROM TBReservas R
    JOIN TBAmigos A ON R.AmigoId = A.Id
    JOIN TBRevistas V ON R.RevistaId = V.Id";

        public RepositorioReservaComDapper(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        //protected override Dictionary<string, object> ObterParametros(Reserva reserva)
        //{
        //    return new Dictionary<string, object>
        //    {
        //        { "@AmigoId", reserva.Amigo.Id },
        //        { "@RevistaId", reserva.Revista.Id },
        //        { "@DataReserva", reserva.DataReserva },
        //        { "@Situacao", (int)reserva.Situacao }
        //    };
        //}

        //protected override Reserva ConverterRegistro(IDataReader reader)
        //{
        //    return new Reserva
        //    {
        //        Id = ConvertToInt(reader["Id"]),
        //        DataReserva = Convert.ToDateTime(reader["DataReserva"]),
        //        Situacao = (SituacaoReserva)ConvertToInt(reader["Situacao"]),

        //        Amigo = new Amigo
        //        {
        //            Id = ConvertToInt(reader["AmigoId"]),
        //            Nome = (string)reader["AmigoNome"],
        //            NomeResponsavel = (string)reader["NomeResponsavel"],
        //            Telefone = (string)reader["Telefone"]
        //        },

        //        Revista = new Revista
        //        {
        //            Id = ConvertToInt(reader["RevistaId"]),
        //            Titulo = (string)reader["Titulo"],
        //            NumeroEdicao = ConvertToInt(reader["NumeroEdicao"]),
        //            AnoPublicacao = ConvertToInt(reader["AnoPublicacao"]),
        //            Status = (Revista.StatusDisponveis)ConvertToInt(reader["Status"])
        //        }
        //    };
        //}


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

        public void InserirRegistro(Reserva registro)
        {
            this.dbConnection.Execute(SqlInserir, new
            {
                AmigoId = registro.Amigo.Id,
                RevistaId = registro.Revista.Id,
                DataReserva = registro.DataReserva,
                Situacao = registro.Situacao
            });

        }

        public bool EditarRegistro(int id, Reserva registroAtualizado)
        {
            registroAtualizado.Id = id;
            return this.dbConnection.Execute(SqlEditar, new
            {
                Id = registroAtualizado.Id,
                AmigoId = registroAtualizado.Amigo.Id,
                RevistaId = registroAtualizado.Revista.Id,
                DataReserva = registroAtualizado.DataReserva,
                Situacao = registroAtualizado.Situacao
            }) > 0;

        }

        public Reserva SelecionarPorId(int id)
        {
            return dbConnection.Query<Reserva, Amigo, Revista, Reserva>(
                SqlSelecionarPorId,
                (reserva, amigo, revista) =>
                {
                    reserva.Amigo = amigo;
                    reserva.Revista = revista;
                    return reserva;
                },
                new { Id = id },
                splitOn: "AmigoId,RevistaId"
            ).FirstOrDefault();
        }

        public bool ExcluirRegistro(int id)
        {
            return this.dbConnection.Execute(SqlExcluir, new { Id = id }) > 0;

        }

        public List<Reserva> SelecionarTodos()
        {
            return dbConnection.Query<Reserva, Amigo, Revista, Reserva>(
                SqlSelecionarTodos,
                (reserva, amigo, revista) =>
                {
                    reserva.Amigo = amigo;
                    reserva.Revista = revista;
                    return reserva;
                },
                splitOn: "AmigoId,RevistaId"
            ).ToList();
        }
    }
}
