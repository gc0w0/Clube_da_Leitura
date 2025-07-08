
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloReserva;
using Clube_da_Leitura.ModuloRevista;
using System.ComponentModel.DataAnnotations;

namespace Clube_da_Leitura.WebApp.Models
{
    public class FormularioReservaViewModel
    {
        [Required(ErrorMessage ="O campo é obrigatório")]
        public Amigo Amigo { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public Revista Revista { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public DateTime DataReserva { get; set; }
    }

    public class CadastrarReservaViewModel : FormularioReservaViewModel
    {
        //public Reserva ParaEntidade()
        //{
        //    return new Reserva(Nome, NomeResponsavel, Telefone);            
        //}
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
