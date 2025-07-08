using AutoMapper;
using Clube_da_Leitura.ModuloReserva;
using Clube_da_Leitura.ModuloMultas;
using Clube_da_Leitura.WebApp.Models;

namespace Clube_da_Leitura.WebApp.Profiles
{
    public class ReservaProfile : Profile
    {
        public ReservaProfile()
        {
            CreateMap<CadastrarReservaViewModel, Reserva>();

            CreateMap<EditarReservaViewModel, Reserva>();

            CreateMap<Reserva, ExcluirReservaViewModel>();
                //.ForMember(dest => dest.Revista, opt => opt.MapFrom(src => src.Revista.Select(x => x.ToString())));                

            CreateMap<Reserva, EditarReservaViewModel>();

        }
    }
}
