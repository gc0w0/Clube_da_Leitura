using AutoMapper;
using Clube_da_Leitura.ModuloRevista;
using Clube_da_Leitura.ModuloMultas;
using Clube_da_Leitura.WebApp.Models;

namespace Clube_da_Leitura.WebApp.Profiles
{
    public class RevistaProfile : Profile
    {
        public RevistaProfile()
        {
            CreateMap<CadastrarRevistaViewModel, Revista>();

            CreateMap<EditarRevistaViewModel, Revista>();

            CreateMap<Revista, ExcluirRevistaViewModel>()
                .ForMember(dest => dest.Emprestimos, opt => opt.MapFrom(src => src.Emprestimos.Select(x => x.ToString())))
                .ForMember(dest => dest.Caixa, opt => opt.Ignore());

            CreateMap<Revista, EditarRevistaViewModel>();

        }
    }
}
