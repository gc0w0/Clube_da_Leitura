using AutoMapper;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloMultas;
using Clube_da_Leitura.WebApp.Models;

namespace Clube_da_Leitura.WebApp.Profiles
{
    public class EmprestimoProfile : Profile
    {
        public EmprestimoProfile()
        {
            CreateMap<CadastrarEmprestimoViewModel, Emprestimo>();

            CreateMap<EditarEmprestimoViewModel, Emprestimo>();
            CreateMap<Emprestimo, EditarEmprestimoViewModel>();

            CreateMap<Emprestimo, ExcluirEmprestimoViewModel>()
                .ForMember(dest => dest.Multa, opt => opt.MapFrom(src => src.Multa.Select(x => x.ToString())));                


            CreateMap<Emprestimo, DevolucaoEmprestimoViewModel>();
            CreateMap<DevolucaoEmprestimoViewModel, Emprestimo>();


        }
    }
}
