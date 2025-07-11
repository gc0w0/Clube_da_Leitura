using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloRevista;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public Caixa Caixa { get; set; } //ver se precisa disso ou somente o asp-select-itens

        [Required(ErrorMessage = "O campo é obrigatório")]
        public int CaixaId { get; set; }
        public List<SelectListItem> ? CaixasDisponiveis { get; set; }

    }

    public class CadastrarRevistaViewModel : FormularioRevistaViewModel
    {
        //public Revista ParaEntidade()
        //{
        //    return new Revista(Nome, NomeResponsavel, Telefone);            
        //}

        public CadastrarRevistaViewModel()
        {
            CaixasDisponiveis = new List<SelectListItem>();
        }

        public CadastrarRevistaViewModel(List<Caixa> caixas) : this()
        {
            foreach (var caixa in caixas)
            {
                var selectViewModel = new SelectListItem(caixa.Etiqueta, caixa.Id.ToString());
                CaixasDisponiveis?.Add(selectViewModel);
            }
        }
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
        public Guid? CaixaId { get; set; }
        public List<SelectListItem>? CaixasDisponiveis { get; set; }
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
