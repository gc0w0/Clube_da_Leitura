
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloRevista;
using System.ComponentModel.DataAnnotations;
using static Clube_da_Leitura.ModuloRevista.Revista;

namespace Clube_da_Leitura.WebApp.Models
{
    public class FormularioRevistaViewModel
    {
        [Required(ErrorMessage ="O campo é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public int NumeroEdicao { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public int AnoPublicacao { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public StatusDisponveis Status { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public Caixa Caixa { get; set; }

    }

    public class CadastrarRevistaViewModel : FormularioRevistaViewModel
    {
        //public Revista ParaEntidade()
        //{
        //    return new Revista(Nome, NomeResponsavel, Telefone);            
        //}
    }

    public class EditarRevistaViewModel : FormularioRevistaViewModel
    {
        public int Id { get; set; }
    }


    public class VisualizacaoRevistaViewModel
    {
        public string Titulo { get; set; }
        public int NumeroEdicao { get; set; }
        public int AnoPublicacao { get; set; }
        public StatusDisponveis Status { get; set; }
        public Caixa Caixa { get; set; }
        public List<string> Emprestimos { get; set; } 
        public List<string> Reserva { get; set; } 
    }

    public class ExcluirRevistaViewModel : VisualizacaoRevistaViewModel
    {

    }

    public class DetalhesRevistaViewModel : VisualizacaoRevistaViewModel
    {

    }

    public class ListarRevistaViewModel
    {
        public string Titulo { get; set; }

        public string Status { get; set; }
        public string Caixa { get; set; }

        public string Reserva { get; set; }
    }

    
}
