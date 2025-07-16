
using Clube_da_Leitura.Dominio.ModuloEmprestimo;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloRevista;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Clube_da_Leitura.WebApp.Models
{
    public class FormularioEmprestimoViewModel
    {
        [Required(ErrorMessage ="O campo é obrigatório")]
        public int AmigoId { get; set; }
        public List<SelectListItem>? AmigosDisponiveis { get; set; }

        //public Amigo Amigo { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public int RevistaId { get; set; }
        public List<SelectListItem> ? RevistasDisponiveis { get; set; }
        //public Revista Revista { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public DateTime? DataEmprestimo { get; set; }

        //[Required(ErrorMessage = "O campo é obrigatório")]
        //public DateTime? DataDevolucao { get; set; }

        //[Required(ErrorMessage = "O campo é obrigatório")]
        //public DateTime? DataPrevistaDevolucao { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public SituacaoEmprestimo Situacao { get; set; }  //Aberto / Concluido / Atrasado
    }

    public class CadastrarEmprestimoViewModel : FormularioEmprestimoViewModel
    {
        //public Emprestimo ParaEntidade()
        //{
        //    return new Emprestimo(Nome, NomeResponsavel, Telefone);            
        //}

        public CadastrarEmprestimoViewModel()
        {
            AmigosDisponiveis = new List<SelectListItem>();
            RevistasDisponiveis = new List<SelectListItem>();
            DataEmprestimo = DateTime.Now;
        }

        public CadastrarEmprestimoViewModel(List<Amigo> amigos, List<Revista> revistas) : this()
        {
            foreach (var amigo in amigos)
            {
                var selectViewModel = new SelectListItem(amigo.Nome, amigo.Id.ToString());
                AmigosDisponiveis?.Add(selectViewModel);
            }
            foreach (var revista in revistas)
            {
                var selectViewModel = new SelectListItem(revista.Titulo, revista.Id.ToString());
                RevistasDisponiveis?.Add(selectViewModel);
            }
        }
    }

    public class EditarEmprestimoViewModel : FormularioEmprestimoViewModel
    {
        public int Id { get; set; }
    }


    public class VisualizacaoEmprestimoViewModel
    {
        public Amigo Amigo { get; set; }
        public Revista Revista { get; set; }
        public DateTime? DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public DateTime? DataPrevistaDevolucao { get; set; }
        public SituacaoEmprestimo Situacao { get; set; }  //Aberto / Concluido / Atrasado
        public List<string> Multa { get; set; } 
        public int AmigoId { get; set; }
        public int RevistaId { get; set; }
        public List<SelectListItem>? AmigosDisponiveis { get; set; }
        public List<SelectListItem>? RevistasDisponiveis { get; set; }
    }

    public class ExcluirEmprestimoViewModel : VisualizacaoEmprestimoViewModel
    {

    }

    public class DetalhesEmprestimoViewModel : VisualizacaoEmprestimoViewModel
    {

    }

    public class ListarEmprestimoViewModel
    {
        public string Amigo { get; set; }

        public string Revista { get; set; }
        public string DataEmprestimo { get; set; }
        public string Situacao { get; set; }
    }

    public class DevolucaoEmprestimoViewModel : FormularioEmprestimoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A data de devolução é obrigatória.")]
        public DateTime? DataDevolucao { get; set; }

    }
}
