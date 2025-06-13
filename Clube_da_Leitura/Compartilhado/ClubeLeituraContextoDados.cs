using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Clube_da_Leitura.Compartilhado
{
    public class ClubeLeituraContextoDados
    {
        private const string caminhoArquivo = @"C:\temp\contexto.json";

        public List<Amigo> Amigos { get; set; }

        public List<Caixa> Caixas { get; set; }

        public ClubeLeituraContextoDados()
        {
            Amigos = new List<Amigo>();
            Caixas = new List<Caixa>();
        }

        public ClubeLeituraContextoDados(bool carregarDoArquivo)
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

            ClubeLeituraContextoDados contexto = this;

            string conteudo = JsonSerializer.Serialize(this, opcoes);

            File.WriteAllText(caminhoArquivo, conteudo);
        }

        public void CarregarDoArquivo()
        {
            var opcoes = new JsonSerializerOptions
            {
                IncludeFields = true,
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            string conteudo = File.ReadAllText(caminhoArquivo);
            ClubeLeituraContextoDados contexto = JsonSerializer.Deserialize<ClubeLeituraContextoDados>(conteudo, opcoes)!;

            Amigos = contexto.Amigos;
            Caixas = contexto.Caixas;
        }
    }
}
