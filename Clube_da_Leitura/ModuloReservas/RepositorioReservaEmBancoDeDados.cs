using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Clube_da_Leitura.Compartilhado;

namespace Clube_da_Leitura.ModuloReservas
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
            @"SELECT * FROM TBReservas";

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
                id = (int)reader["Id"],
                amigo = new ModuloAmigo.Amigo { id = (int)reader["AmigoId"] },
                revista = new ModuloRevista.Revista { id = (int)reader["RevistaId"] },
                dataReserva = Convert.ToDateTime(reader["DataReserva"]),
                situacao = (SituacaoReserva)(int)reader["Situacao"]
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
