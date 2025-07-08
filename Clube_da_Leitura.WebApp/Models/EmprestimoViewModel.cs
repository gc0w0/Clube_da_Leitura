
using Clube_da_Leitura.Dominio.ModuloEmprestimo;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloMultas;
using Clube_da_Leitura.ModuloRevista;
using System.ComponentModel.DataAnnotations;

namespace Clube_da_Leitura.WebApp.Models
{
    public class FormularioEmprestimoViewModel
    {
        [Required(ErrorMessage ="O campo é obrigatório")]
        public Amigo Amigo { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public Revista Revista { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public DateTime? DataEmprestimo { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public DateTime? DataDevolucao { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public DateTime? DataPrevistaDevolucao { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public SituacaoEmprestimo Situacao { get; set; }  //Aberto / Concluido / Atrasado
    }

    public class CadastrarEmprestimoViewModel : FormularioEmprestimoViewModel
    {
        //public Emprestimo ParaEntidade()
        //{
        //    return new Emprestimo(Nome, NomeResponsavel, Telefone);            
        //}
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
        public List<Multa> Multa { get; set; } = new List<Multa>();
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

    
}
