using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clube_da_Leitura.ModuloAmigo;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using static Clube_da_Leitura.ModuloCaixa.Caixa;
namespace Clube_da_Leitura.ModuloCaixa
{
    public class RepositorioCaixaEmBancoDeDados : IRepositorioCaixa
    {
        private readonly string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ClubeLeituraDataBase;Integrated Security=True;";
        public void InserirRegistro(Caixa registro)
        {
            using SqliteConnection con = new SqliteConnection(connectionString);
            con.Open();
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = @"INSERT INTO TBCaixas (Etiqueta, Cor, Dias)
               VALUES (@Etiqueta, @Cor, @Dias)";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Etiqueta", registro.etiqueta);
            cmd.Parameters.AddWithValue("@Cor", registro.cor);
            cmd.Parameters.AddWithValue("@Dias", registro.dias);
            cmd.ExecuteNonQuery();
        }
        public bool EditarRegistro(int id, Caixa registroAtualizado)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = @"UPDATE TBCaixas SET Etiqueta = @Etiqueta, Cor = @Cor, Dias = @Dias WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Etiqueta", registroAtualizado.etiqueta);
            cmd.Parameters.AddWithValue("@Cor", registroAtualizado.cor);
            cmd.Parameters.AddWithValue("@Dias", registroAtualizado.dias);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool ExcluirRegistro(int id)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM TBCaixas WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() > 0;
        }


        public Caixa SelecionarPorId(int id)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM TBCaixas WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Caixa
                {
                    id = (int)reader["Id"],
                    etiqueta = (string)reader["Etiqueta"],
                    cor = (CorCaixa)(int)reader["Cor"],
                    dias = (int)reader["Dias"]
                };
            }
            return null;
        }

        public List<Caixa> SelecionarTodos()
        {
            var lista = new List<Caixa>();

            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM TBCaixas", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var caixa = new Caixa
                {
                    id = (int)reader["Id"],
                    etiqueta = (string)reader["Etiqueta"],
                    cor = (CorCaixa)(int)reader["Cor"],
                    dias = (int)reader["Dias"]
                };

                lista.Add(caixa);
            }
            return lista;
        }

        public bool Validacoes(Func<Caixa, bool> validacao)
        {
            return SelecionarTodos().Any(validacao);
        }
    }
}
