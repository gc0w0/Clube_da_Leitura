using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloReservas;
using Clube_da_Leitura.ModuloRevista;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clube_da_Leitura.Compartilhado
{
    public class ClubeLeituraContextoDeDados
    {
        private const string caminhoArquivo = @"C:\temp\contexto.json";
        public List<Amigo> Amigos { get; set; } 
        public List<Caixa> Caixas { get; set; }
        public List<Emprestimo> Emprestimos { get; set; }
        public List<Reserva> Reservas { get; set; }
        public List<Revista> Revistas { get; set; }

        public ClubeLeituraContextoDeDados()
        {
            Amigos = new List<Amigo>();
            Caixas = new List<Caixa>();
            Emprestimos = new List<Emprestimo>();
            Reservas = new List<Reserva>();
            Revistas = new List<Revista>();
        }

        public ClubeLeituraContextoDeDados(bool carregarDoArquivo)
        {
            if (carregarDoArquivo)
                CarregarDoArquivo();
        }
        public void SalvarEmArquivo()
        {
            var opcoes = new JsonSerializerOptions
            {
                IncludeFields = true,
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            ClubeLeituraContextoDeDados contexto = this; 

            string conteudo = JsonSerializer.Serialize(this, opcoes);
            File.WriteAllText(caminhoArquivo, conteudo);
        }

        public void CarregarDoArquivo()
        {

            var opcoes = new JsonSerializerOptions
            {
                IncludeFields = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string conteudo = File.ReadAllText(caminhoArquivo);
            ClubeLeituraContextoDeDados contexto = JsonSerializer.Deserialize<ClubeLeituraContextoDeDados>(conteudo, opcoes);
            Amigos = contexto.Amigos; 
            Caixas = contexto.Caixas;
            Emprestimos = contexto.Emprestimos;
            Reservas = contexto.Reservas;
            Revistas = contexto.Revistas;
        }
    }
}
