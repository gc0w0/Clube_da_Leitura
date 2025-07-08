
using Clube_da_Leitura.ModuloAmigo;
using System.ComponentModel.DataAnnotations;

namespace Clube_da_Leitura.WebApp.Models
{
    public class FormularioAmigoViewModel
    {
        [Required(ErrorMessage ="O campo é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public string NomeResponsavel { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [RegularExpression(@"\(\d{2}\) \d{4,5}-\d{4}", ErrorMessage = "O telefone deve estar no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.")]
        public string Telefone { get; set; }
    }

    public class CadastrarAmigoViewModel : FormularioAmigoViewModel
    {
        //public Amigo ParaEntidade()
        //{
        //    return new Amigo(Nome, NomeResponsavel, Telefone);            
        //}
    }

    public class EditarAmigoViewModel : FormularioAmigoViewModel
    {
        public int Id { get; set; }
    }


    public class VisualizacaoAmigoViewModel
    {
        public string Nome { get; set; }

        public string NomeReponsavel { get; set; }

        public string Telefone { get; set; }

        public List<string> Multas { get; set; }

        public List<string> Emprestimos { get; set; }

        public List<string> Reservas { get; set; }
    }

    public class ExcluirAmigoViewModel : VisualizacaoAmigoViewModel
    {

    }

    public class DetalhesAmigoViewModel : VisualizacaoAmigoViewModel
    {

    }

    public class ListarAmigoViewModel
    {
        public string Nome { get; set; }

        public string Telefone { get; set; }
    }

    
}
