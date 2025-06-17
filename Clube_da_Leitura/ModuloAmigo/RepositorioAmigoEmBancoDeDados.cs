using Clube_da_Leitura.Compartilhado;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Clube_da_Leitura.ModuloAmigo
{
    public abstract class RepositorioBaseEmBancoDeDados
    {
        private IDbConnection dbConnection;
        protected abstract string SqlInserir { get; }

        protected abstract Dictionary<string, object> ObterParametros(Amigo amigo);

        public void InserirRegistro(Amigo registro)
        {
            dbConnection.Open();
            
            IDbCommand comandoInserir = dbConnection.CreateCommand();

            comandoInserir.CommandText = SqlInserir;

            var parametros = ObterParametros(registro);

            foreach (var p in parametros)
            {
                comandoInserir.AddParametro(p.Key, p.Value);    
            }            

            comandoInserir.ExecuteNonQuery();
        }

    }

    public class RepositorioAmigoEmBancoDeDados : RepositorioBaseEmBancoDeDados, IRepositorioAmigo
    {

        private IDbConnection dbConnection;

        protected override string SqlInserir =>          
                    @"INSERT INTO TBAmigos (Nome, NomeResponsavel, Telefone)
                        VALUES (@Nome, @Responsavel, @Telefone)";

        protected override Dictionary<string, object> ObterParametros(Amigo amigo)
        {
            return new Dictionary<string, object>
            {
                { "@Nome", amigo.nome},
                { "@Responsavel", amigo.nomeReponsavel},
                { "@Telefone", amigo.telefone},
            };                    
        }

        public RepositorioAmigoEmBancoDeDados(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }       
       

        public bool EditarRegistro(int id, Amigo registroAtualizado)
        {
            dbConnection.Open();

            string sql = @"UPDATE TBAmigos SET Nome = @Nome, NomeResponsavel = @Responsavel, Telefone = @Telefone
                           WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(sql, dbConnection as SqlConnection);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Nome", registroAtualizado.nome);
            cmd.Parameters.AddWithValue("@Responsavel", registroAtualizado.nomeReponsavel);
            cmd.Parameters.AddWithValue("@Telefone", registroAtualizado.telefone);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool ExcluirRegistro(int id)
        {
            dbConnection.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM TBAmigos WHERE Id = @Id", dbConnection as SqlConnection);
            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() > 0;
        }

        public Amigo SelecionarPorId(int id)
        {
            dbConnection.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM TBAmigos WHERE Id = @Id", dbConnection as SqlConnection);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Amigo
                {
                    id = (int)reader["Id"],
                    nome = (string)reader["Nome"],
                    nomeReponsavel = (string)reader["NomeResponsavel"],
                    telefone = (string)reader["Telefone"]
                };
            }
            return null;
        }

        public List<Amigo> SelecionarTodos()
        {
            var lista = new List<Amigo>();

            dbConnection.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM TBAmigos", dbConnection as SqlConnection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var amigo = new Amigo
                {
                    id = (int)reader["Id"],
                    nome = (string)reader["Nome"],
                    nomeReponsavel = (string)reader["NomeResponsavel"],
                    telefone = (string)reader["Telefone"]
                };

                lista.Add(amigo);
            }

            return lista;
        }

        public List<Amigo> SelecionarPorFiltro(string letra)
        {
            return SelecionarTodos()
                .Where(a => a.nome.StartsWith(letra, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Amigo> SelecionarPorFiltro2(Predicate<Amigo> condicao)
        {
            return SelecionarTodos().FindAll(condicao);
        }

        public bool Validacoes(Func<Amigo, bool> validacao)
        {
            return SelecionarTodos().Any(validacao);
        }

        
    }
}
