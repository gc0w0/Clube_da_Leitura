using AutoMapper;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloCaixa;
using Clube_da_Leitura.ModuloMultas;
using Clube_da_Leitura.WebApp.Models;

namespace Clube_da_Leitura.WebApp.Profiles
{
    public class CaixaProfile : Profile
    {
        public CaixaProfile()
        {
            CreateMap<CadastrarCaixaViewModel, Caixa>();

            CreateMap<EditarCaixaViewModel, Caixa>();

            CreateMap<Caixa, ExcluirCaixaViewModel>()
                .ForMember(dest => dest.Revistas, opt => opt.MapFrom(src => src.Revistas.Select(x => x.ToString())));                

            CreateMap<Caixa, EditarCaixaViewModel>();

        }
    }
}
