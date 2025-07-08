
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloRevista;
using System.ComponentModel.DataAnnotations;
using static Clube_da_Leitura.ModuloCaixa.Caixa;

namespace Clube_da_Leitura.WebApp.Models
{
    public class FormularioCaixaViewModel
    {

        [Required(ErrorMessage ="O campo é obrigatório")]
        public string Etiqueta { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public CorCaixa Cor { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public int Dias { get; set; }
    }

    public class CadastrarCaixaViewModel : FormularioCaixaViewModel
    {
        //public Caixa ParaEntidade()
        //{
        //    return new Caixa(Nome, NomeResponsavel, Telefone);            
        //}
    }

    public class EditarCaixaViewModel : FormularioCaixaViewModel
    {
        public int Id { get; set; }
    }


    public class VisualizacaoCaixaViewModel
    {
        public string Etiqueta { get; set; }
        public CorCaixa Cor { get; set; }
        public int Dias { get; set; }
        public List<Revista> Revistas { get; set; } = new List<Revista>();
    }

    public class ExcluirCaixaViewModel : VisualizacaoCaixaViewModel
    {

    }

    public class DetalhesCaixaViewModel : VisualizacaoCaixaViewModel
    {

    }

    public class ListarCaixaViewModel
    {
        public string Etiqueta { get; set; }

        public string Dias { get; set; }

        public string Revistas { get; set; }
    }

    
}
