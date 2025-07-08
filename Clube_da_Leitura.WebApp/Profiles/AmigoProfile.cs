using AutoMapper;
using Clube_da_Leitura.ModuloAmigo;
using Clube_da_Leitura.ModuloMultas;
using Clube_da_Leitura.WebApp.Models;

namespace Clube_da_Leitura.WebApp.Profiles
{
    public class AmigoProfile : Profile
    {
        public AmigoProfile()
        {
            CreateMap<CadastrarAmigoViewModel, Amigo>();

            CreateMap<EditarAmigoViewModel, Amigo>();

            CreateMap<Amigo, ExcluirAmigoViewModel>()
                .ForMember(dest => dest.Multas, opt => opt.MapFrom(src => src.Multas.Select(x => x.ToString())));                

            CreateMap<Amigo, EditarAmigoViewModel>();

        }
    }
}
