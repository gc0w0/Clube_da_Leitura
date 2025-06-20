using Clube_da_Leitura.ModuloAmigo;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.Compartilhado
{
    public abstract class RepositorioBaseEmBancoDeDados<T> : IRepositorio<T> where T : EntidadeBase<T>
    {
        private IDbConnection dbConnection;
        protected abstract string SqlInserir { get; }
        protected abstract string SqlEditar { get; }
        protected abstract string SqlSelecionarPorId { get; }
        protected abstract string SqlExcluir { get; }
        protected abstract string SqlSelecionarTodos { get; }
        protected abstract T ConverterRegistro(IDataReader reader);
        protected abstract Dictionary<string, object> ObterParametros(T registro);

        public RepositorioBaseEmBancoDeDados(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;            
        }

        public void InserirRegistro(T registro)
        {            
            dbConnection.Open();
            IDbCommand comandoInserir = dbConnection.CreateCommand();
            comandoInserir.CommandText = SqlInserir + "; select scope_identity()";
            var parametros = ObterParametros(registro);
            foreach (var p in parametros)
            {
                comandoInserir.AddParametro(p.Key, p.Value);
            }

            registro.id = Convert.ToInt32( comandoInserir.ExecuteScalar() );

            dbConnection.Close();
        }

        public bool EditarRegistro(int id, T registroAtualizado)
        {
            using (var dbConnection = this.dbConnection)
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
        }

        public T SelecionarPorId(int id)
        {
            dbConnection.Close();
            dbConnection.Open();
            IDbCommand comandoInserir = dbConnection.CreateCommand();
            comandoInserir.CommandText = SqlSelecionarPorId;
            comandoInserir.AddParametro("@Id", id);
            using var reader = comandoInserir.ExecuteReader();
            return reader.Read() ? ConverterRegistro(reader) : null;

        }

        public bool ExcluirRegistro(int id)
        {
            dbConnection.Close();
            dbConnection.Open();
            IDbCommand comandoInserir = dbConnection.CreateCommand();
            comandoInserir.CommandText = SqlExcluir;
            comandoInserir.AddParametro("@Id", id);
            return comandoInserir.ExecuteNonQuery() > 0;
        }

        public List<T> SelecionarTodos()
        {
            var lista = new List<T>();
            dbConnection.Close();   
            dbConnection.Open();
            IDbCommand comandoInserir = dbConnection.CreateCommand();
            comandoInserir.CommandText = SqlSelecionarTodos;
            using var reader = comandoInserir.ExecuteReader();
            while (reader.Read())
                lista.Add(ConverterRegistro(reader));

            return lista;
        }

        public bool Validacoes(Func<T, bool> validacao)
        {
            return SelecionarTodos().Any(validacao);
        }


    }
}
