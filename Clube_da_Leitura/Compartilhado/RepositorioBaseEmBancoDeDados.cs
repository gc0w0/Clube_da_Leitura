using Clube_da_Leitura.ModuloAmigo;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.Compartilhado
{
    public abstract class RepositorioBaseEmBancoDeDados<T> : IRepositorio<T> where T : EntidadeBase<T> 
    {
        private IDbConnection dbConnection;
        private bool bancoSQLite => dbConnection is SqliteConnection;
        protected abstract string SqlInserir { get; }
        protected abstract string SqlEditar { get; }
        protected abstract string SqlSelecionarPorId { get; }
        protected abstract string SqlExcluir { get; }
        protected abstract string SqlSelecionarTodos { get; }
        protected abstract T ConverterRegistro(IDataReader reader);
        protected abstract Dictionary<string, object> ObterParametros(T registro);
        protected int ConvertToInt(object valor)
        {
            return valor != DBNull.Value ? Convert.ToInt32(Convert.ToInt64(valor)) : 0;
        }

        public RepositorioBaseEmBancoDeDados(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }
        public void InserirRegistro(T registro)
        {
            try
            {
                dbConnection.Open();

                IDbCommand comandoInserir = dbConnection.CreateCommand();
                comandoInserir.CommandText = SqlInserir;

                if (bancoSQLite)
                    comandoInserir.CommandText += "; SELECT last_insert_rowid();";
                else
                    comandoInserir.CommandText += "; SELECT SCOPE_IDENTITY();";

                var parametros = ObterParametros(registro);
                foreach (var p in parametros)
                    comandoInserir.AddParametro(p.Key, p.Value);

                registro.id = Convert.ToInt32(comandoInserir.ExecuteScalar());
            }
            finally
            {
                dbConnection.Close();
            }
        }


        public bool EditarRegistro(int id, T registroAtualizado)
        {
            try
            {
                dbConnection.Open();
                IDbCommand comandoInserir = dbConnection.CreateCommand();
                comandoInserir.CommandText = SqlEditar;
                comandoInserir.AddParametro("@Id", id);
                var parametros = ObterParametros(registroAtualizado);
                foreach (var p in parametros)
                {
                    comandoInserir.AddParametro(p.Key, p.Value);
                }

                return comandoInserir.ExecuteNonQuery() > 0;
            }
            finally { dbConnection.Close();}
        }

        public T SelecionarPorId(int id)
        {
            try
            {
            dbConnection.Open();
            IDbCommand comandoInserir = dbConnection.CreateCommand();
            comandoInserir.CommandText = SqlSelecionarPorId;
            comandoInserir.AddParametro("@Id", id);
            using var reader = comandoInserir.ExecuteReader();
            return reader.Read() ? ConverterRegistro(reader) : null;
            }

            finally { dbConnection.Close(); }
        }

        public bool ExcluirRegistro(int id)
        {
            try
            {
                dbConnection.Open();
                IDbCommand comandoInserir = dbConnection.CreateCommand();
                comandoInserir.CommandText = SqlExcluir;
                comandoInserir.AddParametro("@Id", id);
                return comandoInserir.ExecuteNonQuery() > 0;
            }

            finally { dbConnection.Close(); }
        }

        public List<T> SelecionarTodos()
        {
            try
            {
                var lista = new List<T>();

                dbConnection.Open();
                IDbCommand comandoInserir = dbConnection.CreateCommand();
                comandoInserir.CommandText = SqlSelecionarTodos;
                using var reader = comandoInserir.ExecuteReader();
                while (reader.Read())
                    lista.Add(ConverterRegistro(reader));

                return lista;
            }
            finally { dbConnection.Close(); }
        }

        public bool Validacoes(Func<T, bool> validacao)
        {
            return SelecionarTodos().Any(validacao);
        }

        public void Dispose()
        {
            dbConnection.Dispose();
        }

    }
}
