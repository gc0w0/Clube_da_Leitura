using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace Clube_da_Leitura.ModuloAmigo
{
    public class RepositorioAmigoEmBancoDeDados : IRepositorioAmigo
    {
        private readonly string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";

        public void InserirRegistro(Amigo registro)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = @"INSERT INTO TBAmigos (Nome, NomeResponsavel, Telefone)
               VALUES (@Nome, @Responsavel, @Telefone)";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Nome", registro.nome);
            cmd.Parameters.AddWithValue("@Responsavel", registro.nomeReponsavel);
            cmd.Parameters.AddWithValue("@Telefone", registro.telefone);
            cmd.ExecuteNonQuery();
        }

        public bool EditarRegistro(int id, Amigo registroAtualizado)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = @"UPDATE TBAmigos SET Nome = @Nome, NomeResponsavel = @Responsavel, Telefone = @Telefone
                           WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Nome", registroAtualizado.nome);
            cmd.Parameters.AddWithValue("@Responsavel", registroAtualizado.nomeReponsavel);
            cmd.Parameters.AddWithValue("@Telefone", registroAtualizado.telefone);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool ExcluirRegistro(int id)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM TBAmigos WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() > 0;
        }

        public Amigo SelecionarPorId(int id)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM TBAmigos WHERE Id = @Id", conn);
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

            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM TBAmigos", conn);
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
