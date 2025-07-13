
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloReserva;
using Clube_da_Leitura.ModuloRevista;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Clube_da_Leitura.WebApp.Models
{
    public class FormularioReservaViewModel
    {
        [Required(ErrorMessage ="O campo é obrigatório")]
        public int AmigoId { get; set; }
        //public Amigo Amigo { get; set; }
        public List<SelectListItem>? AmigosDisponiveis { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        //public Revista Revista { get; set; }
        public int RevistaId { get; set; }
        public List<SelectListItem>? RevistasDisponiveis { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public DateTime DataReserva { get; set; }
    }

    public class CadastrarReservaViewModel : FormularioReservaViewModel
    {
        public CadastrarReservaViewModel()
        {
            AmigosDisponiveis = new List<SelectListItem>();
            RevistasDisponiveis = new List<SelectListItem>();
            DataReserva = DateTime.Now;
        }

        public CadastrarReservaViewModel(List<Amigo> amigos, List<Revista> revistas) : this()
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

    public class EditarReservaViewModel : FormularioReservaViewModel
    {
        public int Id { get; set; }
    }


    public class VisualizacaoReservaViewModel
    {
        public Amigo Amigo { get; set; }
        public Revista Revista { get; set; }
        public DateTime DataReserva { get; set; }
        public SituacaoReserva Situacao { get; set; }
    }

    public class ExcluirReservaViewModel : VisualizacaoReservaViewModel
    {

    }

    public class DetalhesReservaViewModel : VisualizacaoReservaViewModel
    {

    }

    public class ListarReservaViewModel
    {
        public string Amigo { get; set; }
        public string DataReserva { get; set; }
        public string Situacao { get; set; }
    }

    
}
