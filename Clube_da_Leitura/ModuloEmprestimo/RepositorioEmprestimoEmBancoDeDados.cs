using System;
using System.Collections.Generic;
using System.Data;
using Clube_da_Leitura.Compartilhado;

namespace Clube_da_Leitura.ModuloEmprestimo
{
    public class RepositorioEmprestimoEmBancoDeDados : RepositorioBaseEmBancoDeDados<Emprestimo>, IRepositorioEmprestimo
    {
        private IDbConnection dbConnection;

        protected override string SqlInserir =>
            @"INSERT INTO TBEmprestimos 
              (AmigoId, RevistaId, DataEmprestimo, DataDevolucao, DataPrevistaDevolucao, Situacao) 
              VALUES 
              (@AmigoId, @RevistaId, @DataEmprestimo, @DataDevolucao, @DataPrevistaDevolucao, @Situacao)";

        protected override string SqlEditar =>
            @"UPDATE TBEmprestimos SET 
              AmigoId = @AmigoId,
              RevistaId = @RevistaId,
              DataEmprestimo = @DataEmprestimo,
              DataDevolucao = @DataDevolucao,
              DataPrevistaDevolucao = @DataPrevistaDevolucao,
              Situacao = @Situacao
              WHERE Id = @Id";

        protected override string SqlSelecionarPorId =>
            @"SELECT * FROM TBEmprestimos WHERE Id = @Id";

        protected override string SqlExcluir =>
            @"DELETE FROM TBEmprestimos WHERE Id = @Id";

        protected override string SqlSelecionarTodos =>
            @"SELECT * FROM TBEmprestimos";

        public RepositorioEmprestimoEmBancoDeDados(IDbConnection dbConnection) : base(dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        protected override Dictionary<string, object> ObterParametros(Emprestimo emprestimo)
        {
            return new Dictionary<string, object>
            {
                { "@AmigoId", emprestimo.amigo.id },
                { "@RevistaId", emprestimo.revista.id },
                { "@DataEmprestimo", emprestimo.dataEmprestimo },
                { "@DataDevolucao", emprestimo.dataDevolucao.HasValue ? emprestimo.dataDevolucao : DBNull.Value },
                { "@DataPrevistaDevolucao", emprestimo.dataPrevistaDevolucao.HasValue ? emprestimo.dataPrevistaDevolucao : DBNull.Value },
                { "@Situacao", (int)emprestimo.situacao }
            };
        }

        protected override Emprestimo ConverterRegistro(IDataReader reader)
        {
            return new Emprestimo
            {
                id = (int)reader["Id"],
                amigo = new ModuloAmigo.Amigo { id = (int)reader["AmigoId"] }, // Será necessário buscar os dados completos depois, se quiser exibir
                revista = new ModuloRevista.Revista { id = (int)reader["RevistaId"] },
                dataEmprestimo = Convert.ToDateTime(reader["DataEmprestimo"]),
                dataDevolucao = reader["DataDevolucao"] == DBNull.Value ? null : Convert.ToDateTime(reader["DataDevolucao"]),
                dataPrevistaDevolucao = reader["DataPrevistaDevolucao"] == DBNull.Value ? null : Convert.ToDateTime(reader["DataPrevistaDevolucao"]),
                situacao = (SituacaoEmprestimo)(int)reader["Situacao"]
            };
        }

        public List<Emprestimo> SelecionarTodosAbertos()
        {
            return SelecionarTodos().Where(e => e.situacao == SituacaoEmprestimo.Aberto).ToList();
        }

        public List<Emprestimo> SelecionarTodosFechados()
        {
            return SelecionarTodos().Where(e => e.situacao == SituacaoEmprestimo.Fechado).ToList();
        }

        public bool Validacoes(Func<Emprestimo, bool> validacao)
        {
            return SelecionarTodos().Any(validacao);
        }
    }
}
