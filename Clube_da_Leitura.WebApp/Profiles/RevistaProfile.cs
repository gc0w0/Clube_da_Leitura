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

            CreateMap<EditarRevistaViewModel, Revista>()
                .ForMember(dest => dest.Caixa, opt => opt.Ignore());

            CreateMap<Revista, EditarRevistaViewModel>()
                .ForMember(dest => dest.CaixaId, opt => opt.MapFrom(src => src.Caixa.Id));

            CreateMap<Revista, ExcluirRevistaViewModel>()
                .ForMember(dest => dest.Emprestimos, opt => opt.MapFrom(src => src.Emprestimos.Select(x => x.ToString())))
                .ForMember(dest => dest.Caixa, opt => opt.Ignore());
        }
    }
}
